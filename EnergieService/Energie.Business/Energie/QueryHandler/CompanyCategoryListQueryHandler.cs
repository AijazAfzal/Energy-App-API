using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class CompanyCategoryListQueryHandler : IRequestHandler<CompanyCategoryListQuery, List<CompanyHelpCategory>>
    {
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ILogger<CompanyCategoryListQueryHandler> _logger;
        private readonly ICompanyHelpCategoryRepository _companyHelpCategoryRepository;
        public CompanyCategoryListQueryHandler(ICompanyUserRepository companyUserRepository
            , ILogger<CompanyCategoryListQueryHandler> logger
            , ICompanyHelpCategoryRepository companyHelpCategoryRepository)
        {
            _companyUserRepository = companyUserRepository;
            _logger = logger;
            _companyHelpCategoryRepository= companyHelpCategoryRepository;
        }
        public async Task<List<CompanyHelpCategory>> Handle(CompanyCategoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var companyCategoryHelp = await _companyHelpCategoryRepository.GetCompanyCategoryHelpByCompanyID(user.Department.Company.Id);
                return companyCategoryHelp; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}");
                throw ex;
            }
        }
    }
}
