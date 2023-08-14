using Energie.Business.CompanyAdmin.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.CompanyAdmin.CommandHandler
{
    public class AddCompanyHelpCommandHandler : IRequestHandler<AddCompanyHelpCommand, ResponseMessage>
    {
        private readonly ILogger<AddCompanyHelpCommandHandler> _logger;
        private readonly ICompanyHelpRepository _companyHelpTipRepository;
        private readonly ICompanyAdminRepository _addCompanyAdminRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddCompanyHelpCommandHandler(ILogger<AddCompanyHelpCommandHandler> logger
            , ICompanyHelpRepository companyHelpTipRepository
            , ICompanyAdminRepository addCompanyAdminRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyHelpTipRepository = companyHelpTipRepository;
            _addCompanyAdminRepository= addCompanyAdminRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddCompanyHelpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                var companyAdmin = await _addCompanyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                var setCompanyHelpTip = new CompanyHelp().SetCompanyHelp(
                                                                            request.Name,
                                                                            request.Description,
                                                                            request.OwnContribution,
                                                                            request.Conditions,
                                                                            request.Requestvia,
                                                                            request.CompanyHelpCategoryId,
                                                                            (int)companyAdmin.CompanyId
                                                                         );
                await _companyHelpTipRepository.AddCompanyHelpAsync(setCompanyHelpTip);
                response.IsSuccess = true;
                //response.Message = _localizer["Hulptip van het bedrijf met deze id"].Value+" "+ setCompanyHelpTip.Id +" "+_localizer["is toegevoegd"].Value;
                response.Message = _localizer["Help_tip_from_the_company_with_ID", setCompanyHelpTip.Id].Value;
                response.Id = setCompanyHelpTip.Id;
                return response;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(AddCompanyHelpCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddCompanyHelpCommandHandler));
                throw;
            }
        }
    }
}
