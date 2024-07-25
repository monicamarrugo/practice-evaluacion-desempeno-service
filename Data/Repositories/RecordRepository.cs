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
        public ResponseTransaction UpdateRecordEvaluation(CreateEvaluationRecordDto recordData)
        {
            throw new NotImplementedException();
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
            var record = recordData.evaluationRecord;
            if (recordData.evaluationRecord.idEvaluationRecord != null &&)
        }
    }
}
