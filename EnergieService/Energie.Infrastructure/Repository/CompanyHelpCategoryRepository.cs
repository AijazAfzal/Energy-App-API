using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Energie.Infrastructure.Repository
{
    public class CompanyHelpCategoryRepository : ICompanyHelpCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CompanyHelpCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CompanyHelpCategory>> GetCompanyCategoryHelpByCompanyID(int companyId)
        {
            var companyHelpCategory = await _appDbContext.CompanyHelpCategorys
                                            .Where(x => x.CompanyId == companyId)
                                            .Include(x => x.Company).ToListAsync();
            return companyHelpCategory;
        }

        #region Created Employer Help Category And Employer Help for Department

        public async Task<IList<HelpCategory>> GetEmployerHelpCategoriesAsync()
        {
            return await _appDbContext.HelpCategory.ToListAsync();
        }      
        public async Task<IList<CompanyDepartmentHelp>> EmployerHelpByDepartmentIdAsync(int Id, int categoryId)
        {
            var test = await _appDbContext.CompanyDepartmentHelp
                            .Where(x => x.DepartmentId == Id && x.HelpCategoryId == categoryId)
                            .Include(x => x.Department)
                            .Include(x => x.HelpCategory)
                            .ToListAsync();
            return test;
        }
        public async Task AddDepartmentFavouriteHelpAsync(DepartmentFavouriteHelp departmentFavouriteHelp)
        {
            await _appDbContext.DepartmentFavouriteHelps.AddAsync(departmentFavouriteHelp);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<DepartmentFavouriteHelp> GetDepartmentFavouriteForUserAsync(int departmentHelpId, int userId)
        {
            return await _appDbContext.DepartmentFavouriteHelps
                        .Where(x => x.CompanyDepartmentHelpId == departmentHelpId && x.CompanyUserId == userId)
                        .FirstOrDefaultAsync();
        }
        public async Task DeleteDepartmentFavouriteForUserAsync(int departmentHelpId, int userId)
        {
            var departmentUserHelp = await _appDbContext.DepartmentFavouriteHelps
                        .Where(x => x.Id == departmentHelpId && x.CompanyUserId == userId)
                        .FirstOrDefaultAsync();
            _appDbContext.DepartmentFavouriteHelps.Remove(departmentUserHelp);
            await _appDbContext.SaveChangesAsync();
        }


        #endregion
    }
}
