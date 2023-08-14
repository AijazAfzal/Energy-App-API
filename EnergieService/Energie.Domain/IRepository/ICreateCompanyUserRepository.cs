using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ICreateCompanyUserRepository
    {
        Task CreateCompanyUserAsync(B2CCompanyUser b2CCompanyUser);
        Task<int> GetUSerByIDAsync(int userID);
        Task DeleteUserAccountAsync(int userId);

        Task SaveChangesAsync();
    }
}
