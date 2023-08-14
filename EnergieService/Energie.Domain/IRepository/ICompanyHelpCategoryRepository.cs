using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ICompanyHelpCategoryRepository
    {
        Task<List<CompanyHelpCategory>> GetCompanyCategoryHelpByCompanyID(int companyId);
        Task<IList<HelpCategory>> GetEmployerHelpCategoriesAsync();
        Task<IList<CompanyDepartmentHelp>> EmployerHelpByDepartmentIdAsync(int Id, int categoryId);
        Task AddDepartmentFavouriteHelpAsync(DepartmentFavouriteHelp departmentFavouriteHelp);
        Task<DepartmentFavouriteHelp> GetDepartmentFavouriteForUserAsync(int departmentHelpId, int userId);
        Task DeleteDepartmentFavouriteForUserAsync(int departmentHelpId, int userId);
    }
}
