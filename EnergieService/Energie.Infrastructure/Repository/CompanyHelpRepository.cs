using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Infrastructure.Repository
{
    public class CompanyHelpRepository : ICompanyHelpRepository
    {
        private readonly AppDbContext _appDbContext;
        public CompanyHelpRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region for personal tip by employer
        public async Task AddCompanyHelpAsync(CompanyHelp companyHelp)
        {
            await _appDbContext.CompanyHelps.AddAsync(companyHelp);
            await _appDbContext.SaveChangesAsync();
        }

        // check where we are using
        public async Task<List<CompanyHelp>> GetCompanyHelpsByCategoryIdAsync(int companyHelpCategoryId)
        {
            
            var companyHelpCategoryList = await _appDbContext.CompanyHelps
                                              .Where(x => x.HelpCategoryId == companyHelpCategoryId)
                                              .Include(x => x.HelpCategory)
                                              .ToListAsync();

            return companyHelpCategoryList;
        }
        public async Task<List<CompanyHelp>> GetCompanyHelpsAsync(int companyId)
        {
            var companyHelp = await _appDbContext.CompanyHelps
                                    .Include(x => x.HelpCategory)
                                    .Where(x => x.CompanyId == companyId)
                                    .ToListAsync();
            return companyHelp;
        }
        public async Task<CompanyHelp> GetCompanyHelpByIdAsync(int id, int companyId)
        {
            return await _appDbContext.CompanyHelps
                         .Where(x=> x.Id == id && x.CompanyId == companyId)
                         .FirstOrDefaultAsync();
        }

        public async Task UpdateCompanyHelpAsync(CompanyHelp companyHelp)
        {
            _appDbContext.CompanyHelps.Update(companyHelp);
            await _appDbContext.SaveChangesAsync();
            
        }

        public async Task DeleteCompanyHelpAsync(int id, int companyId)
        {
            var companyHelp = await _appDbContext.CompanyHelps
                                    .Where(x => x.Id == id && x.CompanyId == companyId)
                                    .FirstOrDefaultAsync();
            _appDbContext.CompanyHelps.Remove(companyHelp);
            await _appDbContext.SaveChangesAsync();
           
        }

        #endregion

        #region for department tip by employer

        public async Task AddDepartmentEmployerHelpAsync(CompanyDepartmentHelp companyDepartmentHelp)
        {
            await _appDbContext.CompanyDepartmentHelp.AddAsync(companyDepartmentHelp);
            await _appDbContext.SaveChangesAsync();
        }
        
        public async Task<CompanyDepartmentHelp> GetEmployerDepartmentHelpbyId(int id)
        {
            return await _appDbContext.CompanyDepartmentHelp.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateEmployerDepartmentHelp(CompanyDepartmentHelp companyDepartmentHelp)
        {
            _appDbContext.CompanyDepartmentHelp.Update(companyDepartmentHelp);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IList<CompanyDepartmentHelp>> DepartmentEmployerHelpListAsync(int companyId)
        {
            return await _appDbContext.CompanyDepartmentHelp
                                      .Where(x => x.Department.CompanyId == companyId)
                                      .Include(x => x.Department)
                                      .Include(x => x.HelpCategory).ToListAsync();
        }

        public async Task DeleteDepartmentEmployerHelpListAsync(int id, int companyId)
        {
            var departmentEmployerHelp = await _appDbContext.CompanyDepartmentHelp
                                            .Where(x => x.Id == id && x.Department.CompanyId == companyId)
                                            .FirstOrDefaultAsync();
            _appDbContext.CompanyDepartmentHelp.Remove(departmentEmployerHelp);
            await _appDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
