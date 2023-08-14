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
    public class CompanyHelp
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
        public virtual HelpCategory HelpCategory { get; set; }
        [Required]
        [Display(Name = "Company")]
        public virtual int? CompanyId { get; private set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }

        //[Required]
        //[Display(Name = "CompanyHelpCategory")]
        //public virtual int? CompanyHelpCategoryID { get; private set; }
        //[ForeignKey("CompanyHelpCategoryID")]
        //public virtual CompanyHelpCategory? CompanyHelpCategory { get; private set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }

        public CompanyHelp SetCompanyHelp(string name, string description
            ,string ownContribution, string requirement, string requestvia
            , int companyhelpCategoryId, int companyId)
        {
            Name= name;
            Description= description;
            Requestvia= requestvia;
            Conditions = requirement;
            HelpCategoryId = companyhelpCategoryId;
            OwnContribution = ownContribution;
            CreatedOn = DateTime.Now;
            CompanyId = companyId;
            return this;

        }
        public CompanyHelp UpdateCompanyHelp (string name, 
            string description, 
            string ownContribution, 
            string requirement, 
            string requestvia, 
            int companyhelpCategoryId)
        {
            Name= name; 
            Description= description;
            Requestvia= requestvia;
            OwnContribution= ownContribution;
            Requestvia= requestvia;
            HelpCategoryId = companyhelpCategoryId;
            Conditions= requirement;
            UpdatedOn = DateTime.Now;
            return this;
        }

        public CompanyHelp SetCompanyHelpTest(int id,string name, string description
           , string ownContribution, string requirement, string requestvia
           , int companyhelpCategoryId, int companyId)
        {
            Id= id;
            Name = name;
            Description = description;
            Requestvia = requestvia;
            Conditions = requirement;
            HelpCategoryId = companyhelpCategoryId;
            OwnContribution = ownContribution;
            CreatedOn = DateTime.Now;
            CompanyId = companyId;
            return this;

        }
    }
}
