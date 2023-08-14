using Energie.Business.Energie.Command;using Energie.Domain.IRepository;using Energie.Model;using MediatR;using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;namespace Energie.Business.Energie.CommandHandler{    public class RemoveEnergyPlanCommandHandler : IRequestHandler<RemoveEnergyPlanCommand, ResponseMessage>    {        private readonly ILogger<RemoveEnergyPlanCommandHandler> _logger;        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public RemoveEnergyPlanCommandHandler(ILogger<RemoveEnergyPlanCommandHandler> logger, IEnergyPlanRepository energyPlanRepository, IStringLocalizer<Resources.Resources> localizer)
        {            _logger = logger;            _energyPlanRepository = energyPlanRepository;
            _localizer = localizer;

        }        public async Task<ResponseMessage> Handle(RemoveEnergyPlanCommand request, CancellationToken cancellationToken)        {            try            {                var responsemessage = new ResponseMessage();                await _energyPlanRepository.DeleteEnergyPlanAsync(request.EnergyPlanId);
                responsemessage.Message = _localizer["Energy_plan_removed"].Value;
                responsemessage.Id = request.EnergyPlanId;                responsemessage.IsSuccess = true;
                return responsemessage;


            }            catch (Exception Ex)            {            
                _logger.LogError(Ex, $"Er is een fout opgetreden in {{0}}", nameof(RemoveEnergyPlanCommandHandler));                  throw;

            }

        }    }}