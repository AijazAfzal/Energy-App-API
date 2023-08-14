using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ICompanyAdminRepository
    {
        Task CreateCompanyAdmin(AddB2CCompanyAdmin addB2CCompanyAdmin);
        Task<CompanyAdmin> GetCompanyAdminByEmailAsync(string email);
    }
}
