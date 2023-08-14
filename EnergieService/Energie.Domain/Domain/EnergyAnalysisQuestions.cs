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
    public class EnergyAnalysisQuestions
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        // Foreign key   
        [Display(Name = "EnergyAnalysis")]
        public virtual int? EnergyAnalysisID { get; private set; }
        [ForeignKey("EnergyAnalysisID")]
        public virtual EnergyAnalysis? EnergyAnalysis { get; set; }
        public DateTime CreatedOn { get; private set; }

        public string? ImageUrl { get; private set; }

        public EnergyAnalysisQuestions SetEnergyAnalysisQuestions(string name,string description,int energyanalysisId )
        {
            Name = name;
            Description = description;
            EnergyAnalysisID = energyanalysisId;
            return this;  
        }
    }

    
}
