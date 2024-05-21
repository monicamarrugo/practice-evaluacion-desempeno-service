using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using System.Security.Cryptography;

namespace EvaluacionDesempenoApi.Services
{
    public class QuestionariesConfigService : IQuestionariesConfigService
    {
        private readonly IGenericRepository<QuestionariesConfig> _questionariesConfigGenericRepository;
        private readonly IQuestionariesConfigRepository _questionariesConfigRepository;
        private readonly IMapper _mapper;

        public QuestionariesConfigService(IQuestionariesConfigRepository questionariesConfigRepository,
            IGenericRepository<QuestionariesConfig> questionariesConfigGenericRepository,
            IMapper mapper)
        {
            _questionariesConfigGenericRepository = questionariesConfigGenericRepository;
            _questionariesConfigRepository = questionariesConfigRepository;
            _mapper = mapper;
        }
        public List<QuestionariesConfigDto> GetByQuestionary(int idQuestionary)
        {
            List<QuestionariesConfigDto> configs = new List<QuestionariesConfigDto>();
            var entities = _questionariesConfigRepository.GetByQuestionaryIncludes(idQuestionary);

            foreach (var entity in entities)
            {
                configs.Add(new QuestionariesConfigDto()
                {
                    idQuestionaryConfig = entity.IdQuestionaryConfig,
                    idQuestionary = entity.IdQuestionary,
                    idQuestions = entity.IdQuestions,
                    nameQuestionES = entity.Questions.NameES,
                    nameQuestionEN = entity.Questions.NameEN,
                    descriptionQuestionES = entity.Questions.DescriptionES,
                    descriptionQuestionEN = entity.Questions.DescriptionEN,
                    idGroups = entity.Questions.QuestionGroupRelations.Count > 0 ?
                        entity.Questions.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0,
                    groupNameES = entity.Questions.QuestionGroupRelations.Count > 0 ?
                        entity.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno...",
                    groupNameEN = entity.Questions.QuestionGroupRelations.Count > 0 ?
                        entity.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno...",
                    cdArea = entity.Questions.CdArea != null? entity.Questions.CdArea: null,
                    nameArea = entity.Questions.Areas != null ? entity.Questions.Areas.NameArea:null,
                    incentive = entity.Incentive != null ? entity.Incentive.Value: 0,
                    weight = entity.Weight,
                    noApplyScale = entity.NoApplyScale,
                    control = entity.Control
                });
            }
            return configs;
        }
        public CreateQuestionaryConfigDto GetCompleteByQuestionary(int idQuestionary)
        {
            CreateQuestionaryConfigDto configCreate = new CreateQuestionaryConfigDto();
            List<QuestionariesConfigDto> configs = new List<QuestionariesConfigDto>();
            QuestionaryDto questionary = new QuestionaryDto();
            var entities = _questionariesConfigRepository.GetByQuestionaryIncludesComplete(idQuestionary);
            if(entities.Count <= 0)
            {
                throw new Exception("No existen preguntas relacionadas al cuestionario ");
            }
            var firstElement = entities.FirstOrDefault();
            questionary.cdQuestionaryType = firstElement.Questionary.CdQuestionaryType;
            questionary.name = firstElement.Questionary.Name;
            questionary.cdArea = firstElement.Questionary.CdArea;
            questionary.iDProcessLeader = firstElement.Questionary.IDProcessLeader;
            questionary.idQuestionary = firstElement.IdQuestionary;
            questionary.createDate = firstElement.Questionary.CreateDate;
            questionary.createUser = firstElement.Questionary.CreateUser;

            foreach (var entity in entities)
            {
                configs.Add(new QuestionariesConfigDto()
                {
                    idQuestionaryConfig = entity.IdQuestionaryConfig,
                    idQuestionary = entity.IdQuestionary,
                    idQuestions = entity.IdQuestions,
                    nameQuestionES = entity.Questions.NameES,
                    nameQuestionEN = entity.Questions.NameEN,
                    descriptionQuestionES = entity.Questions.DescriptionES,
                    descriptionQuestionEN = entity.Questions.DescriptionEN,
                    idGroups = entity.Questions.QuestionGroupRelations.Count > 0 ?
                        entity.Questions.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0,
                    groupNameES = entity.Questions.QuestionGroupRelations.Count > 0 ?
                        entity.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno...",
                    groupNameEN = entity.Questions.QuestionGroupRelations.Count > 0 ?
                        entity.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno...",
                    cdArea = entity.Questions.CdArea != null ? entity.Questions.CdArea : null,
                    nameArea = entity.Questions.Areas != null ? entity.Questions.Areas.NameArea : null,
                    incentive = entity.Incentive != null ? entity.Incentive.Value : 0,
                    weight = entity.Weight,
                    noApplyScale = entity.NoApplyScale,
                    control = entity.Control
                });
            }
            configCreate.questionary = questionary;
            configCreate.questionariesConfig = configs;
            return configCreate;
        }

        public ResponseTransaction SaveQuestionariesConfig(List<QuestionariesConfigDto> config)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (config == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos de la configuración";
                return response;
            }
            try
            {
                var listConfig = _mapper.Map<List<QuestionariesConfig>>(config);
                _questionariesConfigGenericRepository.AddRange(listConfig);
                response.error = "NO";
                response.message = "El registro fue creado exitosamente!";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public ResponseTransaction UpdateQuestionariesConfig(List<QuestionariesConfigDto> UpdatedConfigs, int idQuestionary)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (UpdatedConfigs == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos de la configuración";
                return response;
            }
            try
            {
                _questionariesConfigRepository.UpdateConfigs(UpdatedConfigs, idQuestionary);
                response.error = "NO";
                response.message = "El registro fue actualizado exitosamente!";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
