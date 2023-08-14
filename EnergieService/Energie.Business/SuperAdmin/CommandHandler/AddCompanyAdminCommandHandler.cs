using Energie.Business.SuperAdmin.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.SuperAdmin.CommandHandler
{
    public class AddCompanyAdminCommandHandler : IRequestHandler<AddCompanyAdminCommand, ResponseMessage>
    {
        private readonly ILogger<AddCompanyAdminCommandHandler> _logger;
        private readonly ICompanyAdminRepository _addCompanyAdminRepository;
        private readonly ICompanyRepository<Company> _companyRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddCompanyAdminCommandHandler(
            ILogger<AddCompanyAdminCommandHandler> logger
            , ICompanyAdminRepository addCompanyAdminRepository
            , ICompanyRepository<Company> companyRepository
            , IStringLocalizer<Resources.Resources> localizer
            )
        {
            _logger = logger;
            _addCompanyAdminRepository = addCompanyAdminRepository;
            _companyRepository = companyRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddCompanyAdminCommand request, CancellationToken cancellationToken)
        {
            var responseMessage = new ResponseMessage();
            try
            {
                var company = await _companyRepository.GetCompanyByID(request.CompanyId);
                if (company != null)
                {

                    var setCompanyAdmin = new AddB2CCompanyAdmin().SetCompanyAdmin(request.CompanyId, company.Name, request.UserName, request.Email);
                    await _addCompanyAdminRepository.CreateCompanyAdmin(setCompanyAdmin);
                    responseMessage.IsSuccess = true;
                    responseMessage.Message = _localizer["Company_administrator_added"].Value;
                    return responseMessage;
                }

               
                    responseMessage.IsSuccess = false;
                    responseMessage.Message = _localizer["Company_administrator_already_exist"].Value;
                    return responseMessage;  
               
              
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(AddCompanyAdminCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddCompanyAdminCommandHandler)); 
                throw;
            }

        }
    }
}
