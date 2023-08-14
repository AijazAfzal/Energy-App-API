using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IUserEnergyTipRepository
    {
        Task<List<Tip>> GetUserEnergyTip(int categoryId);
        Task SetUserFavouriteTip(UserFavouriteTip userFavouriteTip);
        //Task<List<UserFavouriteTip>> UserFavouriteTipAsync(string email);
        Task<UserFavouriteTip> GetFavouriteTipById(UserFavouriteTip userFavouriteTip);
        Task RemoveSuperAdminFavouriteTipAsync(int tipId, int userId);

        Task RemoveUserFavouriteTipAsync(int tipId);

        //Employer Help
        Task SetCompanyHelpUserFavouriteTip(UserFavouriteHelp userFavouriteHelp);
        Task<UserFavouriteHelp> GetUserFavouriteHelpByIdAsync(UserFavouriteHelp userFavouriteHelp);
        //Task<List<UserFavouriteHelp>> UserFavouriteHelpAsync(string email);
        
        Task RemoveEmployerFavouriteHelpAsync(int tipId, int userId);
        // User Tip
        Task<UserEnergyAnalysis> UserEnergyAnalysisAsync(string userEmail, int Id);
        Task AddUserFavouriteTipAsync(UserTip userTip);
        //Task<List<UserTip>> UserFavouriteTipByUserAsync(string email);
        Task RemoveUserFavouriteHelpAsync(int tipsId, int userId);
        Task<UserTip> GetUserFavouriteTipByIdAsync(int id);
        Task<UserTip> UpdateUserFavouriteTipAsync(UserTip userTip);

        //for EnergyPlan
        Task<int> GetUserFavouriteTipById(int id, int userId);
        Task<int> UserFavouriteHelpByIdAsync(int id, int userId);
        Task<int> UserFavouriteTipByIdAsync(int id, int userId);



        Task<(List<UserFavouriteTip>, List<UserFavouriteHelp>, List<UserTip>)> GetUserFavoritesAsync(string email);

        Task<(List<UserFavouriteTip>, List<UserFavouriteHelp>, List<UserTip>)> GetAllFavTipAsync(); 



    }
}
