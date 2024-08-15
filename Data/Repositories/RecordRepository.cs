using AutoMapper;
using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public EvaluationRecord GetRecordsTemp(int idEvaluationsRecord)
        {
            return _dbContext.EvaluationRecord
                .Include(e => e.RecordDetailsTemp)
                .Where(r => r.IdEvaluationRecord == idEvaluationsRecord)
                .FirstOrDefault();
        }

        public EvaluationRecord GetRecords(int idEvaluationsRecord)
        {
            return _dbContext.EvaluationRecord
                .Include(e => e.RecordDetails)
                .Where(r => r.IdEvaluationRecord == idEvaluationsRecord)
                .FirstOrDefault();
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
