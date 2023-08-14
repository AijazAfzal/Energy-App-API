using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    
    public class CompanyUserEnergyTipList
    {
        public int Id { get; set; }
        public string CompanyHelpCategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<CompanyUserEnergyTip> CompanyUserEnergyTips { get; set; }
    }
   
    public class CompanyUserEnergyTip
    {
        public int Id { get; set; }
        [Required]
        public string Name { get;  set; }
        [Required]
        public string Description { get;  set; }
        [Required]
        public string Requestvia { get;  set; }
        [Required]
        public string Conditions { get; set; }
        [Required]
        public string OwnContribution { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
