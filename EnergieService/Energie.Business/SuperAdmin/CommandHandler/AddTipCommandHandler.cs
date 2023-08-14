using Energie.Business.SuperAdmin.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.CommandHandler
{
    public class AddTipCommandHandler : IRequestHandler<AddTipCommand, ResponseMessage>
    {
        private readonly ILogger<AddTipCommandHandler> _logger;
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddTipCommandHandler(ILogger<AddTipCommandHandler> logger
            , IEnergyTipRepository energyTipRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _energyTipRepository = energyTipRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var setEnrgyTip = new Tip().SetEnergyTip(request.EnergyAnalysisQuestionId, request.Tip, request.Description);
                await _energyTipRepository.AddEnergyTipAsync(setEnrgyTip);
                var responseMessage = new ResponseMessage();
                responseMessage.Id = setEnrgyTip.Id;
                responseMessage.IsSuccess = true;
                //responseMessage.Message = _localizer["Tip met dit id"].Value + " " + setEnrgyTip.Id +" "+ _localizer["toegevoegd"].Value;
                responseMessage.Message = _localizer["Tip_added", setEnrgyTip.Id].Value;
                return responseMessage;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"error occured in {{0}}", nameof(AddTipCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddTipCommandHandler));
                throw;
            }
            
        }
    }
}
