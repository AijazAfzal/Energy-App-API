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
    public class CompanyUserRepository : ICompanyUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public CompanyUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CompanyUser> GetCompanyUserAsync(string email) 
        {
            var companyUser =  await _appDbContext.CompanyUser
                .Include(c => c.Department)
                .Include(x => x.Department.Company)
                .Include(x=>x.Language)
                .Where(x => x.Email == email) 
                .FirstOrDefaultAsync(); 
            return companyUser; 
        }
        public async Task<List<CompanyUser>> GetCompanyUserByDepartmentId(int departmentId)
        {
            return await _appDbContext.CompanyUser.Where(x => x.DepartmentID == departmentId).ToListAsync(); 
        }

        //For Updating Language of Company User

        public async Task UpdateCompanyUserLanguage(CompanyUser companyUser)
        {
            _appDbContext.CompanyUser.Update(companyUser);
            await _appDbContext.SaveChangesAsync();   
        }

        //For Removing User Account

        public async Task RemoveUserAccount(int userID)
        {
            //deleting user history

            var usermonthlynotifications = await _appDbContext.MonthlyNotifications.Where(x => x.CompanyUserID == userID).ToListAsync();
            _appDbContext.MonthlyNotifications.RemoveRange(usermonthlynotifications); 
           // _appDbContext.MonthlyNotifications.RemoveRange(await _appDbContext.MonthlyNotifications.Where(x => x.CompanyUserID == userID).ToListAsync());

            var userenergyscore = await _appDbContext.EnergyScore.Where(x=>x.CompanyUserID==userID).ToListAsync();
            _appDbContext.EnergyScore.RemoveRange(userenergyscore);

            var userenergyanalysis = await _appDbContext.UserEnergyAnalyses.Where(x=>x.CompanyUserID==userID).ToListAsync(); 
            _appDbContext.UserEnergyAnalyses.RemoveRange(userenergyanalysis);    

            var userfavtip = await _appDbContext.UserTip.Where(x=>x.CompanyUserID==userID).ToListAsync();
            _appDbContext.UserTip.RemoveRange(userfavtip);

            var userfavdepttip = await _appDbContext.UserDepartmentTip.Where(x =>x.CompanyUserId==userID).ToListAsync();
            _appDbContext.UserDepartmentTip.RemoveRange(userfavdepttip); 

            var superadminfavtip = await _appDbContext.UserFavouriteTips.Where(x=>x.CompanyUserId==userID).ToListAsync();
            _appDbContext.UserFavouriteTips.RemoveRange(superadminfavtip);

            var companyadminhelp = await _appDbContext.UserFavouriteHelp.Where(x => x.CompanyUserId == userID).ToListAsync();
            _appDbContext.UserFavouriteHelp.RemoveRange(companyadminhelp);

            var deptsuperadminfavtip = await _appDbContext.DepartmentFavouriteTip.Where(x => x.CompanyUserId == userID).ToListAsync();
            _appDbContext.DepartmentFavouriteTip.RemoveRange(deptsuperadminfavtip);

            var deptfavhelp = await _appDbContext.DepartmentFavouriteHelps.Where(x=>x.CompanyUserId==userID).ToListAsync();
            _appDbContext.DepartmentFavouriteHelps.RemoveRange(deptfavhelp);

            var liketipbyuserindept = await _appDbContext.LikeTip.Where(x => x.CompanyUserID == userID).ToListAsync();
            _appDbContext.LikeTip.RemoveRange(liketipbyuserindept);

            var energyplan = await _appDbContext.EnergyPlan.Where(x=>x.CompanyUserId==userID).ToListAsync();
            _appDbContext.EnergyPlan.RemoveRange(energyplan);

            var departmentenergyplan = await _appDbContext.DepartmentEnergyPlans.Where(x => x.CompanyUserId == userID).ToListAsync(); 
            _appDbContext.DepartmentEnergyPlans.RemoveRange(departmentenergyplan);

            await _appDbContext.SaveChangesAsync(); 

            //deleting user

            var usertoberemoved=await _appDbContext.CompanyUser.Where(x=>x.Id==userID).FirstOrDefaultAsync();
            _appDbContext.CompanyUser.Remove(usertoberemoved); 

            await _appDbContext.SaveChangesAsync(); 

            


        }

        public async Task<IList<Language>> GetLanguagesAsync()
        {
            return await _appDbContext.Languages.ToListAsync();  
            
        }


    }
}
