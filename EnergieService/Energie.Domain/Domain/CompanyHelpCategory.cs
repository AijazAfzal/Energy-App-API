using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class CompanyHelpCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Description { get; private set; }
        [Required]
        [Display(Name = "Company")]
        public virtual int? CompanyId { get; private set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }

        public CompanyHelpCategory SetCompanyHelpCategory(string name, string description, int companyId)
        {
            Name = name;
            Description = description;
            CompanyId = companyId;
            CreatedOn = DateTime.Now;
            return this;

        }
        public CompanyHelpCategory UpdateCompanyHelpCategory(string name, string description)
        {
            Name = name;
            Description = description;
            UpdatedOn = DateTime.Now;
            return this;
        }
    }
}
