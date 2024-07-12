using AutoMapper;
using Azure;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class QuestionaryService : IQuestionaryService
    {
        private readonly IGenericRepository<Questionaries> _questionaryGenericRepository;
        private readonly IQuestionaryRepository _questionaryRepository;
        private readonly IQuestionariesConfigService _questionariesConfigService;
        private readonly IMapper _mapper;

        public QuestionaryService(IGenericRepository<Questionaries> questionaryGenericRepository,
            IQuestionaryRepository questionaryRepository,
            IQuestionariesConfigService questionariesConfigService,
            IMapper mapper)
        {
            _questionaryGenericRepository = questionaryGenericRepository;
            _questionaryRepository = questionaryRepository;
            _questionariesConfigService = questionariesConfigService;
            _mapper = mapper;
        }


        public List<QuestionaryDto> GetAllQuestionaries()
        {
            List<QuestionaryDto> questionaries = new List<QuestionaryDto>();
            var entities = _questionaryRepository.GetAllIncludes();

            foreach (var entity in entities)
            {
                questionaries.Add(new QuestionaryDto()
                {
                    idQuestionary = entity.IdQuestionary,
                    name = entity.Name,
                    cdQuestionaryType = entity.CdQuestionaryType,
                    nameQuestionaryType = entity.QuestionaryTypes.Name,
                    
                    createUser = entity.CreateUser,
                    createDate = entity.CreateDate !=  null ? entity.CreateDate.Value: null,
                    modifiedUser = entity.ModifiedUser,
                    modifiedDate = entity.ModifiedDate != null? entity.ModifiedDate.Value : null,
                    cdArea = entity.CdArea,
                    nameArea = entity.Area != null?entity.Area.NameArea: null

                    
                });
            }
            return questionaries;
        }

        public List<QuestionaryDto> GetAllQuestionariesByType(string type)
        {
            List<QuestionaryDto> questionaries = new List<QuestionaryDto>();
            var entities = _questionaryRepository.GetByTypeIncludes(type);

            foreach (var entity in entities)
            {
                questionaries.Add(new QuestionaryDto()
                {
                    idQuestionary = entity.IdQuestionary,
                    name = entity.Name,
                    cdQuestionaryType = entity.CdQuestionaryType,
                    nameQuestionaryType = entity.QuestionaryTypes.Name,

                    createUser = entity.CreateUser,
                    createDate = entity.CreateDate != null ? entity.CreateDate.Value : null,
                    modifiedUser = entity.ModifiedUser,
                    modifiedDate = entity.ModifiedDate != null ? entity.ModifiedDate.Value : null,
                    cdArea = entity.CdArea,
                    nameArea = entity.Area != null ? entity.Area.NameArea : null


                });
            }
            return questionaries;
        }

        public QuestionaryDto GetById(int id)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {
                QuestionaryDto questionary = new QuestionaryDto();
                var entity = _questionaryRepository.GetByIdIncludes(id);

                questionary.idQuestionary = entity.IdQuestionary;
                questionary.name = entity.Name;
                questionary.cdQuestionaryType = entity.CdQuestionaryType;
                questionary.nameQuestionaryType = entity.QuestionaryTypes.Name;

                questionary.createUser = entity.CreateUser;
                questionary.createDate = entity.CreateDate != null ? entity.CreateDate.Value : null;
                questionary.modifiedUser = entity.ModifiedUser;
                questionary.modifiedDate = entity.ModifiedDate != null ? entity.ModifiedDate.Value : null;

                return questionary;
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message, ex);
            }
        }

        public ResponseTransaction SaveQuestionary(CreateQuestionaryConfigDto questionaryConfig)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (questionaryConfig == null || questionaryConfig.questionary == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos del cuestionario";
                return response;
            }
            try
            {
                Questionaries Questionary = new Questionaries()
                {
                    Name = questionaryConfig.questionary.name,
                    CdQuestionaryType = questionaryConfig.questionary.cdQuestionaryType,
                    CdArea = questionaryConfig.questionary.cdArea,
                    CreateDate = DateTime.Now
                };
                 

                //var idQuestionary = _questionaryGenericRepository.Add2(Questionary);
                if (questionaryConfig.questionariesConfig.Count > 0)
                {
                    var listConfig = _mapper.Map<List<QuestionariesConfig>>(questionaryConfig.questionariesConfig);
                    Questionary.QuestionariesConfig = listConfig;
                   // questionaryConfig.questionariesConfig.ForEach(x => { x.idQuestionary = Questionary.IdQuestionary; });
                   // _questionariesConfigService.SaveQuestionariesConfig(questionaryConfig.questionariesConfig);
                }
                _questionaryGenericRepository.Add(Questionary); 

                response.error = "NO";
                response.message = "El registro fue creado exitosamente!";
                response.response = Questionary.IdQuestionary.ToString();
                return response;
            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction UpdateQuestionary(CreateQuestionaryConfigDto questionaryConfig)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (questionaryConfig == null || questionaryConfig.questionary == null)
            {

                response.error = "SI";
                response.errorDetail = "Faltan datos del cuestionario";
                return response;
            }
            try
            {
                
                var Questionary =  _mapper.Map<Questionaries>(questionaryConfig.questionary);
                Questionary.ModifiedDate = DateTime.Now;
                _questionaryGenericRepository.Update(Questionary);
                _questionariesConfigService.UpdateQuestionariesConfig(questionaryConfig.questionariesConfig, Questionary.IdQuestionary);
                
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
    }
}
