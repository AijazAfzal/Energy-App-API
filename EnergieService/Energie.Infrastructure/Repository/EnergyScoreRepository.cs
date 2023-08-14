using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;

namespace Energie.Infrastructure.Repository
{
    public class EnergyScoreRepository : IEnergyScoreRepository
    {
        private readonly AppDbContext _appDbContext;
        public EnergyScoreRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Month>> GetAllMonth()
        {
            return await _appDbContext
                        .Month
                        .ToListAsync(); ;
        }
        public async Task<List<EnergyScore>> GetAllEnergyScores(string userId)
        {
            return await _appDbContext
                        .EnergyScore
                        .Include(x => x.CompanyUser)
                        .Where(x => x.CompanyUser.Email == userId)
                        .ToListAsync();
        }
        public async Task<EnergyScore> AddEnergyScoreAsync(EnergyScore energyScore)
        {
            await _appDbContext.EnergyScore.AddAsync(energyScore);
            await _appDbContext.SaveChangesAsync();
            return energyScore;
        }
        public async Task<EnergyScore> GetCompanyUserEnergyScore(int userId)
        {
            var energyScore = await _appDbContext.EnergyScore.Where(x => x.CompanyUserID == userId)
                .Include(x => x.CompanyUser).Include(x => x.CompanyUser.Department)
                .Include(x => x.CompanyUser.Department.Company)
                .FirstOrDefaultAsync();
            return energyScore;
        }
        public async Task<Department> GetDepartmentIdByUser(string email)
        {
            return await _appDbContext
                        .Department
                        .Where(x => x.CompanyUser.Any(y => y.Email == email))
                        .FirstOrDefaultAsync();
        }

        public async Task<List<EnergyScore>> GetDepartmentScoreList(int departmentId)
        {
            var departmentScore = await _appDbContext.EnergyScore.Where(x => x.CompanyUser!.DepartmentID == departmentId).ToListAsync();
            return departmentScore;
        }
        public async Task<List<EnergyScore>> GetAllDepartmentEnergyScores(int departmentId)
        {
            return await _appDbContext
                        .EnergyScore
                        .Where(x => x.CompanyUser.DepartmentID == departmentId)
                        .ToListAsync();
        }

        public async Task<IList<EnergyScore>> GetAllUserEnergyScores()
        {
            var currentdateee = DateTime.Now;
            var currentmonth = currentdateee.Month; 
            var reqdate = new DateTime(currentdateee.Year, currentdateee.Month, 1);
            return await _appDbContext
                      .EnergyScore
                      .Where(x => x.MonthId == currentmonth && x.CreatedOn != reqdate) 
                      .Include(x => x.CompanyUser) 
                      .ToListAsync(); 
        }
    }
}
