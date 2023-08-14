using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ICompanyHelpRepository
    {
        // Employer help for personal added by employer 
        Task AddCompanyHelpAsync(CompanyHelp companyHelp);
        Task<List<CompanyHelp>> GetCompanyHelpsByCategoryIdAsync(int companyHelpCategoryId);
        Task<List<CompanyHelp>> GetCompanyHelpsAsync(int companyId);
        Task<CompanyHelp> GetCompanyHelpByIdAsync(int id, int companyId);
        Task UpdateCompanyHelpAsync(CompanyHelp companyHelp);
        Task DeleteCompanyHelpAsync(int id, int companyId);

        // Employer help for department added by employer 

        Task AddDepartmentEmployerHelpAsync(CompanyDepartmentHelp companyDepartmentHelp);
        Task<CompanyDepartmentHelp> GetEmployerDepartmentHelpbyId(int id);
        Task UpdateEmployerDepartmentHelp(CompanyDepartmentHelp companyDepartmentHelp);
        Task<IList<CompanyDepartmentHelp>> DepartmentEmployerHelpListAsync(int companyId);
        Task DeleteDepartmentEmployerHelpListAsync(int id, int companyId);
    }
}
