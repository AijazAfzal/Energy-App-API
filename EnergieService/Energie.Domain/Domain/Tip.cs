using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class Tip
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Description { get; private set; }
        [Display(Name = "EnergyAnalysisQuestions")]
        public virtual int? EnergyAnalysisQuestionsId { get; private set; }
        [ForeignKey("EnergyAnalysisQuestionsId")]
        public virtual EnergyAnalysisQuestions? EnergyAnalysisQuestions { get; set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }
        
        
        public Tip SetEnergyTip(int energyAnalysisQuestionsID, string name, string description)
        {
            EnergyAnalysisQuestionsId = energyAnalysisQuestionsID;
            Name = name;
            Description = description;
            CreatedOn = DateTime.Now;
            return this;
        }
        public Tip UpdateEnergyTip(int energyAnalysisQuestionsID, string name, string description)
        {
            EnergyAnalysisQuestionsId = energyAnalysisQuestionsID;
            Name = name;
            Description = description;
            UpdatedOn = DateTime.Now;
            return this;
        }

    }
}
