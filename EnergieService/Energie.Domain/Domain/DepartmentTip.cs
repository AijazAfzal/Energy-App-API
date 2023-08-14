using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class DepartmentTip
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        [MaxLength(120)]
        public string Description { get; private set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        
        public DateTime UpdatedOn { get; private set;}
        [Required]
        public string AddedBy { get; private set; }
        [Display(Name = "EnergyAnalysisQuestions")]
        [Required]
        public virtual int? EnergyAnalysisQuestionsId { get; private set; }
        [ForeignKey("EnergyAnalysisQuestionsId")]
        public virtual EnergyAnalysisQuestions? EnergyAnalysisQuestions { get; set; }

        [JsonIgnore]
        public List<LikeTip> LikeTips { get; set; }

        public DepartmentTip SetDepartMentTip(string name, string description, int energyAnalysisQuestionsId)
        {
            Name = name;
            Description = description;
            EnergyAnalysisQuestionsId = energyAnalysisQuestionsId;
            CreatedOn = DateTime.Now;
            AddedBy = "SuperAdmin";
            return this;
        }
        public DepartmentTip UpdateDepartMentTip(string name, string description, int energyAnalysisQuestionsId)
        {
            Name = name;
            Description = description;
            EnergyAnalysisQuestionsId = energyAnalysisQuestionsId;
            UpdatedOn = DateTime.Now;
            AddedBy = "SuperAdmin";
            return this;
        }
    }
}
