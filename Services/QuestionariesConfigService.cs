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

            configs = _mapper.Map<List<QuestionariesConfigDto>>(entities);
            return configs;
        }

        public List<RecordDetailsDto> GetQuestionaryToRecord(int idQuestionary)
        {
            List<RecordDetailsDto> configsRecords = new List<RecordDetailsDto>();
            var entities = _questionariesConfigRepository.GetByQuestionaryIncludes(idQuestionary);

            configsRecords = _mapper.Map<List<RecordDetailsDto>>(entities);
            return configsRecords;
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
            questionary.idQuestionary = firstElement.IdQuestionary;
            questionary.createDate = firstElement.Questionary.CreateDate;
            questionary.createUser = firstElement.Questionary.CreateUser;

            configs = _mapper.Map<List<QuestionariesConfigDto>>(entities);
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
