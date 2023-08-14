using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Infrastructure.Repository
{
    public class DepartmentTipRepository : IDepartmentTipRepository
    {
        private readonly AppDbContext _appDbContext;
        public DepartmentTipRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DepartmentTip>> GetDepatrmentTipByListAsync(int Id)
        {
            await _appDbContext.LikeTip.ToListAsync();
            return await _appDbContext.DepatrmentTip
                         .Include(x => x.EnergyAnalysisQuestions)
                         .Include(x => x.EnergyAnalysisQuestions.EnergyAnalysis)
                         .Where(x => x.EnergyAnalysisQuestionsId == Id)
                         .ToListAsync();
        }
        // for test



        public async Task<List<DepartmentTip>> GetDepatrmentTipListAsync()
        {
            await _appDbContext.LikeTip.ToListAsync();
            return await _appDbContext.DepatrmentTip
                         .Include(x => x.EnergyAnalysisQuestions)
                         .Include(x => x.EnergyAnalysisQuestions.EnergyAnalysis)
                         //.Where(x => x.EnergyAnalysisQuestionsId == Id)
                         .ToListAsync();
        }


        public async Task AddUserFavouriteDepartmentTipAsync(DepartmentFavouriteTip departmentFavouriteTip)
        {
            await _appDbContext.DepartmentFavouriteTip.AddAsync(departmentFavouriteTip);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<IList<DepartmentFavouriteTip>> UserFavouriteDepartmentTipListAsync(string UserEmail)
        {
            return await _appDbContext.DepartmentFavouriteTip
                        .Where(x=> x.CompanyUser.Email == UserEmail)
                        .Include(x=> x.DepartmentTip)
                        .ToListAsync();
        }

        public async Task<IList<DepartmentFavouriteHelp>> DepartmentFavouriteHelpListAsync(string UserEmail)
        {
            return await _appDbContext.DepartmentFavouriteHelps
                    .Where(x => x.CompanyUser.Email == UserEmail)
                    .Include(x => x.CompanyDepartmentHelps).ToListAsync();
        }

        public async Task<IList<UserDepartmentTip>> GetUserAddedDepartmentTip(int departmentId)
        {
            return await _appDbContext.UserDepartmentTip
                                      .Where(x=> x.CompanyUser.DepartmentID== departmentId)
                                      .Include(x=> x.CompanyUser.Department)
                                      .Include(x=> x.EnergyAnalysisQuestions)
                                      .Include(x=> x.EnergyAnalysisQuestions.EnergyAnalysis)
                                      .ToListAsync();
        }
        public async Task RemovedDepartmentFavouriteTipsAsync(int userId, int departmentTipId)
        {
            var depatrmentTip = await _appDbContext.DepartmentFavouriteTip
                .Where(x => x.CompanyUserId == userId
                && x.DepartmentTip.ID == departmentTipId).FirstOrDefaultAsync();
            _appDbContext.DepartmentFavouriteTip.Remove(depatrmentTip);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<DepartmentFavouriteTip> UserFavouriteDepartmentTipAsync(int userID, int departmentTipId)
        {
            return await _appDbContext.DepartmentFavouriteTip
                .Where(x => x.CompanyUserId == userID
                & x.DepartmentTipId == departmentTipId).FirstOrDefaultAsync(); 

        }
        public async Task RemoveDepartmentFavouriteTipAsync(int Id)
        {
            var departmentTip = await _appDbContext.DepartmentFavouriteTip.Where(x => x.Id == Id).FirstOrDefaultAsync();
            _appDbContext.DepartmentFavouriteTip.Remove(departmentTip);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveUserDepartmentFavouriteTipsAsync(int departmentTipId, int userId)
        {
            var userfavhelp = await _appDbContext.UserDepartmentTip
                                                                  .Where(x => x.Id == departmentTipId && x.CompanyUser.DepartmentID==userId)
                                                                  .FirstOrDefaultAsync();

            _appDbContext.UserDepartmentTip.Remove(userfavhelp);
            await _appDbContext.SaveChangesAsync(); 
        }

        //For Department EnergyPlan

        public async Task<int> UserFavouriteDepartmenTiptForPlanAsync(int departmentId, int departmentTipId)
        {
            return await _appDbContext.DepartmentFavouriteTip
                .Where(x =>x.CompanyUser.DepartmentID == departmentId && x.Id == departmentTipId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(); 
          
            
        }

        public async Task<int> DeparmentFavouriteHelpAsync(int departmentId, int departmentTipId)
        {
            return await _appDbContext.DepartmentFavouriteHelps
                .Where(x=>x.CompanyUser.DepartmentID==departmentId && x.Id==departmentTipId)
                .Select(x=>x.Id)
                .FirstOrDefaultAsync();   

            
        }

        public async Task<int> UserDepartmentFavouriteTip(int departmentId, int departmentTipId)
        {
            return await _appDbContext.UserDepartmentTip
                .Where(x=>x.CompanyUser.DepartmentID==departmentId && x.Id==departmentTipId)
                .Select(x=>x.Id)
                .FirstOrDefaultAsync(); 
        }
    }
}
