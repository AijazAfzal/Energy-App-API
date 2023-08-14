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
    public class UserEnergyAnalysis
    {
        [Key]
        public int Id { get; set; }
        // Foreign key   
        [Display(Name = "EnergyAnalysisQuestions")]
        public virtual int? EnergyAnalysisQuestionsID { get; private set; }
        [ForeignKey("EnergyAnalysisQuestionsID")]
        public virtual EnergyAnalysisQuestions? EnergyAnalysisQuestions { get; set; }
        // Foreign key   
        [Display(Name = "CompanyUserID")]
        public virtual int? CompanyUserID { get; private set; }
        [ForeignKey("CompanyUserID")]
        public virtual CompanyUser? CompanyUser { get; set; }
        public DateTime CreatedOn { get; private set; }

        //public bool Is_Updated { get; set; } = false; 
        public UserEnergyAnalysis SetUserEnergyAnalysis(int energyAnalysisQuestionsID, int userId)
        {
            CompanyUserID = userId;
            CreatedOn = DateTime.Now;
            EnergyAnalysisQuestionsID = energyAnalysisQuestionsID;
            return this;
            
        }
    }
}
