using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IUserDepartmentTipRepository
    {
        Task<List<UserDepartmentTip>> GetUserDepartmentTipListAsync(int id); 
        
        Task AddUserDepartmentTipAsync(UserDepartmentTip userDepartmentTip); 

        Task<List<UserDepartmentTip>> GetUserDepartmentTipsAddedByCollegue(string userEmail);  

        Task<UserDepartmentTip> GetUserDepartmentTipbyIdAsync(int Id);

        Task DeleteUserDepartmentTipAsync(int id);
        Task UpdateUserDepartmentTipAsync(UserDepartmentTip userDepartmentTip);  


    }
}
