using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ICompanyUserRepository
    {
        Task<CompanyUser> GetCompanyUserAsync(string email);
        Task<List<CompanyUser>> GetCompanyUserByDepartmentId(int departmentId);

        Task RemoveUserAccount(int userID);

        Task UpdateCompanyUserLanguage(CompanyUser companyUser);

        Task<IList<Language>> GetLanguagesAsync();  
    }
}
