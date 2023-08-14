using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IDepartmentTipRepository
    {
        Task<List<DepartmentTip>> GetDepatrmentTipByListAsync(int Id);
        // fort test 

        Task<List<DepartmentTip>> GetDepatrmentTipListAsync();


        Task AddUserFavouriteDepartmentTipAsync(DepartmentFavouriteTip departmentFavouriteTip);
        Task RemovedDepartmentFavouriteTipsAsync(int userId, int departmentTipId);
        Task<DepartmentFavouriteTip> UserFavouriteDepartmentTipAsync(int userID, int departmentTipId);
        Task<IList<DepartmentFavouriteTip>> UserFavouriteDepartmentTipListAsync(string UserEmail);
        Task RemoveDepartmentFavouriteTipAsync(int Id);
        Task<IList<UserDepartmentTip>> GetUserAddedDepartmentTip(int departmentId);
        Task<IList<DepartmentFavouriteHelp>> DepartmentFavouriteHelpListAsync(string UserEmail);

        Task RemoveUserDepartmentFavouriteTipsAsync(int departmentTipId, int userId);

        //For Department Energy Plan

        Task<int> UserFavouriteDepartmenTiptForPlanAsync(int departmentId, int departmentTipId); // SuperAdmin 

        Task<int> DeparmentFavouriteHelpAsync(int departmentId, int departmentTipId); // CompanyAdmin 

        Task<int> UserDepartmentFavouriteTip(int departmentId,int departmentTipId);  // User 
    }
}
