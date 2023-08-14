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
    public class AddDepartmentTipCommandHandler : IRequestHandler<AddDepartmentTipCommand, ResponseMessage>
    {
        private readonly ILogger<AddDepartmentTipCommandHandler> _logger;
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddDepartmentTipCommandHandler(IEnergyTipRepository energyTipRepository
            , ILogger<AddDepartmentTipCommandHandler> logger, IStringLocalizer<Resources.Resources> localizer)
        {
            _energyTipRepository =energyTipRepository;
            _logger = logger;
            _localizer =localizer;
        }
        public async Task<ResponseMessage> Handle(AddDepartmentTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMassage = new ResponseMessage();
                var setDepartmentTip = new DepartmentTip().SetDepartMentTip(
                    request.DepartmentTip,
                    request.Description,
                    request.EnergyAnalysisQuestionId);
                await _energyTipRepository.AddDepartmentTipAsync(setDepartmentTip);
                responseMassage.IsSuccess = true;
                //responseMassage.Message = _localizer["Afdeling met"].Value + " " + setDepartmentTip.ID +" "+ _localizer["Id toegevoegd"].Value;
                responseMassage.Message = _localizer["Department_added", setDepartmentTip.ID].Value;
                responseMassage.Id = setDepartmentTip.ID;
                return responseMassage;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(AddDepartmentTipCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddDepartmentTipCommandHandler));
                throw;
            }
            
        }
    }
}
