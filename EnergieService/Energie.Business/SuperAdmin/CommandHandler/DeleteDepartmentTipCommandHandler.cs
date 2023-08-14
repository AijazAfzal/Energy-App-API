using Energie.Business.SuperAdmin.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.SuperAdmin.CommandHandler
{
    public class DeleteDepartmentTipCommandHandler : IRequestHandler<DeleteDepartmentTipCommand, ResponseMessage>
    {
        private readonly ILogger<DeleteDepartmentTipCommandHandler> _logger;
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public DeleteDepartmentTipCommandHandler(ILogger<DeleteDepartmentTipCommandHandler> logger
            , IEnergyTipRepository energyTipRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _energyTipRepository = energyTipRepository;
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(DeleteDepartmentTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMassage = new ResponseMessage();
                await _energyTipRepository.DeleteDepartmentTipAsync(request.Id);
                responseMassage.IsSuccess = true;
                responseMassage.Id = request.Id;
                //responseMassage.Message = _localizer["Afdeling met"].Value + " " + request.Id + " "+_localizer["id verwijderd"].Value;
                responseMassage.Message = _localizer["Department_tip_removed"].Value;
                return responseMassage;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(DeleteDepartmentTipCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(DeleteDepartmentTipCommandHandler));
                throw;
            }
        }
    }
}
