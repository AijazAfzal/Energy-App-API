using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class CategoryList
    {
        public List<Category> Categories { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string ImageUrl { get; set; }
    }
}