using Energie.Domain.Domain;

namespace Energie.Domain.IRepository
{
    public interface ICompanyRepository<T> where T : Company
    {
        Task<IList<T>> CompanyListAsync();
        Task<T> GetCompanyByNameAsync(string companyName);
        Task CreateCompanyAsync(T entity);
        Task<T> GetCompanyByID(int id);
    }
}
