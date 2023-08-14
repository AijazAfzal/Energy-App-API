using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Energie.Domain.Domain
{
    public class EmployerHelp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Description { get; private set; }
        [Required]
        public string OwnContribution { get; private set; }
        public string Requestvia { get; private set; }
        [Required]
        public string Conditions { get; private set; }
        [Required]
        [Display(Name = "HelpCategory")]
        public virtual int HelpCategoryId { get; private set; }
        [ForeignKey("HelpCategoryId")]
        public virtual HelpCategory? HelpCategory { get; private set; }
        [Required]
        [Display(Name = "CompanyUser")]
        public virtual int CompanyUserId { get; private set; }
        [ForeignKey("CompanyUserId")]
        public virtual CompanyUser? CompanyUser { get; set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public string ImageUrl { get; private set; }  

        public EmployerHelp SetEmployerHelp(string name, string description,
                                            string ownContribution, 
                                            string requestvia, 
                                            string conditions,
                                            int helpCategoryId,
                                            int userId)
        {
            Name = name;
            Description = description;
            OwnContribution = ownContribution;
            Requestvia = requestvia;
            Conditions = conditions;
            HelpCategoryId = helpCategoryId;
            CompanyUserId = userId;
            return this;
        }

        public EmployerHelp UpdateEmployerHelp(string name,
            string description,
            string ownContribution,
            string contition,
            string requestvia,
            int helpCategoryId,
            int userId)
        {
            Name = name;
            Description = description;
            Requestvia = requestvia;
            OwnContribution = ownContribution;
            Requestvia = requestvia;
            HelpCategoryId = helpCategoryId;
            CompanyUserId = userId;
            Conditions = contition;
            UpdatedOn = DateTime.Now;
            return this;
        }

    }
}
