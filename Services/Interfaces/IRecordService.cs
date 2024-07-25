using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IRecordService
    {
        ResponseTransaction SaveRecordEvaluation(CreateEvaluationRecordDto recordData);
        ResponseTransaction UpdateRecordEvaluation(CreateEvaluationRecordDto recordData);
        ResponseTransaction SaveRecordKpi(CreateEvaluationKpiRecordDto recordData);

        ResponseTransaction FinishEvaluation(CreateEvaluationRecordDto recordData);
        CreateEvaluationRecordDto GetRecordById(int idEvaluationsRecord);
        CreateEvaluationRecordDto GetRecordTempById(int idEvaluationsRecord);
    }
}
