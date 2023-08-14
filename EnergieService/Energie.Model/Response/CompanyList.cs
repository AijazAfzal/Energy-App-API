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
    public class CompanyList
    {
        public IList<Company> Companies { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}


