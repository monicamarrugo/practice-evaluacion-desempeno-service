using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IRecordRepository
    {
        void UpdateRecordEvaluation(CreateEvaluationRecordDto recordData);

        Task<EvaluationRecord> GetRecordsTemp(int idEvaluationsRecord);
        Task<EvaluationRecord> GetRecords(int idEvaluationsRecord);
        EvaluationRecord GetRecordsTemp(CreateEvaluationRecordDto recordData);
        void RemoveTemp(List<RecordDetailsTemp> records);

        void FinishRecords(CreateEvaluationRecordDto recordData);
        int GetIdRecords(CreateEvaluationRecordDto recordData);

        Task<PaginatedList<EvaluatorRecordDto>> GetEvaluatorRecordsByParams(SearchEmployeesDto data);
    }
}
