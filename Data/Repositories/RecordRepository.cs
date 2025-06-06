using AutoMapper;
using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Enums;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RecordRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public EvaluationRecord GetRecordsTemp(CreateEvaluationRecordDto recordData)
        {
            var existsRecord = _dbContext.EvaluationRecord
                .Include(e => e.RecordDetailsTemp)
                .Where(r => ((r.IdEvaluator == recordData.evaluationRecord.idEvaluator) &&
                              (r.IdEmployee == recordData.evaluationRecord.idEmployee) &&
                              (r.IdEvaluations == recordData.evaluationRecord.idEvaluations))).FirstOrDefault();
            return existsRecord;
        }
        public int GetIdRecords(CreateEvaluationRecordDto recordData)
        {
            var idRecord = _dbContext.EvaluationRecord
                         .Where(r => ((r.IdEvaluator == recordData.evaluationRecord.idEvaluator) &&
                              (r.IdEmployee == recordData.evaluationRecord.idEmployee) &&
                              (r.IdEvaluations == recordData.evaluationRecord.idEvaluations)))
                         .Select(r => r.IdEvaluationRecord)
                         .FirstOrDefault();
            return idRecord;
        }
        public async Task<List<EvaluationRecord>> GetEvaluationRecordsByEvaluatorAsync(
            string? evaluatorIdNumber = null,
            string? evaluatorName = null)
        {
           // Inicializar la consulta base
            var query = _dbContext.EvaluationRecord
                .Include(er => er.Evaluator) // Cargar los datos del evaluador relacionados
                .Include(er => er.Employee)  // Opcional: Cargar el empleado evaluado si es necesario
                .AsQueryable();

            // Aplicar filtros dinámicos según los parámetros proporcionados
            if (!string.IsNullOrEmpty(evaluatorIdNumber))
            {
                query = query.Where(er => er.Evaluator.Identification == evaluatorIdNumber);
            }

            if (!string.IsNullOrEmpty(evaluatorName))
            {
                query = query.Where(er =>
                    (er.Evaluator.Names + " " + er.Evaluator.LastNames).Contains(evaluatorName));
            }

            // Ejecutar la consulta y devolver los resultados
            return await query.ToListAsync();
        }

        public async Task<PaginatedList<EvaluatorRecordDto>> GetEvaluatorRecordsByParams(SearchEmployeesDto data)
        {
            var idPositionsList = data.idPositions.Select(p => p.idPosition).ToList();
            // Inicializar la consulta base
            var query = _dbContext.EvaluationRecord
                .Include(er => er.Evaluator)
                .ThenInclude(e => e.Subordinates)
                .ThenInclude(e => e.Positions)// Cargar los datos del evaluador relacionados
                .AsQueryable();

            // Aplicar filtros dinámicos según los parámetros proporcionados
            if (!string.IsNullOrEmpty(data.identificationEvaluator))
            {
                query = query.Where(er => er.Evaluator.Identification == data.identificationEvaluator);
            }

            if (!string.IsNullOrEmpty(data.nameEvaluator))
            {
                query = query.Where(er =>
                    (er.Evaluator.Names + " " + er.Evaluator.LastNames).Contains(data.nameEvaluator));
            }

            // Ejecutar la consulta y devolver los resultados
            var records = await query.ToListAsync();

            var totalRecords = records.Count();

            var paginatedRecords = records
                .Skip((data.pageNumber - 1) * data.pageSize)
                .Take(data.pageSize)
                .ToList();

            // Procesar los datos en memoria
            var result = paginatedRecords
                .Select(record =>
                {
                  
                    return new EvaluatorRecordDto
                    {
                        evaluatorId = record.IdEvaluator,
                        evaluatorName = $"{record.Evaluator.Names} {record.Evaluator.LastNames}",
                        positionName = record.Positions.NamePosition,
                        totalSubordinates = record.Evaluator.Subordinates.Count(s => s.Enabled),
                        totalEnables = record.Evaluator.Subordinates.Count(sub =>
                            (data.cdDivisions == null || sub.CdDivisions == data.cdDivisions) &&
                            (data.idPositions.Count == 0 || idPositionsList.Contains(sub.IdPosition)) && sub.Enabled),
                        totalDone = records.Count(e => e.CdEvaluationStates == RecordStateEnum.Finished.GetStringValue()),
                        evaluationRecords = records.Select(evaluation => new EvaluationRecordDto
                        {
                            idEvaluationRecord = evaluation.IdEvaluationRecord,
                            startDate = evaluation.StartDate,
                            startDateLocal = evaluation.StartDateLocal,
                            endDate = evaluation.EndDate,
                            endDateLocal = evaluation.EndDateLocal,
                            finalCalification = evaluation.FinalCalification,
                            cdEvaluationStates = evaluation.CdEvaluationStates,
                            idEmployee = evaluation.IdEmployee,
                            nameEmployee = $"{evaluation.Employee?.Names} {evaluation.Employee?.LastNames}",
                        }).ToList()
                    };
                })
                .ToList();

            var paginatedResult = new PaginatedList<EvaluatorRecordDto>(result, totalRecords, data.pageNumber, data.pageSize);

            return paginatedResult;
        }

        public void UpdateRecordEvaluation(CreateEvaluationRecordDto recordData)
        {
            var existsRecord = _dbContext.EvaluationRecord
                .Where(r => ((r.IdEvaluator == recordData.evaluationRecord.idEvaluator) && 
                              (r.IdEmployee == recordData.evaluationRecord.idEmployee) &&
                              (r.IdEvaluations == recordData.evaluationRecord.idEvaluations))).FirstOrDefault();

           
            if (existsRecord == null)
            {
              
                var record = _mapper.Map<EvaluationRecord>(recordData.evaluationRecord);
                if (recordData.recordDetails != null && recordData.recordDetails.Count > 0)
                {
                    var details = _mapper.Map<List<RecordDetailsTemp>>(recordData.recordDetails);
                    record.RecordDetailsTemp = details;
                }
                
                _dbContext.Add(record);
            }
            else
            {
                _mapper.Map(recordData.evaluationRecord, existsRecord);
                if (recordData.recordDetails != null && recordData.recordDetails.Count > 0)
                {
                    var details = _mapper.Map<List<RecordDetailsTemp>>(recordData.recordDetails);
                    existsRecord.RecordDetailsTemp = details;
                }
            }

            _dbContext.SaveChanges();
        }

        public async Task<EvaluationRecord> GetRecordsTemp(int idEvaluationsRecord)
        {
            EvaluationRecord record = null;
            if (idEvaluationsRecord > 0) { 
                
                record = await _dbContext.EvaluationRecord
                .Include(e => e.RecordDetailsTemp)
                .Where(r => r.IdEvaluationRecord == idEvaluationsRecord)
                .FirstOrDefaultAsync();
            }
            return record;
        }

        public async Task<EvaluationRecord> GetRecords(int idEvaluationsRecord)
        {
            EvaluationRecord record = null;
            if (idEvaluationsRecord > 0)
            {
                record = await  _dbContext.EvaluationRecord
               .Include(e => e.RecordDetails)
               .Where(r => r.IdEvaluationRecord == idEvaluationsRecord)
               .FirstOrDefaultAsync();
            }
            return record;
        }

        public void RemoveTemp(List<RecordDetailsTemp> records)
        {
            _dbContext.RecordDetailsTemp.RemoveRange(records);
            _dbContext.SaveChanges();
        }

        public void FinishRecords(CreateEvaluationRecordDto recordData)
        {
           var record = _mapper.Map<EvaluationRecord>(recordData.evaluationRecord);
            var details = _mapper.Map<List<RecordDetails>>(recordData.recordDetails);
            if (record.IdEvaluationRecord == null || record.IdEvaluationRecord == 0)
            {
                record.CreateDate = DateTime.Now.ToUniversalTime();
                record.RecordDetails = details;

                _dbContext.EvaluationRecord.Add(record);
               
            }
            else
            {
                record.ModifiedDate = DateTime.Now.ToUniversalTime();
                _dbContext.EvaluationRecord.Update(record);

                details.ForEach(e => {
                    e.IdRecordDetails = 0;
                }
                );

                _dbContext.RecordDetails.AddRange(details);
            }
            _dbContext.SaveChanges();
        }
    }
}
