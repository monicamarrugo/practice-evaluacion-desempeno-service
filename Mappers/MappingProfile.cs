using AutoMapper;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;

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
            CreateMap<EvaluationPositionDto, EvaluationsPositions>();
            CreateMap<EvaluationsPositions, EvaluationPositionDto>();
            CreateMap<Positions, EvaluationPositionDto>();
            CreateMap<Evaluations, EvaluationsDto>()
           .ForMember(dto => dto.questionaryName, opt => opt.MapFrom(ent => ent.Questionaries.Name))
           .ForMember(dto => dto.scaleValues, opt => opt.MapFrom(ent => ent.Escales.EscalesValues));

            CreateMap<Employees, EmployeeDto>()
           .ForMember(dto => dto.namePosition, opt => opt.MapFrom(ent => ent.Positions.NamePosition))
           .ForMember(dto => dto.nameDivisions, opt => opt.MapFrom(ent => ent.Divisions.Name))
           .ForMember(dto => dto.nameResponsible, opt => opt.MapFrom(ent => ent.Responsible.Names + " " + ent.Responsible.LastNames))
           .ForMember(dto => dto.nameArea, opt => opt.MapFrom(ent => ent.Areas.NameArea));

            CreateMap<EvaluationRecordDto, EvaluationRecord>();
            CreateMap<EvaluationRecord, EvaluationRecordDto>();
            CreateMap<RecordDetailsDto, RecordDetails>();
            CreateMap<RecordDetailsDto, RecordDetailsTemp>();
            CreateMap<RecordDetails, RecordDetailsDto>();
            CreateMap<RecordDetailsTemp, RecordDetailsDto>();
            CreateMap<EscalesValues, EscaleValuesDto>();
            CreateMap<FileDto, Files>();
            CreateMap<Files, FileDto>();
            CreateMap<Flags, FlagsDto>();
            CreateMap<FlagsDto,Flags>();
            CreateMap<FlagRulesDto,FlagRules>();
            CreateMap<FlagRules,FlagRulesDto>();
            CreateMap<Colors, ColorDto>();
            CreateMap<ColorDto, Colors>();
            CreateMap<FlagTypeDto, FlagTypes>();
            CreateMap<FlagTypes, FlagTypeDto>();
            CreateMap<Languages, LanguageDto>();

            CreateMap<QuestionariesConfig, RecordDetailsDto>()
                 .ForMember(dto => dto.idGroups, opt => opt.MapFrom(ent => ent.Questions.QuestionGroupRelations.Count > 0 ?
                        ent.Questions.QuestionGroupRelations.FirstOrDefault().Groups.IdGroups : 0))
                 .ForMember(dto => dto.groupNameES, opt => opt.MapFrom(ent => ent.Questions.QuestionGroupRelations.Count > 0 ?
                        ent.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameES : "Ninguno..."))
                 .ForMember(dto => dto.groupNameEN, opt => opt.MapFrom(ent => ent.Questions.QuestionGroupRelations.Count > 0 ?
                        ent.Questions.QuestionGroupRelations.FirstOrDefault().Groups.NameEN : "Ninguno..."))
                 .ForMember(dto => dto.descriptionES, opt => opt.MapFrom(ent => ent.Questions.DescriptionES))
                 .ForMember(dto => dto.descriptionEN, opt => opt.MapFrom(ent => ent.Questions.DescriptionEN));

            CreateMap<UsersProfiles, UserProfileDto>()
           .ForMember(dto => dto.profileNameES, opt => opt.MapFrom(ent => ent.Profiles.ProfileNameES))
           .ForMember(dto => dto.profileNameEN, opt => opt.MapFrom(ent => ent.Profiles.ProfileNameEN));

            CreateMap<Profiles, UserProfileDto>();
            CreateMap<UserProfileDto, UsersProfiles>();
            CreateMap<UserRegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserRegisterDto>();
        }
    }
}

