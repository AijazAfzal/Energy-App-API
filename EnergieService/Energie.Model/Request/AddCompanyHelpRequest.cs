using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class AddCompanyHelpRequest 
    {
        [Required]
        public int CompanyHelpCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; } 
        [Required]
        public string OwnContribution { get; set; }
        [Required]
        public string Requestvia { get; set; }
        [Required]
        public string Conditions { get; set; }
    }
}
