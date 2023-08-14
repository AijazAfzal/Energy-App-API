using Energie.Business.CompanyAdmin.Query;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.CompanyAdmin.QueryHandler
{
    public class DepartmentEmployerHelpListQueryHandler : IRequestHandler<DepartmentEmployerHelpListQuery, DepartmentEmployerHelpList>
    {
        private readonly ICompanyHelpRepository _companyHelpRepository;
        private readonly ICompanyAdminRepository _companyAdminRepository;
        private readonly ILogger<DepartmentEmployerHelpListQueryHandler> _logger;
        public DepartmentEmployerHelpListQueryHandler(ICompanyHelpRepository companyHelpRepository
            , ICompanyAdminRepository addCompanyAdminRepository,
            ILogger<DepartmentEmployerHelpListQueryHandler> logger)
        {
            _companyHelpRepository = companyHelpRepository;
            _companyAdminRepository = addCompanyAdminRepository;
            _logger = logger;
        }
        public async Task<DepartmentEmployerHelpList> Handle(DepartmentEmployerHelpListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var companyAdmin = await _companyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                var employerHelpList = await _companyHelpRepository.DepartmentEmployerHelpListAsync((int)companyAdmin.CompanyId);

                var lists = employerHelpList.Select
                                   (x => new DepartmentEmployerHelp
                                   {
                                       Id = x.Id,
                                       Name = x.Name,
                                       Description = x.Description,
                                       Contribution = x.Contribution,
                                       Requestvia = x.Requestvia,
                                       MoreInformation = x.MoreInformation,
                                       DepartmentId = x.DepartmentId,
                                       DepartmentName = x.Department.Name,
                                       HelpCategoryId = x.HelpCategoryId,
                                       HelpCategoryName = x.HelpCategory.Name,
                                   }).ToList();
                return new DepartmentEmployerHelpList { DepartmentEmployerHelps = lists };

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(DepartmentEmployerHelpListQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(DepartmentEmployerHelpListQueryHandler));
                throw;
            }
        }
    }
}
