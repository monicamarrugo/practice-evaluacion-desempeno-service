using AutoMapper;
using Azure;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Enums;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IGenericRepository<Evaluations> _evaluationsGenericRepository;
        private readonly IEvaluationRepository _evaluationsRepository;
        private readonly IMapper _mapper;

        public EvaluationService(IGenericRepository<Evaluations> evaluationsGenericRepository, 
            IMapper mapper, IEvaluationRepository evaluationsRepository)
        {
            _evaluationsGenericRepository = evaluationsGenericRepository;
            _mapper = mapper;
            _evaluationsRepository = evaluationsRepository;
        }


        public async Task<ActiveEvaluationsDto> GetActiveEvaluation(SearchActiveEvaluationDto dataSearch)
        {
            List<Evaluations> entities = new List<Evaluations>();
            List<EvaluationsDto> evaluations = new List<EvaluationsDto>();
            ActiveEvaluationsDto activeEvaluationsDto = new ActiveEvaluationsDto();
            entities = await  _evaluationsRepository.GetActiveEvaluations(dataSearch);
            evaluations = _mapper.Map<List<EvaluationsDto>>(entities);
            activeEvaluationsDto.evaluations = evaluations;
            activeEvaluationsDto.numTotalEvaluations = entities.Count;
            activeEvaluationsDto.numPerformanceEvaluations = entities
                                   .Where(e => e.CdTypeEvaluation == QuestionaryTypeEnum.Evaluation.GetStringValue()).Count();
            activeEvaluationsDto.numIndicadorsEvaluations = entities
                                  .Where(e => e.CdTypeEvaluation == QuestionaryTypeEnum.Indicators.GetStringValue()).Count();
            return activeEvaluationsDto;
        }

        public List<EvaluationsDto> GetAllEvaluations()
        {
            List<Evaluations> entities = new List<Evaluations>();
            List<EvaluationsDto> evaluations = new List<EvaluationsDto>();
            entities = _evaluationsRepository.GetEvaluationsInclude().ToList();
            evaluations = _mapper.Map<List<EvaluationsDto>>(entities);
            return evaluations;
        }

        public EvaluationCreateDto GetEvaluationsByIdInclude(int idEvaluations)
        {
            EvaluationCreateDto evaluations = new EvaluationCreateDto();
            var entity = _evaluationsRepository.GetEvaluationsByIdInclude(idEvaluations);
            if(entity != null) {
                var eevaluations = _mapper.Map<EvaluationsDto>(entity);
                var epositions = _mapper.Map<List<EvaluationPositionDto>>(entity.EvaluationsPositions);
                evaluations.evaluationPosition = epositions;
                evaluations.evaluation = eevaluations;

            }
            return evaluations;
        }

        public EvaluationsDto GetEvaluationsById(int idEvaluations)
        {
            EvaluationsDto evaluations = new EvaluationsDto();
            var entity = _evaluationsGenericRepository.GetById(idEvaluations);
            if (entity != null)
            {
                evaluations = _mapper.Map<EvaluationsDto>(entity);

            }
            return evaluations;
        }

        public ResponseTransaction SaveEvaluation(EvaluationCreateDto evaluationData)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (evaluationData == null || evaluationData.evaluation == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos de la evaluación";
                return response;
            }
            try
            {
                var evaluation = _mapper.Map<Evaluations>(evaluationData.evaluation);
                evaluation.CreateDate = DateTime.Now;
                if(evaluationData.evaluationPosition != null && evaluationData.evaluationPosition.Count > 0)
                {
                    var positions = _mapper.Map<List<EvaluationsPositions>>(evaluationData.evaluationPosition);
                    evaluation.EvaluationsPositions = positions;
                }
                
                _evaluationsGenericRepository.Add(evaluation);
                response.error = "NO";
                response.message = "La Evaluacion fue creada exitosamente!";
                return response;

            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction UpdateEvaluation(EvaluationCreateDto evaluationData)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (evaluationData == null || evaluationData.evaluation == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos de la evaluación";
                return response;
            }
            try
            {
                _evaluationsRepository.UpdateEvaluation(evaluationData);
                response.error = "NO";
                response.message = "La Evaluación fue actualizada exitosamente!";
                return response;

            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction EnableEvaluation(EvaluationCreateDto evaluationData)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (evaluationData == null || evaluationData.evaluation == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos de la evaluación";
                return response;
            }
            try
            {
                _evaluationsRepository.EnableEvaluation(evaluationData);
                response.error = "NO";
                response.message = "La Evaluación fue actualizada exitosamente!";
                return response;

            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }
    }
}
