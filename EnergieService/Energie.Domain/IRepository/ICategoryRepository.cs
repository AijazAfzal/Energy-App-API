using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);
        Task<IList<Category>> GetCategoryList();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int categoryID);
    }
}
