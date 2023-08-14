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
    public class DeleteEnergyTipCommandHandler: IRequestHandler<DeleteEnergyTipCommand, ResponseMessage>
    {
        private readonly ILogger<DeleteEnergyTipCommandHandler> _logger;
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public DeleteEnergyTipCommandHandler(IEnergyTipRepository energyTipRepository
            , ILogger<DeleteEnergyTipCommandHandler> logger, IStringLocalizer<Resources.Resources> localizer)
        {
            _energyTipRepository = energyTipRepository;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<ResponseMessage> Handle(DeleteEnergyTipCommand request, CancellationToken cancellationToken)
        {
           
            try
            {
                var response = new ResponseMessage();
                var deletedtip =  await _energyTipRepository.DeleteEnergyTipAsync(request.TipId);
                response.IsSuccess = true;
                response.Id = request.TipId;
                response.Message = _localizer["Tip_removed"].Value ;
                return response;
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Error occured in {{0}}", ex.Message);
                _logger.LogError($"Er is een fout opgetreden in {{0}}", ex.Message);
                throw;
            }
        }
    }
}
