using Energie.Business.Energie.Command;
using Energie.Domain.ApplicationEnum;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddDepartmentEnergyPlanCommandHandler : IRequestHandler<AddDepartmentEnergyPlanCommand, ResponseMessage>
    {
        private readonly ILogger<AddDepartmentEnergyPlanCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly IDepartmentTipRepository _departmentTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddDepartmentEnergyPlanCommandHandler(IEnergyPlanRepository energyPlanRepository
                                                     , ICompanyUserRepository companyUserRepository
                                                     , ILogger<AddDepartmentEnergyPlanCommandHandler> logger
                                                     , IDepartmentTipRepository departmentTipRepository
                                                     , IStringLocalizer<Resources.Resources> localizer)
        {
            _energyPlanRepository = energyPlanRepository;
            _companyUserRepository = companyUserRepository;
            _logger = logger;
            _departmentTipRepository = departmentTipRepository;
            _localizer = localizer; 

        }
        public async Task<ResponseMessage> Handle(AddDepartmentEnergyPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responsemessage = new ResponseMessage();
                var companyuser = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                int i = 0;
                if (request.TipBy == TipTypeEnum.SuperAdmin.ToString())
                {
                    i = await _departmentTipRepository
                              .UserFavouriteDepartmenTiptForPlanAsync((int)companyuser.DepartmentID, request.FavouriteTipId); 
                }
                else if (request.TipBy == TipTypeEnum.CompanyAdmin.ToString())
                {
                    i = await _departmentTipRepository
                              .DeparmentFavouriteHelpAsync((int)companyuser.DepartmentID, request.FavouriteTipId);
                }
                else
                {
                    i = await _departmentTipRepository
                              .UserDepartmentFavouriteTip((int)companyuser.DepartmentID, request.FavouriteTipId);
                }

                Enum.TryParse(request.TipBy, out TipTypeEnum tipByEnum);

                var energyplan = await _energyPlanRepository
                                       .GetDepartmentEnergyPlanAsync((int)companyuser.Id, request.FavouriteTipId, (int)tipByEnum);

                if (energyplan != null)
                {
                    responsemessage.Id = request.FavouriteTipId;
                    responsemessage.Message = _localizer["Energy_plan_exists"].Value;
                    return responsemessage;

                }
                var setenergyplanfordepartment = new DepartmentEnergyPlan()
                                                                           .SetDepartmentEnergyPlan( request.FavouriteTipId 
                                                                           , (int)tipByEnum, (int)companyuser.Id 
                                                                           , request.ResponsiblePersonId, request.EndDate
                                                                           , request.IsReminder);
                await _energyPlanRepository.AddDepartmentEnergyPlanAsync(setenergyplanfordepartment);
                responsemessage.Id = setenergyplanfordepartment.Id;
                responsemessage.Message = _localizer["Energy_department_plan_added"].Value;
                responsemessage.IsSuccess = true;
                return responsemessage;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddDepartmentEnergyPlanCommandHandler));
                throw; 
            }

        }
    }
}
