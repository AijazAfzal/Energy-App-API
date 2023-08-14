using Energie.Business.CompanyAdmin.Command;
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
    public class UpdateCompanyHelpCommandHandler : IRequestHandler<UpdateCompanyHelpCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateCompanyHelpCommandHandler> _logger;
        private readonly ICompanyHelpRepository _companyHelpTipRepository;
        private readonly ICompanyAdminRepository _addCompanyAdminRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateCompanyHelpCommandHandler(
            ILogger<UpdateCompanyHelpCommandHandler> logger,
            ICompanyHelpRepository companyHelpTipRepository,
            ICompanyAdminRepository addCompanyAdminRepository,
            IStringLocalizer<Resources.Resources> localizer
            )
        {
            _companyHelpTipRepository = companyHelpTipRepository;
            _logger = logger;
            _addCompanyAdminRepository= addCompanyAdminRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(UpdateCompanyHelpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var companyAdmin = await _addCompanyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                var companyHelp = await _companyHelpTipRepository.GetCompanyHelpByIdAsync(request.Id, (int)companyAdmin.CompanyId);
                if (companyHelp == null)
                    return default;

                var updatecompanyCategory = companyHelp.UpdateCompanyHelp(
                                            request.Name, 
                                            request.Description, 
                                            request.OwnContribution,
                                            request.Conditions,
                                            request.Requestvia,
                                            request.CompanyHelpCategoryId);
                await _companyHelpTipRepository.UpdateCompanyHelpAsync(updatecompanyCategory);
                responseMessage.IsSuccess = true;
                //responseMessage.Message = _localizer["Bedrijfstips met id:"].Value+" "+ updatecompanyCategory.Id + _localizer["bijgewerkt"].Value;
                responseMessage.Message = _localizer["Business_tips_with_ID_Update",updatecompanyCategory.Id].Value;
                responseMessage.Id = updatecompanyCategory.Id;
                return responseMessage;
                
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(UpdateCompanyHelpCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(UpdateCompanyHelpCommandHandler));
                throw;
            }
            
        }
    }
}
