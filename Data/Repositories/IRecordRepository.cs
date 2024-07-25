using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IRecordRepository
    {
        ResponseTransaction UpdateRecordEvaluation(CreateEvaluationRecordDto recordData);

        EvaluationRecord GetRecordsTemp(int idEvaluationsRecord);
        EvaluationRecord GetRecords(int idEvaluationsRecord);
        void RemoveTemp(List<RecordDetailsTemp> records);
    }
}
