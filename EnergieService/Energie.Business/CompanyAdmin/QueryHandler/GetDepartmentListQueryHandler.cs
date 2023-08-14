using AutoMapper;
using Energie.Business.CompanyAdmin.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.CompanyAdmin.QueryHandler
{
    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, List<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<GetDepartmentListQueryHandler> _logger;
        private readonly ICompanyAdminRepository _addCompanyAdminRepository;
        public GetDepartmentListQueryHandler(
            IDepartmentRepository departmentRepository
            , ILogger<GetDepartmentListQueryHandler> logger
            , ICompanyAdminRepository addCompanyAdminRepository
           )
        {
            _departmentRepository = departmentRepository;
            _addCompanyAdminRepository = addCompanyAdminRepository;
            _logger = logger;
        }

        public async Task<List<Department>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _addCompanyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                if (company == null)
                    return null;

                var department = await _departmentRepository.GetDepartmentListAsync((int)company.CompanyId);
                return department;
                
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}");
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}");
                throw;
            }
        }
    }
}
