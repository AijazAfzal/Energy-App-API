using AutoMapper;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Command;
using Energie.Business.SuperAdmin.MediatR.Commands.Company;
using Energie.Domain.Domain;
using Energie.Model.Request;
using Energie.Model.Response;

namespace Energie.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //For Super Admin
            //CreateMap<AddCompanyRequest, AddCompanyCommand>().ReverseMap();
            CreateMap<Energie.Domain.Domain.Company, Energie.Model.Request.Company>().ReverseMap();
            CreateMap<CreateCompanyCommand, CreateCompanyRequest>().ReverseMap();
            CreateMap<AddCompanyAdminCommand, AddCompanyAdminRequest>().ReverseMap();
            CreateMap<AddTipCommand, AddEnergyTipRequest>().ReverseMap();
            CreateMap<UpdateEnergyTipCommand, UpdateTipRequest>().ReverseMap();
            //For Company Admin
            CreateMap<AddDepartmentRequest, AddDepartmentCommand>().ReverseMap();
            CreateMap<Energie.Domain.Domain.Department, Energie.Model.Request.Department>().ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.CompanyUser.Count)).ReverseMap();
            CreateMap<AddCompanyUserCommand, AddCompanyUserRequest>().ReverseMap();
            //multiple user
            //CreateMap<AddMultipleUserCommandList, AddMultipleUserRequestList>().ForMember(dest=>dest.AddMultipleUserRequests.ForEach()).ReverseMap(); 
            CreateMap<AddMultipleUserRequest, AddMultipleUserCommand>();
            CreateMap<AddMultipleUserRequestList, AddMultipleUserCommandList>()
                .ForMember(dest => dest.AddMultipleUserCommands, opt => opt.MapFrom(src => src.AddMultipleUserRequests));
            CreateMap<Domain.Domain.CompanyUser, DepartmentUser>().ReverseMap();
            CreateMap<AddEmployerHelpForDeparmentCommand, AddEmployerHelpRequest>().ReverseMap();
            //CreateMap<GetDepartmentUserCommand, GetDepartmentUserRequest>().ReverseMap();
            CreateMap<AddMonthlyEnergyRequest, AddMonthlyEnergyScoreCommand>().ReverseMap();
            CreateMap<Model.Response.EnergyAnalysisQuestions, Energie.Domain.Domain.EnergyAnalysisQuestions>().ReverseMap();
            CreateMap<Energie.Domain.Domain.Language, Energie.Model.Request.Language>().ReverseMap();
            CreateMap<AddEnergyAnalysisScoreCommand, AddEnergyAnalysisScoreRequest>().ReverseMap();
            CreateMap<AddCompanyHelpCommand, AddCompanyHelpRequest>().ReverseMap();
            CreateMap<Energie.Domain.Domain.CompanyHelp, Model.Response.CompanyHelp>().ReverseMap();
            CreateMap<UpdateCompanyHelpCommand, UpdateCompanyHelpRequest>().ReverseMap();
            //CreateMap<UserEnergyAnalysis, UserEnergyAnalysisRequest>().ReverseMap();
            CreateMap<UserEnergyAnalysis, Energie.Model.Request.UserEnergyAnalysisRequest>().ReverseMap();
            CreateMap<Energie.Domain.Domain.CompanyUser, Model.Response.CompanyUser>().ReverseMap();
            CreateMap<Energie.Domain.Domain.EnergyAnalysis, Model.Response.EnergyAnalysis>().ReverseMap();
            CreateMap<Energie.Domain.Domain.Tip, Energie.Model.Request.UserEnergyTip>().ReverseMap();
            CreateMap<Energie.Domain.Domain.CompanyHelp, Energie.Model.Request.CompanyUserEnergyTip>().ReverseMap();
            CreateMap<Energie.Domain.Domain.CompanyHelp, Model.Response.CompanyHelp>().ReverseMap();
            CreateMap<RemoveUserFavouriteTipCommand, RemoveUserFavouriteTipRequest>().ReverseMap();
            CreateMap<AddTipByUserCommand, AddTipByUserRequest>().ReverseMap();
            CreateMap<UpdateTipByUserCommand, UpdateTipByUserRequest>().ReverseMap();
            CreateMap<AddDepartmentTipCommand, AddDepartmentTipRequest>().ReverseMap();
            CreateMap<UpdateDepartmentTipCommand, UpdateDepartmentTipRequest>().ReverseMap();
            CreateMap<Domain.Domain.HelpCategory, Energie.Model.Request.EmployerHelpCategory>();
            CreateMap<Domain.Domain.DepartmentTip, DepartmentEnergyTip>().ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.LikeTips.Count)).ReverseMap();
            //CreateMap<Domain.Domain.DepartmentFavouriteTip, Model.Request.DepartmentEnergyTip>().ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.LikeTips.Count)).ReverseMap(); 
            //CreateMap<Domain.Domain.DepartmentTip.LikeTip, DepartmentEnergyTip>().ForMember(src => src.Count, opt => opt.MapFrom(src => src.CompanyUser.Count)).ReverseMap();
            CreateMap<Domain.Domain.EnergyAnalysis, Model.Response.EnergyAnalysis>().ReverseMap();
            CreateMap<AddDepartmentTipbyUserCommand, AddDepartmentTipbyUserRequest>().ReverseMap();
            CreateMap<UpdateDepartmentTipbyUserCommand, UpdateDepartementTipbyUser>().ReverseMap();
            CreateMap<UpdateEmployerHelpForDeparmentCommand, UpdateEmployerHelpRequest>().ReverseMap();
            CreateMap<RemoveDepartmentFavouriteTipCommand, RemoveDepartmentTipRequest>().ReverseMap();
            CreateMap<CompanyDepartmentHelp, DepartmentEmployerHelp>();
            CreateMap<AddEnergyPlanCommand, EnergyPlanRequest>().ReverseMap();
            CreateMap<AddDepartmentEnergyPlanCommand, DepartmentEnergyPlanRequest>().ReverseMap();
            CreateMap<UpdateEnergyPlanCommand, UpdateEnergyPlanRequest>().ReverseMap();
            CreateMap<UpdateDepartmentEnergyPlanCommand, UpdateDepartmentEnergyPlanRequest>().ReverseMap();
            CreateMap<UpdateCompanyUserLanguageCommand, UpdateCompanyUserLanguageRequest>().ReverseMap();
            CreateMap<AddFeedbackCommand, AddFeedbackRequest>().ReverseMap();
            CreateMap<UpdateUserFeedbackCommand, UpdateUserFeedbackRequest>().ReverseMap(); 


            CreateMap<Domain.Domain.DepartmentFavouriteHelp, Model.Request.DepartmentEnergyTip>().ReverseMap();

            CreateMap<Domain.Domain.UserDepartmentTip, Model.Request.DepartmentEnergyTip>().ReverseMap(); 
        }

    }
}
