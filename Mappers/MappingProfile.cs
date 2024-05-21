using AutoMapper;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Questions, QuestionDto>();
            CreateMap<QuestionaryDto, Questionaries>();
            CreateMap<QuestionariesConfigDto, QuestionariesConfig>();
            CreateMap<EvaluationsDto, Evaluations>();
            CreateMap<Evaluations, EvaluationsDto>();
            CreateMap<EvaluationPositionDto, EvaluationsPositions>();
            CreateMap<EvaluationsPositions, EvaluationPositionDto>();
            CreateMap<Positions, EvaluationPositionDto>();
            CreateMap<Evaluations, EvaluationsDto>()
           .ForMember(dto => dto.questionaryName, opt => opt.MapFrom(ent => ent.Questionaries.Name));
        }
    }
}
