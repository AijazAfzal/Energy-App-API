using Energie.Business.SuperAdmin.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Energie.Business.SuperAdmin.CommandHandler
{
    public class UpdateDepartmentTipCommandHandler : IRequestHandler<UpdateDepartmentTipCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateDepartmentTipCommandHandler> _logger;
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateDepartmentTipCommandHandler(ILogger<UpdateDepartmentTipCommandHandler> logger
            , IEnergyTipRepository energyTipRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger= logger;
            _energyTipRepository = energyTipRepository;
            _localizer= localizer;

        }
        public async Task<ResponseMessage> Handle(UpdateDepartmentTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var department = await _energyTipRepository.GetDepatrmentTipByIdAsync(request.Id);
                var updateDepartment = department.UpdateDepartMentTip
                                            ( 
                                                request.DepartmentTip, 
                                                request.Description, 
                                                request.EnergyAnalysisQuestionId
                                            );
                await _energyTipRepository.UpdateDepartmentTipAsync(updateDepartment);
                responseMessage.IsSuccess = true;
                //responseMessage.Message = _localizer["Fooi hierbij"].Value+" "+ updateDepartment.ID + _localizer["Id geüpdatet"].Value;
                responseMessage.Message = _localizer["Update_department_tip", updateDepartment.ID].Value;
                responseMessage.Id = updateDepartment.ID;
                return responseMessage;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(UpdateDepartmentTipCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(UpdateDepartmentTipCommandHandler));
                throw;
            }
        }
    }
}
