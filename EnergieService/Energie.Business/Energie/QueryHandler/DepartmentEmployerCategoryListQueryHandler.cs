using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentEmployerCategoryListQueryHandler : IRequestHandler<DepartmentEmployerCategoryListQuery, CompanyCategoryUserList>
    {
        private readonly ICompanyHelpCategoryRepository _companyHelpCategoryRepository;
        private readonly ILogger<DepartmentEmployerCategoryListQueryHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        public DepartmentEmployerCategoryListQueryHandler(ICompanyHelpCategoryRepository companyHelpCategoryRepository
            , ILogger<DepartmentEmployerCategoryListQueryHandler> logger,
ICompanyUserRepository companyUserRepository)
        {
            _companyHelpCategoryRepository = companyHelpCategoryRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
        }

        public async Task<CompanyCategoryUserList> Handle(DepartmentEmployerCategoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employerHelpList = await _companyHelpCategoryRepository.GetEmployerHelpCategoriesAsync();
                var list = employerHelpList.Select(x => new CompanyCategory
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();
                return new CompanyCategoryUserList { CompanyCategories = list };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"error occured in {{0}}", nameof(DepartmentEmployerCategoryListQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(DepartmentEmployerCategoryListQueryHandler)); 
                throw;
            }
        }
    }
}
