using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class DepartmentCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class DepartmentCategoryList
    {
        public List<DepartmentCategory> DepartmentCategories { get; set; }
    }
}
