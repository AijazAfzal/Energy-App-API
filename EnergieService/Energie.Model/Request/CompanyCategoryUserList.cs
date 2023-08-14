using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class CompanyCategoryUserList
    {
        public List<CompanyCategory> CompanyCategories { get; set; }
    }
    [ExcludeFromCodeCoverage] 
    public class CompanyCategory
    { 
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; } 
    }
}
