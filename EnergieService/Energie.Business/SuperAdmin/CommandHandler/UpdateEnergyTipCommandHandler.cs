using Energie.Business.SuperAdmin.Command;
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
    public class UpdateEnergyTipCommandHandler : IRequestHandler<UpdateEnergyTipCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateEnergyTipCommandHandler> _logger;
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateEnergyTipCommandHandler(IEnergyTipRepository energyTipRepository
            , ILogger<UpdateEnergyTipCommandHandler> logger, IStringLocalizer<Resources.Resources> localizer)
        {
            _energyTipRepository = energyTipRepository;
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(UpdateEnergyTipCommand request, CancellationToken cancellationToken)
        {
            var responseMessage = new ResponseMessage();
            var tip = await _energyTipRepository.GetEnergyTipByIdAsync(request.Id);
            if (tip == null)
            {
                throw new NullReferenceException();
                
            }
            var updateTip = tip.UpdateEnergyTip(request.CategoryId, request.Name, request.Description);
            await _energyTipRepository.UpdateEnergyTipAsync(updateTip);
            responseMessage.Id = updateTip.Id;
            responseMessage.IsSuccess = true;
            responseMessage.Message = _localizer["Tip_Update"].Value;
            return responseMessage;

        }
    }
}
