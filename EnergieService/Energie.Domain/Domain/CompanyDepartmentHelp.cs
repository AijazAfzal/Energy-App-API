using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class CompanyDepartmentHelp
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Description { get; private set; }
        [Required]
        public string Contribution { get; private set; }
        [Required]
        public string Requestvia { get; private set; }
        public string MoreInformation { get; private set; }
        [Required]
        [Display(Name = "Department")]
        public virtual int DepartmentId { get; private set; }
        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
        [Required]
        [Display(Name= "HelpCategory")]
        public virtual int HelpCategoryId { get; private set; }
        [ForeignKey("HelpCategoryId")]
        public virtual HelpCategory HelpCategory { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CompanyDepartmentHelp SetCompanyDepartmentHelp(string name
            , string description
            , string contribution
            , string requestvia
            , string moreInformation
            , int departmentId
            , int helpCategoryId)
        {
            Name = name;
            Description = description;
            Contribution = contribution;
            Requestvia = requestvia;
            MoreInformation = moreInformation;
            DepartmentId = departmentId;
            HelpCategoryId = helpCategoryId;
          
            return this;
        }
        public CompanyDepartmentHelp UpdateCompanyDepartmentHelp(string name
           , string description
           , string contribution
           , string requestvia
           , string moreInformation
           , int departmentId
           , int helpCategoryId)
        {
            Name = name;
            Description = description;
            Contribution = contribution;
            Requestvia = requestvia;
            MoreInformation = moreInformation;
            DepartmentId = departmentId;
            HelpCategoryId = helpCategoryId;
            UpdatedDate = DateTime.Now; 

            return this;
        }


    }
}
