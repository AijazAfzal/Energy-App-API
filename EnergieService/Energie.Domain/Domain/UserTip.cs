using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class UserTip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        // Foreign key   
        [Display(Name = "EnergyAnalysisQuestions")]
        public virtual int EnergyAnalysisQuestionsId { get; private set; }
        [ForeignKey("EnergyAnalysisQuestionsId")]
        public virtual EnergyAnalysisQuestions? EnergyAnalysisQuestions { get; private set; }
        [Required]
        public string Description { get; private set; }
        [Required]
        // Foreign key   
        [Display(Name = "CompanyUserID")]
        public virtual int? CompanyUserID { get; private set; }
        [ForeignKey("CompanyUserID")]
        public virtual CompanyUser? CompanyUser { get; set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public UserTip SetUserTip(int energyAnalysisQuestionsId, int companyUserId, string description)
        {
            EnergyAnalysisQuestionsId = energyAnalysisQuestionsId;
            CompanyUserID = companyUserId;
            Description = description;
            CreatedOn = DateTime.Now;
            return this;
        }
        public UserTip UpdateUserTip(int energyAnalysisQuestionsId, string description)
        {
            EnergyAnalysisQuestionsId = energyAnalysisQuestionsId;
            Description = description;
            CreatedOn = DateTime.Now;
            return this;
        }
    }
}
