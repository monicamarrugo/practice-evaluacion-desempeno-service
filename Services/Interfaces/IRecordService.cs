using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IRecordService
    {
        CreateEvaluationRecordDto SaveRecordEvaluation(CreateEvaluationRecordDto recordData);
        ResponseTransaction UpdateRecordEvaluation(CreateEvaluationRecordDto recordData);
        ResponseTransaction SaveRecordKpi(CreateEvaluationKpiRecordDto recordData);

        ResponseTransaction FinishEvaluation(CreateEvaluationRecordDto recordData);
        Task<CreateEvaluationRecordDto> GetRecordById(int idEvaluationsRecord);
        Task<CreateEvaluationRecordDto> GetRecordTempById(int idEvaluationsRecord);

        Task<PaginatedList<EvaluatorRecordDto>> GetEvaluatorRecordsByParams(SearchEmployeesDto data);
    }
}
