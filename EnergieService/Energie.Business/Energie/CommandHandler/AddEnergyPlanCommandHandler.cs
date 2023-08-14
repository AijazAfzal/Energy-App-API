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
    public class AddEnergyPlanCommandHandler : IRequestHandler<AddEnergyPlanCommand, ResponseMessage>
    {
        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly ILogger<AddEnergyPlanCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddEnergyPlanCommandHandler(IEnergyPlanRepository energyPlanRepository
            , ILogger<AddEnergyPlanCommandHandler> logger
            , ICompanyUserRepository companyUserRepository
            , IUserEnergyTipRepository userEnergyTipRepository
            , IStringLocalizer<Resources.Resources> localizer
           )
        {
            _energyPlanRepository = energyPlanRepository;
            _companyUserRepository = companyUserRepository;
            _logger = logger;
            _userEnergyTipRepository = userEnergyTipRepository;
            _localizer = localizer;
            
        }
        public async Task<ResponseMessage> Handle(AddEnergyPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                int i = 0;
                if (request.TipBy == TipTypeEnum.SuperAdmin.ToString())
                {
                    i = await _userEnergyTipRepository.GetUserFavouriteTipById(request.FavouriteTipId, user.Id);
                }
                else if (request.TipBy == TipTypeEnum.CompanyAdmin.ToString())
                {
                    i = await _userEnergyTipRepository.UserFavouriteHelpByIdAsync(request.FavouriteTipId, user.Id);
                }
                else
                {
                    i = await _userEnergyTipRepository.UserFavouriteTipByIdAsync(request.FavouriteTipId, user.Id);
                }
                Enum.TryParse(request.TipBy, out TipTypeEnum tipByEnum);

                var plans = await _energyPlanRepository.GetEnergyPlanForUserAsync(user.Id, request.FavouriteTipId, (int)tipByEnum);
                if (plans != null)
                {
                    response.Id = request.FavouriteTipId;
                    response.Message = _localizer["Energy_plan_exists"].Value;
                }
                else if (i == request.FavouriteTipId)
                {
                    var setenergyPlan = new EnergyPlan().SetEnergyPlan(request.FavouriteTipId
                    , (int)tipByEnum
                    , user.Id
                    , request.ResponsiblePersonId
                    , request.EndDate
                    , request.IsReminder);
                    await _energyPlanRepository.AddEnergyPlanAsync(setenergyPlan);
                    response.Id = setenergyPlan.Id;
                    response.IsSuccess = true;
                    response.Message = _localizer["Energy_plan_added"].Value; 
                }
                else
                {
                    response.Id = request.FavouriteTipId;
                    response.Message = _localizer["favorite_tip_not_exist"].Value;
                }
                return response;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"error occured in {{0}}", nameof(AddEnergyPlanCommandHandler));
                throw;
            }
        }
    }
}
