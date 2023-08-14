using Energie.Business.CompanyAdmin.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.CompanyAdmin.CommandHandler
{
    public class DeleteDepartmentEmployerHelpCommandHandler : IRequestHandler<DeleteDepartmentEmployerHelpCommand, ResponseMessage>
    {
        private readonly ICompanyHelpRepository _companyHelpRepository;
        private readonly ILogger<DeleteDepartmentEmployerHelpCommandHandler> _logger;
        private readonly ICompanyAdminRepository _companyAdminRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public DeleteDepartmentEmployerHelpCommandHandler(ICompanyHelpRepository companyHelpRepository
            , ILogger<DeleteDepartmentEmployerHelpCommandHandler> logger
            , ICompanyAdminRepository companyAdminRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _companyHelpRepository = companyHelpRepository;
            _logger = logger;
            _companyAdminRepository = companyAdminRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(DeleteDepartmentEmployerHelpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                var companyAdmin = await _companyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                await _companyHelpRepository.DeleteDepartmentEmployerHelpListAsync(request.Id, (int)companyAdmin.CompanyId);
                response.IsSuccess = true;
                response.Id = request.Id;
                response.Message = _localizer["Help_removed"].Value;
                return response;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"error occured in {{0}}", nameof(DeleteDepartmentEmployerHelpCommandHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(DeleteDepartmentEmployerHelpCommandHandler));
                throw;
            }
        }
    }
}
