using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Energie.Infrastructure.Repository
{
    public class CompanyRepository<T> : ICompanyRepository<T> where T : Company
    {
        private readonly AppDbContext _appDbContext;
        public CompanyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<T>> CompanyListAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetCompanyByNameAsync(string companyName)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Name == companyName);
        }
        public async Task<T> GetCompanyByID(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }
        public async Task CreateCompanyAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
