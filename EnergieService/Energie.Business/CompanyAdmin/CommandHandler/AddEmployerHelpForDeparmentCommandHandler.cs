using Energie.Business.CompanyAdmin.Command;
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

namespace Energie.Business.CompanyAdmin.CommandHandler
{
    public class AddEmployerHelpForDeparmentCommandHandler : IRequestHandler<AddEmployerHelpForDeparmentCommand, ResponseMessage>
    {
        private readonly ICompanyHelpRepository _companyHelpRepository;
        private readonly ILogger<AddEmployerHelpForDeparmentCommandHandler> _logger;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddEmployerHelpForDeparmentCommandHandler(ICompanyHelpRepository companyHelpRepository
            , ILogger<AddEmployerHelpForDeparmentCommandHandler> logger, IStringLocalizer<Resources.Resources> localizer)
        {
            _companyHelpRepository = companyHelpRepository;
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddEmployerHelpForDeparmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                var employerHelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp(request.Name, request.Description
                    , request.Contribution
                    , request.Requestvia
                    , request.MoreInformation
                    , request.DepartmentId
                    , request.HelpCategoryId);
                await _companyHelpRepository.AddDepartmentEmployerHelpAsync(employerHelp);
                response.IsSuccess = true;
                //response.Message = _localizer["Hulp van de werkgever toegevoegd met"].Value+" "+ employerHelp.Id + " "+_localizer["dit identiteitsbewijs"].Value;
                response.Message = _localizer["Added_help_from_the_employer_with_employerHelpID"].Value;
                response.Id = employerHelp.Id;
                return response;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"error occured in {{0}}", nameof(AddEmployerHelpForDeparmentCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddEmployerHelpForDeparmentCommandHandler));
                throw;
            }
        }
    }
}
