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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public DepartmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Department>> GetDepartmentListAsync(int companyid)
        {
            var user = await _appDbContext.CompanyUser.Where(x => x.Department.CompanyId == companyid).ToListAsync();
            var departmentLists = await _appDbContext.Department
                                                    .Where(x => x.CompanyId == companyid)
                                                    .Include(x => x.Company).ToListAsync();
            return departmentLists;
        }
        public async Task<List<string>> GetDepartmentByCompanyIdAsync(int companyId)
        {
            var companyDepartment = await _appDbContext.Department.
                                           Where(x => x.CompanyId == companyId)
                                          .Select(x => x.Name)
                                          .ToListAsync();
            return companyDepartment;
        }
        public async Task<int> AddDepartmentAsync(Department companyDepartment)
        {
            await _appDbContext.Department.AddAsync(companyDepartment);
            await _appDbContext.SaveChangesAsync();
            return companyDepartment.Id;
        }

        public async Task<List<CompanyUser>> GetDepartmentUserAsync(int departmentId)
        {
            var departmentUser = await _appDbContext.CompanyUser
                .Where(x => x.DepartmentID == departmentId)
                .Include(x => x.Department)
                .Include(x => x.Department.Company).ToListAsync();
            return departmentUser;
        }

    }
}
