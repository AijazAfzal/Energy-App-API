using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class UserDepartmentTip
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "EnergyAnalysisQuestions")]
        public virtual int EnergyAnalysisQuestionsId { get; private set; }
        [ForeignKey("EnergyAnalysisQuestionsId")]
        public virtual EnergyAnalysisQuestions? EnergyAnalysisQuestions { get; set; } 
        [Required]
        public string Description { get; private set; } 
        [Required]

        //[Display(Name = "Department")]
        //public virtual int? DepartmentId { get; private set; }
        //[ForeignKey("DepartmentId")] 
        //public virtual Department? Department { get; set; }

        [Display(Name = "CompanyUser")]
        public virtual int CompanyUserId { get; private set; }
        [ForeignKey("CompanyUserId")]
        public virtual CompanyUser? CompanyUser { get; set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public UserDepartmentTip SetUserDepartmentTip(int energyAnalysisQuestionsId, string description, int userId)
        {
            EnergyAnalysisQuestionsId = energyAnalysisQuestionsId;
            Description = description;
            CompanyUserId = userId;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            return this; 
            
        }

        public UserDepartmentTip UpdateDepartmentTip(string description)
        {
            Description = description;
            CreatedOn = DateTime.Now; 
            UpdatedOn = DateTime.Now;
            return this;

        }
    }
}
