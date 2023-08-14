using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Energie.Infrastructure.Repository
{
    public class UserEnergyTipRepository : IUserEnergyTipRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserEnergyTipRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #region Super Admin Tips
        public async Task<List<Tip>> GetUserEnergyTip(int energyAnalysisQuestionsId)
        {
            var tipList = await _appDbContext.Tips.Where(x => x.EnergyAnalysisQuestionsId == energyAnalysisQuestionsId)
                .Include(x => x.EnergyAnalysisQuestions).ToListAsync();
            return tipList;
        }
        public async Task SetUserFavouriteTip(UserFavouriteTip userFavouriteTips)
        {
            await _appDbContext.UserFavouriteTips.AddAsync(userFavouriteTips);
            await _appDbContext.SaveChangesAsync();
        }
        //public async Task<List<UserFavouriteTip>> UserFavouriteTipAsync(string email)
        //{

        //    var usertips = await _appDbContext.UserFavouriteTips
        //                         .Include(x => x.Tips)
        //                         .Include(x => x.CompanyUser)
        //                         .Where(x => x.CompanyUser.Email == email)
        //                         .ToListAsync();
        //    return usertips;
        //}

        //Remove user Favourite tip 

        public async Task RemoveUserFavouriteTipAsync(int tipId)
        {
            var tips = await _appDbContext.UserFavouriteTips
                                 .Where(x => x.Id == tipId)
                                 .FirstOrDefaultAsync();
            _appDbContext.UserFavouriteTips.Remove(tips);
        }

        public async Task<UserFavouriteTip> GetFavouriteTipById(UserFavouriteTip userFavouriteTips)
        {
            var existTips = await _appDbContext.UserFavouriteTips
                            .Where(x => x.TipId == userFavouriteTips.TipId
                                   && x.CompanyUserId == userFavouriteTips.CompanyUserId)
                            .FirstOrDefaultAsync();
            return existTips;
        }

        public async Task RemoveSuperAdminFavouriteTipAsync(int tipsId, int userId)
        {
            var tips = await _appDbContext.UserFavouriteTips
                                 .Where(x => x.Id == tipsId && x.CompanyUserId == userId)
                                 .FirstOrDefaultAsync();
            _appDbContext.UserFavouriteTips.Remove(tips);
            await _appDbContext.SaveChangesAsync();

        }
        #endregion


        #region Company Admin Help (Employer Help Tips)
        public async Task SetCompanyHelpUserFavouriteTip(UserFavouriteHelp userFavouriteHelp)
        {
            await _appDbContext.UserFavouriteHelp.AddAsync(userFavouriteHelp);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<UserFavouriteHelp> GetUserFavouriteHelpByIdAsync(UserFavouriteHelp userFavouriteHelp)
        {
            return await _appDbContext.UserFavouriteHelp
                                        .Where(x => x.CompanyHelpID == userFavouriteHelp.CompanyHelpID
                                                && x.CompanyUserId == userFavouriteHelp.CompanyUserId)
                                        .FirstOrDefaultAsync();

        }

        //public async Task<List<UserFavouriteHelp>> UserFavouriteHelpAsync(string email)
        //{
        //    return await _appDbContext.UserFavouriteHelp
        //                          .Where(x => x.CompanyUser.Email == email)
        //                          .Include(x => x.CompanyHelp).ToListAsync();
        //
        //}

        public async Task RemoveEmployerFavouriteHelpAsync(int tipsId, int userId)
        {
            var help = await _appDbContext.UserFavouriteHelp
                                 .Where(x => x.Id == tipsId && x.CompanyUserId == userId)
                                 .FirstOrDefaultAsync();
            _appDbContext.UserFavouriteHelp.Remove(help);
            await _appDbContext.SaveChangesAsync();
        }
        #endregion

        #region User Tips

        public async Task<UserEnergyAnalysis> UserEnergyAnalysisAsync(string userEmail, int Id)
        {
            var selectedUserTip = await _appDbContext.UserEnergyAnalyses
                                .Where(x => x.CompanyUser.Email == userEmail
                                && x.EnergyAnalysisQuestionsID == Id)
                                .Include(x => x.EnergyAnalysisQuestions)
                                .FirstOrDefaultAsync();
            return selectedUserTip;

        }
        public async Task AddUserFavouriteTipAsync(UserTip userTip)
        {
            await _appDbContext.UserTip.AddAsync(userTip);
            await _appDbContext.SaveChangesAsync();
        }

        //public async Task<List<UserTip>> UserFavouriteTipByUserAsync(string email)
        //{
        //    var usertips = await _appDbContext.UserTip
        //                         .Where(x => x.CompanyUser.Email == email)
        //                         .Include(x => x.EnergyAnalysisQuestions).ToListAsync();
        //    return usertips;
        //}

        public async Task RemoveUserFavouriteHelpAsync(int tipsId, int userId)
        {
            var usertip = await _appDbContext.UserTip
                                 .Where(x => x.Id == tipsId && x.CompanyUserID == userId)
                                 .FirstOrDefaultAsync();
            _appDbContext.UserTip.Remove(usertip);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<UserTip> GetUserFavouriteTipByIdAsync(int id)
        {
            var usertip = await _appDbContext.UserTip.Where(x => x.Id == id)
                .Include(x => x.EnergyAnalysisQuestions)
                .FirstOrDefaultAsync();
            return usertip;
        }
        public async Task<UserTip> UpdateUserFavouriteTipAsync(UserTip userTip)
        {
            _appDbContext.UserTip.Update(userTip);
            await _appDbContext.SaveChangesAsync();
            return userTip;
        }
        #endregion

        #region check tips for plan 

        public async Task<int> GetUserFavouriteTipById(int id, int userId)
        {
            return await _appDbContext.UserFavouriteTips
                                .Where(x => x.Id == id && x.CompanyUserId == userId)
                                .Select(x => x.Id)
                                .FirstOrDefaultAsync();
        }
        public async Task<int> UserFavouriteHelpByIdAsync(int id, int userId)
        {
            return await _appDbContext.UserFavouriteHelp
                                 .Where(x => x.Id == id && x.CompanyUserId == userId)
                                 .Select(x => x.Id)
                                 .FirstOrDefaultAsync();

        }
        public async Task<int> UserFavouriteTipByIdAsync(int id, int userId)
        {
            return await _appDbContext.UserTip
                                 .Where(x => x.Id == id && x.CompanyUserID == userId)
                                 .Select(x => x.Id)
                                 .FirstOrDefaultAsync();
        }

        #endregion

        #region OptmizeCode 

        public async Task<(List<UserFavouriteTip>, List<UserFavouriteHelp>, List<UserTip>)> GetUserFavoritesAsync(string email)
        {
            var userFavouriteTips = await _appDbContext.UserFavouriteTips
                                                     .Include(x => x.Tips)
                                                     .Include(x => x.CompanyUser)
                                                     .Where(x => x.CompanyUser.Email == email)
                                                     .ToListAsync();

            var userFavouriteHelps = await _appDbContext.UserFavouriteHelp
                                                      .Where(x => x.CompanyUser.Email == email)
                                                      .Include(x => x.CompanyHelp)
                                                      .ToListAsync();

            var userTips = await _appDbContext.UserTip
                                               .Where(x => x.CompanyUser.Email == email)
                                               .Include(x => x.EnergyAnalysisQuestions)
                                               .ToListAsync();

            return (userFavouriteTips, userFavouriteHelps, userTips);
        }

        public async Task<(List<UserFavouriteTip>, List<UserFavouriteHelp>, List<UserTip>)> GetAllFavTipAsync()
        {
            var userFavouriteTips = await _appDbContext.UserFavouriteTips
                                                     .Include(x => x.Tips)
                                                     .Include(x => x.CompanyUser)
                                                     .ToListAsync();

            var userFavouriteHelps = await _appDbContext.UserFavouriteHelp
                                                      .Include(x => x.CompanyHelp)
                                                      .ToListAsync();

            var userTips = await _appDbContext.UserTip
                                               .Include(x => x.EnergyAnalysisQuestions)
                                               .ToListAsync();

            return (userFavouriteTips, userFavouriteHelps, userTips); 
        }

        #endregion 
    }

}
