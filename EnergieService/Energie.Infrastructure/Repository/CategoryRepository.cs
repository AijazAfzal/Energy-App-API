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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext= appDbContext;
        }

        public async Task AddCategory(Category category)
        {
            await _appDbContext.Category.AddAsync(category);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<IList<Category>> GetCategoryList()
        {
            return await _appDbContext.Category.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _appDbContext.Category.Where(x => x.Id == categoryId).FirstOrDefaultAsync();
        }
        public async Task<Category> UpdateCategory(Category category)
        {
            _appDbContext.Update(category);
            await _appDbContext.SaveChangesAsync();
            return category;
        }
        public async Task DeleteCategory(int categoryID)
        {
            var category = await _appDbContext.Category.Where(x => x.Id == categoryID).FirstOrDefaultAsync();
            _appDbContext.Category.Remove(category);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
