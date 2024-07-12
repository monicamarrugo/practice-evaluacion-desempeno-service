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

            CreateMap<QuestionariesConfig, QuestionariesConfigDto>()
                 .ForMember(dto => dto.nameQuestionES, opt => opt.MapFrom(ent => ent.Questions.NameES))
                 .ForMember(dto => dto.nameQuestionEN, opt => opt.MapFrom(ent => ent.Questions.NameEN))
                 .ForMember(dto => dto.descriptionQuestionES, opt => opt.MapFrom(ent => ent.Questions.DescriptionES))
                 .ForMember(dto => dto.descriptionQuestionEN, opt => opt.MapFrom(ent => ent.Questions.DescriptionEN))
                 .ForMember(dto => dto.idGroups, opt => opt.MapFrom(ent => ent.Questions.QuestionGroupRelations.Count > 0 ?
                        ent.Questions.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0))
                 .ForMember(dto => dto.groupNameES, opt => opt.MapFrom(ent => ent.Questions.QuestionGroupRelations.Count > 0 ?
                        ent.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno..."))
                 .ForMember(dto => dto.groupNameEN, opt => opt.MapFrom(ent => ent.Questions.QuestionGroupRelations.Count > 0 ?
                        ent.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno..."))
                 .ForMember(dto => dto.cdArea, opt => opt.MapFrom(ent => ent.Questions.CdArea))
                 .ForMember(dto => dto.nameArea, opt => opt.MapFrom(ent => ent.Questions.Areas.NameArea));

            CreateMap<EvaluationsDto, Evaluations>();
            CreateMap<Evaluations, EvaluationsDto>();
            CreateMap<EvaluationPositionDto, EvaluationsPositions>();
            CreateMap<EvaluationsPositions, EvaluationPositionDto>();
            CreateMap<Positions, EvaluationPositionDto>();
            CreateMap<Evaluations, EvaluationsDto>()
           .ForMember(dto => dto.questionaryName, opt => opt.MapFrom(ent => ent.Questionaries.Name));

            CreateMap<Employees, EmployeeDto>()
           .ForMember(dto => dto.namePosition, opt => opt.MapFrom(ent => ent.Positions.NamePosition))
           .ForMember(dto => dto.nameDivisions, opt => opt.MapFrom(ent => ent.Divisions.Name))
           .ForMember(dto => dto.nameResponsible, opt => opt.MapFrom(ent => ent.Responsible.Names + " " + ent.Responsible.LastNames));
        }
    }
}
