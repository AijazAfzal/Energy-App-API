using Energie.Business.CompanyAdmin.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.CompanyAdmin.CommandHandler
{
    public class UpdateEmployerHelpForDeparmentCommandHandler : IRequestHandler<UpdateEmployerHelpForDeparmentCommand, ResponseMessage>
    {
        private readonly ICompanyHelpRepository _companyHelpRepository;
        private readonly ILogger<UpdateEmployerHelpForDeparmentCommandHandler> _logger;
        private readonly IStringLocalizer<Resources.Resources> _localizer;

        public UpdateEmployerHelpForDeparmentCommandHandler(ICompanyHelpRepository companyHelpRepository
            , ILogger<UpdateEmployerHelpForDeparmentCommandHandler> logger, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyHelpRepository = companyHelpRepository;
            _localizer = localizer;

        }
        public async Task<ResponseMessage> Handle(UpdateEmployerHelpForDeparmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var employerhelptobeupdated = await _companyHelpRepository.GetEmployerDepartmentHelpbyId(request.Id);
                if (employerhelptobeupdated == null)
                    return default;
                var updateemployerhelp = employerhelptobeupdated.UpdateCompanyDepartmentHelp
                                                                   (request.Name,
                                                                   request.Description,
                                                                   request.Contribution,
                                                                   request.Requestvia,
                                                                   request.MoreInformation,
                                                                   request.DepartmentId,
                                                                   request.HelpCategoryId
                                                                   );
                await _companyHelpRepository.UpdateEmployerDepartmentHelp(updateemployerhelp);
                responseMessage.IsSuccess = true;
                // responseMessage.Message = _localizer["Werkgever helpt met id"].Value+" "+ updateemployerhelp.Id +" "+ _localizer["upbijgewerktdated"].Value;
                responseMessage.Message = _localizer["Employer_helps_with_id_update", updateemployerhelp.Id].Value;
                responseMessage.Id = updateemployerhelp.Id;
                return responseMessage;
            }

            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(UpdateEmployerHelpForDeparmentCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(UpdateEmployerHelpForDeparmentCommandHandler));
                throw;
            }
        }
    }
}
