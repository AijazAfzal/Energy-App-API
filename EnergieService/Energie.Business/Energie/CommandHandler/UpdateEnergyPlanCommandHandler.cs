using Energie.Business.Energie.Command;
using Energie.Domain.ApplicationEnum;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class UpdateEnergyPlanCommandHandler : IRequestHandler<UpdateEnergyPlanCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateEnergyPlanCommandHandler> _logger;
        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;

        public UpdateEnergyPlanCommandHandler(ILogger<UpdateEnergyPlanCommandHandler> logger
                                             , IEnergyPlanRepository energyPlanRepository, IStringLocalizer<Resources.Resources> localizer)

        {
            _logger = logger;
            _energyPlanRepository = energyPlanRepository;
            _localizer = localizer;

        }
        public async Task<ResponseMessage> Handle(UpdateEnergyPlanCommand request, CancellationToken cancellationToken)
        {
            try

            {
                var responsemessage=new ResponseMessage();
                var energyplan = await _energyPlanRepository.GetEnergyPlanbyIdAsync(request.Id,request.UserEmail);  
                Enum.TryParse(request.Status, out PlanStatusEnum statusEnum);
                if (energyplan != null) 
                {
                    var setenergyplan = energyplan.UpdateEnergyPlan(request.EndDate, (int)statusEnum, request.IsReminder);
                    await _energyPlanRepository.UpdateEnergyPlanAsync(setenergyplan);
                    responsemessage.Message = _localizer["Energy_plan_updated"].Value;
                    responsemessage.IsSuccess = true; 
                    responsemessage.Id = request.Id;
                    return responsemessage;

                }
                else
                {
                    return default;  
                }
            }

            catch (Exception ex)
            {             
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(UpdateEnergyPlanCommandHandler));
                throw;
            }


        }
    }
}
