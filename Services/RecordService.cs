using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;

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

        public ResponseTransaction SaveRecordEvaluation(CreateEvaluationRecordDto recordData)
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
                record.CreateDate = DateTime.Now;
                if (recordData.recordDetails != null && recordData.recordDetails.Count > 0)
                {
                    var details = _mapper.Map<List<RecordDetailsTemp>>(recordData.recordDetails);
                    record.RecordDetailsTemp = details;
                }

                _recordGenericRepository.Add(record);
                response.error = "NO";
                response.message = "El registro fue guardado exitosamente!";
                return response;

            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
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
                var record = _mapper.Map<EvaluationRecord>(recordData.evaluationRecord);
                record.CreateDate = DateTime.Now;
                if (recordData.recordDetails != null && recordData.recordDetails.Count > 0)
                {
                    var details = _mapper.Map<List<RecordDetails>>(recordData.recordDetails);
                    record.RecordDetails = details;
                }

                _recordGenericRepository.Add(record);

                var detailsTemp = _mapper.Map<List<RecordDetailsTemp>>(recordData.recordDetails);
                _recordRepository.RemoveTemp(detailsTemp);

                response.error = "NO";
                response.message = "El registro fue guardado exitosamente!";
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
