using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;
using System.Security.Cryptography;
using static Azure.Core.HttpHeader;

namespace EvaluacionDesempenoApi.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepository<Questions> _genericRepository;
        private readonly IGenericRepository<Formats> _formatsRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IGenericRepository<QuestionGroupRelation> _questionGroupRelationRepository;
        private readonly IMapper mapper;

        public QuestionService(IGenericRepository<Questions> genericRepository, IQuestionRepository questionRepository,
            IGenericRepository<QuestionGroupRelation> questionGroupRelationRepository, IGenericRepository<Formats> formatsRepository,
            IMapper _mapper)
        {
            _genericRepository = genericRepository;
            _questionRepository = questionRepository;
            _questionGroupRelationRepository = questionGroupRelationRepository;
            _formatsRepository = formatsRepository;
            mapper = _mapper;
        }

        public void DisableQuestion()
        {
            throw new NotImplementedException();
        }
        public QuestionDto GetById(int id)
        {
            ResponseTransaction response = new ResponseTransaction();
            QuestionDto question = new QuestionDto();
            try
            {

                var entity = _questionRepository.GetByIdIncludes(id);
                if(entity != null)
                {
                    //mapper.Map<QuestionDto>(valorIndice);
                    question.idQuestion = entity.IdQuestions;
                    question.idGroups = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0;
                    question.idQuestionGroup = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().IdQuestionGroupRelation : 0;
                    question.idQuestionType = entity.IdQuestionType;
                    question.groupNameES = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno...";
                    question.groupNameEN = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno...";
                    question.descriptionES = entity.DescriptionES;
                    question.descriptionEN = entity.DescriptionEN;
                    question.nameES = entity.NameES;
                    question.nameEN = entity.NameEN;
                    question.idFormats = entity.IdFormats;
                    question.cdArea = entity.CdArea;

                }

                return question;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<QuestionDto> GetAllQuestions()
        {
            List<QuestionDto> questions = new List<QuestionDto>();
            var entities = _questionRepository.GetAllIncludes();

            foreach ( var entity in entities )
            {
                questions.Add(new QuestionDto()
                {
                    idQuestion = entity.IdQuestions,
                    idGroups = entity.QuestionGroupRelations.Count > 0?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups: 0,
                    idQuestionGroup = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().IdQuestionGroupRelation : 0,
                    idQuestionType = entity.IdQuestionType,
                    groupNameES = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno...",
                    groupNameEN = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno...",
                    descriptionES = entity.DescriptionES,
                    descriptionEN = entity.DescriptionEN,
                    idFormats = entity.IdFormats,
                    nameES = entity.NameES,
                    nameEN = entity.NameEN,
                    cdArea = entity.CdArea
                });
            }
            return questions;
        }

        public ResponseTransaction SaveQuestion(QuestionDto question)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {
                Questions questionCreate = new Questions()
                {
                    IdQuestionType = question.idQuestionType,
                    IdFormats = question.idQuestionType == 1? 4: question.idFormats.Value,
                    DescriptionEN = question.descriptionEN,
                    DescriptionES = question.descriptionES,
                    CdArea = question.cdArea,
                    NameES = question.nameES,
                    NameEN = question.nameEN,
                    Enabled = true,
                    CreateDate = DateTime.Now
                };
                if (question.idGroups !=null && question.idGroups != 0)
                {
                    QuestionGroupRelation
                    questionGroupRelation = new QuestionGroupRelation()
                    {
                        IdGroups = question.idGroups.Value,
                        Questions = questionCreate,
                        CreateDate = DateTime.Now
                    };
                    _questionGroupRelationRepository.Add(questionGroupRelation);
                }
                else
                {
                    _genericRepository.Add(questionCreate);
                }

                response.error = "NO";
                response.message = "El registro fue creado exitosamente!";
                return response;
            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction UpdateQuestion(QuestionDto question)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {
                Questions questionUpdate = new Questions()
                {
                    IdQuestions = question.idQuestion,
                    IdQuestionType = question.idQuestionType,
                    IdFormats = question.idQuestionType == 1 ? 4 : question.idFormats.Value,
                    DescriptionEN = question.descriptionEN,
                    DescriptionES = question.descriptionES,
                    NameES = question.nameES,
                    NameEN = question.nameEN,
                    CdArea = question.cdArea,
                    Enabled = true,
                    CreateDate= DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
            
                if(question.idGroups != 0 && question.idGroups != null)
                {
                    if (question.idQuestionGroup != 0 && question.idQuestionGroup != null)
                    {
                        QuestionGroupRelation
                        questionGroupRelation = new QuestionGroupRelation()
                        {
                            IdQuestionGroupRelation = question.idQuestionGroup.Value,
                            IdGroups = question.idGroups.Value,
                            ModifiedDate = DateTime.Now,
                            CreateDate = DateTime.Now,
                            Questions = questionUpdate

                        };
                        _questionGroupRelationRepository.Update(questionGroupRelation);
                    }
                    else
                    {
                        QuestionGroupRelation
                        questionGroupRelation = new QuestionGroupRelation()
                        {
                            IdGroups = question.idGroups.Value,
                            IdQuestions = question.idQuestion,
                            CreateDate = DateTime.Now
                        };
                        _questionGroupRelationRepository.Add(questionGroupRelation);
                    }
                }
                else
                {
                    if (question.idQuestionGroup != 0 && question.idQuestionGroup != null)
                    {
                        QuestionGroupRelation
                        questionGroupRelation = new QuestionGroupRelation()
                        {
                            IdQuestionGroupRelation = question.idQuestionGroup.Value

                        };
                        _questionGroupRelationRepository.Delete(questionGroupRelation);
                    }
                    _genericRepository.Update(questionUpdate);
                }
                
               
                response.error = "NO";
                response.message = "Registro actualizado con exito!";
                return response;
            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
            
        }
        public List<FormatDto> GetAllFormats()
        {
            List<FormatDto> formats = new List<FormatDto>();
            var entities = _formatsRepository.GetAll().ToList();

            foreach (var entity in entities)
            {
                formats.Add(new FormatDto()
                {
                    idFormats = entity.IdFormats,
                    cdFormats = entity.CdFormats,
                    nameFormats = entity.NameFormats,
                    descriptionFormats = entity.DescriptionFormats,
                    display = entity.display
                });
            }
            return formats;
        }

        List<QuestionDto> IQuestionService.GetQuestionsByType(string questionType)
        {
            List<QuestionDto> questions = new List<QuestionDto>();
            var entities = _questionRepository.GetByType(questionType);

            foreach (var entity in entities)
            {
                questions.Add(new QuestionDto()
                {
                    idQuestion = entity.IdQuestions,
                    idGroups = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0,
                    idQuestionGroup = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().IdQuestionGroupRelation : 0,
                    idQuestionType = entity.IdQuestionType,
                    groupNameES = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno...",
                    groupNameEN = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno...",
                    descriptionES = entity.DescriptionES,
                    descriptionEN = entity.DescriptionEN,
                    idFormats = entity.IdFormats,
                    nameES = entity.NameES,
                    nameEN = entity.NameEN,
                    cdArea = entity.CdArea
                });
            }
            return questions;
        }

        List<QuestionDto> IQuestionService.GetQuestionsByGroup(int group)
        {
            List<QuestionDto> questions = new List<QuestionDto>();
            var entities = _questionRepository.GetByGroup(group);

            foreach (var entity in entities)
            {
                questions.Add(new QuestionDto()
                {
                    idQuestion = entity.IdQuestions,
                    idGroups = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0,
                    idQuestionGroup = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().IdQuestionGroupRelation : 0,
                    idQuestionType = entity.IdQuestionType,
                    groupNameES = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno...",
                    groupNameEN = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno...",
                    descriptionES = entity.DescriptionES,
                    descriptionEN = entity.DescriptionEN,
                    idFormats = entity.IdFormats,
                    nameES = entity.NameES,
                    nameEN = entity.NameEN,
                    cdArea = entity.CdArea
                });
            }
            return questions;
        }

        List<QuestionDto> IQuestionService.GetQuestionsByArea(string area)
        {
            List<QuestionDto> questions = new List<QuestionDto>();
            var entities = _questionRepository.GetByArea(area);

            foreach (var entity in entities)
            {
                questions.Add(new QuestionDto()
                {
                    idQuestion = entity.IdQuestions,
                    idGroups = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0,
                    idQuestionGroup = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().IdQuestionGroupRelation : 0,
                    idQuestionType = entity.IdQuestionType,
                    groupNameES = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno...",
                    groupNameEN = entity.QuestionGroupRelations.Count > 0 ?
                        entity.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno...",
                    descriptionES = entity.DescriptionES,
                    descriptionEN = entity.DescriptionEN,
                    idFormats = entity.IdFormats,
                    nameES = entity.NameES,
                    nameEN = entity.NameEN,
                    cdArea = entity.CdArea,
                    nameArea = entity.Areas != null?entity.Areas.NameArea:null
                });
            }
            return questions;
        }
    }
}
