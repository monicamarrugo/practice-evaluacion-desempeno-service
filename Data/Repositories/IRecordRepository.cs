using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IRecordRepository
    {
        void UpdateRecordEvaluation(CreateEvaluationRecordDto recordData);

        EvaluationRecord GetRecordsTemp(int idEvaluationsRecord);
        EvaluationRecord GetRecords(int idEvaluationsRecord);
        EvaluationRecord GetRecordsTemp(CreateEvaluationRecordDto recordData);
        void RemoveTemp(List<RecordDetailsTemp> records);

        void FinishRecords(CreateEvaluationRecordDto recordData);
    }
}
