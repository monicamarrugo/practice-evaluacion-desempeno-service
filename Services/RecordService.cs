using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace EvaluacionDesempenoApi.Services
{
    public class RecordService : IRecordService
    {
        private readonly IGenericRepository<EvaluationRecord> _recordGenericRepository;
        private readonly IGenericRepository<RecordDetailsTemp> _recordTempGenericRepository;
        private readonly IRecordRepository _recordRepository;
        private readonly IMapper _mapper;

        public RecordService(IGenericRepository<EvaluationRecord> recordGenericRepository,
            IGenericRepository<RecordDetailsTemp> recordTempGenericRepository,
            IRecordRepository recordRepository,
            IMapper mapper)
        {
            _recordGenericRepository = recordGenericRepository;
            _recordTempGenericRepository = recordTempGenericRepository;
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public CreateEvaluationRecordDto GetRecordTempById(int idEvaluationsRecord)
        {
            CreateEvaluationRecordDto evaluationRecord = new CreateEvaluationRecordDto();
            var entity = _recordRepository.GetRecordsTemp(idEvaluationsRecord);
            if (entity != null)
            {
                var record = _mapper.Map<EvaluationRecordDto>(entity);
                var details = _mapper.Map<List<RecordDetailsDto>>(entity.RecordDetailsTemp);
                evaluationRecord.evaluationRecord = record;
                evaluationRecord.recordDetails = details;

            }
            return evaluationRecord;
        }

        public CreateEvaluationRecordDto GetRecordById(int idEvaluationsRecord)
        {
            try
            {
                CreateEvaluationRecordDto evaluationRecord = new CreateEvaluationRecordDto();
                var entity = _recordRepository.GetRecords(idEvaluationsRecord);
                if (entity != null)
                {
                    var record = _mapper.Map<EvaluationRecordDto>(entity);
                    var details = _mapper.Map<List<RecordDetailsDto>>(entity.RecordDetails);
                    evaluationRecord.evaluationRecord = record;
                    evaluationRecord.recordDetails = details;

                }
                return evaluationRecord;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public CreateEvaluationRecordDto SaveRecordEvaluation(CreateEvaluationRecordDto recordData)
        {
            ResponseTransaction response = new ResponseTransaction();
            CreateEvaluationRecordDto evaluationRecord = new CreateEvaluationRecordDto();
            if (recordData == null || recordData.evaluationRecord == null)
            {
                throw new Exception("Faltan datos del registro");
            }
            try
            {
                var record = _mapper.Map<EvaluationRecord>(recordData.evaluationRecord);
                record.CreateDate = DateTime.Now;
                if (recordData.recordDetails != null && recordData.recordDetails.Count > 0)
                {
                    var details = _mapper.Map<List<RecordDetailsTemp>>(recordData.recordDetails);
                    record.RecordDetailsTemp = details;
                }

                _recordGenericRepository.Add(record);
                var entityExists = _recordRepository.GetRecordsTemp(recordData);
                var recordDto = _mapper.Map<EvaluationRecordDto>(entityExists);
                var detailsDto = _mapper.Map<List<RecordDetailsDto>>(entityExists.RecordDetailsTemp);

                evaluationRecord.evaluationRecord = recordDto;
                evaluationRecord.recordDetails = detailsDto;

                return evaluationRecord;

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public ResponseTransaction UpdateRecordEvaluation(CreateEvaluationRecordDto recordData)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (recordData == null || recordData.evaluationRecord == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos del registro";
                return response;
            }
            try
            {
                var record = _mapper.Map<EvaluationRecord>(recordData.evaluationRecord);
                if (recordData.recordDetails != null && recordData.recordDetails.Count > 0)
                {
                    var details = _mapper.Map<List<RecordDetailsTemp>>(recordData.recordDetails);
                    record.RecordDetailsTemp = details;
                }

                _recordGenericRepository.Update(record);
                response.error = "NO";
                response.message = "El registro fue actualizado exitosamente!";
                return response;

            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction FinishEvaluation(CreateEvaluationRecordDto recordData)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (recordData == null || recordData.evaluationRecord == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos del registro";
                return response;
            }
            try
            {
                _recordRepository.FinishRecords(recordData);
               
                if(recordData.evaluationRecord.idEvaluationRecord != null 
                    && recordData.evaluationRecord.idEvaluationRecord != 0)
                {
                    var detailsTemp = _mapper.Map<List<RecordDetailsTemp>>(recordData.recordDetails);
                    _recordRepository.RemoveTemp(detailsTemp);
                }
               

                response.error = "NO";
                response.message = "El registro fue finalizado exitosamente!";
                return response;

            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction SaveRecordKpi(CreateEvaluationKpiRecordDto recordData)
        {
            throw new NotImplementedException();
        }
    }
}
