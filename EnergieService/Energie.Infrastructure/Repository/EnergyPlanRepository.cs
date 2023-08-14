using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Energie.Infrastructure.Repository
{
    public class EnergyPlanRepository : IEnergyPlanRepository
    {
        private readonly AppDbContext _appDbContext;
        public EnergyPlanRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #region for personal
        public async Task AddEnergyPlanAsync(EnergyPlan energyPlan)
        {
            await _appDbContext.EnergyPlan.AddAsync(energyPlan);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<EnergyPlan> GetEnergyPlanForUserAsync(int userId, int favouriteTipId, int tiptype)
        {
            return await _appDbContext.EnergyPlan
                         .Where(x => x.CompanyUserId == userId && x.FavouriteTipId == favouriteTipId && x.TipTypeId == tiptype)
                         .FirstOrDefaultAsync();

        }

        public async Task<IList<EnergyPlan>> GetEnergyPlanListAsync(string userEmail)
        {
            return await _appDbContext.EnergyPlan
                         .Include(x => x.CompanyUser)
                         .Include(x => x.ResponsiblePerson)
                         .Include(x => x.PlanStatus)
                         .Include(x => x.TipType)
                         .Where(x => x.CompanyUser.Email == userEmail).ToListAsync();
        }

        public async Task<EnergyPlan> GetEnergyPlanbyIdAsync(int id, string userEmail)
        {
            return await _appDbContext.EnergyPlan.Where(x => x.Id == id && x.CompanyUser.Email == userEmail).FirstOrDefaultAsync();
        }
        public async Task UpdateEnergyPlanAsync(EnergyPlan energyPlan)
        {
            _appDbContext.EnergyPlan.Update(energyPlan);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteEnergyPlanAsync(int id)
        {
            var energyplan = await _appDbContext.EnergyPlan.Where(x => x.Id == id).FirstOrDefaultAsync();
            _appDbContext.EnergyPlan.Remove(energyplan);
            await _appDbContext.SaveChangesAsync();
        }
        #endregion

        //For Department EnergyPlan
        #region for department
        public async Task AddDepartmentEnergyPlanAsync(DepartmentEnergyPlan EnergyPlan)
        {
            await _appDbContext.DepartmentEnergyPlans.AddAsync(EnergyPlan);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<DepartmentEnergyPlan> GetDepartmentEnergyPlanAsync(int departmentId, int favouriteTipId, int TiptypeId)
        {
            return await _appDbContext.DepartmentEnergyPlans
                         .Where(x => x.CompanyUserId == departmentId && x.FavouriteTipId == favouriteTipId && x.TipTypeId == TiptypeId)
                         .FirstOrDefaultAsync();

            
        } 

        public async Task<IList<DepartmentEnergyPlan>> GetDepartmentEnergyPlanListAsync(string useremail)
        {
            return await _appDbContext.DepartmentEnergyPlans
                          .Include(x => x.CompanyUser)
                          .Include(x => x.ResponsiblePerson)
                          .Include(x => x.PlanStatus)
                          .Include(x => x.TipType)
                          .Where(x => x.CompanyUser.Email == useremail)
                          .ToListAsync();
        }
        public async Task<DepartmentEnergyPlan> GetDepartmentEnergyPlanByIdAsync(int Id, string userEmail)
        {
            return await _appDbContext.DepartmentEnergyPlans
                        .Where(x => x.Id == Id && x.CompanyUser.Email == userEmail)
                        .FirstOrDefaultAsync();
        }
        public async Task UpdateDepartmentEnergyPlanAsync(DepartmentEnergyPlan energyPlan)
        {
            _appDbContext.DepartmentEnergyPlans.Update(energyPlan);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteDepartmentEnergyPlanAsync(int id, string userEmail)
        {
            var userDepartmentPlan = await _appDbContext.DepartmentEnergyPlans
                                           .Where(x => x.Id == id && x.CompanyUser.Email == userEmail)
                                           .FirstOrDefaultAsync();
            _appDbContext.DepartmentEnergyPlans.Remove(userDepartmentPlan);
            await _appDbContext.SaveChangesAsync();
        }
        #endregion

        public async Task<IList<EnergyPlan>> GetEnergyPlanAsync()
        {
            //DateTime datetime = DateTime.Now;
            //var adddays = datetime.AddDays(3);
            //return await _appDbContext.EnergyPlan.Where(x=>x.PlanEndDate <= adddays).ToListAsync();
            var currentdate = DateTime.UtcNow;
            return await _appDbContext.EnergyPlan.Include(x=>x.CompanyUser).Where(x => x.PlanEndDate >= currentdate).ToListAsync(); 
          

        }

        public async Task<List<DateTime>> GetEnergyPlanDeadlineAsync(string useremail)
        {
            return await _appDbContext.EnergyPlan.Where(x => x.CompanyUser.Email == useremail).Select(x => x.PlanEndDate).ToListAsync();
        }

        public async Task<List<EnergyPlan>> GetEnergyPlansForDeadline(string userEmail)
        {
            var currentdate = DateTime.UtcNow;
            var plans = await _appDbContext.EnergyPlan.Where(x => x.CompanyUser!.Email == userEmail).ToListAsync();
            var list = plans.Where(x => x.PlanEndDate >= currentdate).ToList(); 
            return list; 
        }
    }
}
        
