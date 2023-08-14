using Energie.Business.Energie.Command;
using Energie.Domain.ApplicationEnum;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class UpdateDepartmentEnergyPlanCommandHandler : IRequestHandler<UpdateDepartmentEnergyPlanCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateDepartmentEnergyPlanCommandHandler> _logger;
        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateDepartmentEnergyPlanCommandHandler(ILogger<UpdateDepartmentEnergyPlanCommandHandler> logger
            , IEnergyPlanRepository energyPlanRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _energyPlanRepository = energyPlanRepository;
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(UpdateDepartmentEnergyPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                var departmentTip = await _energyPlanRepository.GetDepartmentEnergyPlanByIdAsync(request.Id, request.UserEmail);
                if (departmentTip == null)
                {
                    return default;
                }
                Enum.TryParse(request.PlanStatus, out PlanStatusEnum planStatusEnum);
                departmentTip.UpdateDepartmentPlan((int)planStatusEnum, request.IsReminder, request.EndDate);
                await _energyPlanRepository.UpdateDepartmentEnergyPlanAsync(departmentTip);
                response.IsSuccess = true;
                response.Message = _localizer["Plan_updated"].Value; 
                response.Id = request.Id;
                return response;
            }
            catch (Exception ex)
            {            
                _logger.LogError(ex, $"fout opgetreden in{{0}}", nameof(UpdateDepartmentEnergyPlanCommandHandler)); 
                throw;
            }
        }
    }
}
