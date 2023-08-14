using Energie.Business.Energie.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class DeleteDepartmentEnergyCommandHandler : IRequestHandler<DeleteDepartmentEnergyCommand, ResponseMessage>
    {
        private readonly ILogger<DeleteDepartmentEnergyCommandHandler> _logger;
        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public DeleteDepartmentEnergyCommandHandler(ILogger<DeleteDepartmentEnergyCommandHandler> logger,
            IEnergyPlanRepository energyPlanRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _energyPlanRepository = energyPlanRepository;
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(DeleteDepartmentEnergyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                await _energyPlanRepository.DeleteDepartmentEnergyPlanAsync(request.Id, request.UserEmail);
                response.IsSuccess = true;
                response.Id = request.Id;
                response.Message = _localizer["Subscription_removed"].Value    ;  
                return response;
            }
            catch (Exception ex)
            {              
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(DeleteDepartmentEnergyCommandHandler)); 
                throw;
            }
        }
    }
}
