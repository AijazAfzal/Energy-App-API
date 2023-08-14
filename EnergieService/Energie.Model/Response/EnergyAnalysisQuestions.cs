using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;
using Energie.Model.Request;
using Department = Energie.Model.Request.Department;
using Language = Energie.Model.Request.Language;

namespace Energie.Model.Response
{

    public class ListEnergyAnalysisQuestions
    {
        public List<EnergyAnalysisQuestions> EnergyAnalysisQuestion { get; set; }
    }

    public class EnergyAnalysisQuestions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Foreign key   
        [Display(Name = "EnergyAnalysis")]
        public virtual int? EnergyAnalysisID { get; set; }
        [ForeignKey("EnergyAnalysisID")]
        public virtual EnergyAnalysis? EnergyAnalysis { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsSelected { get; set; }
    }

    public class EnergyAnalysisList
    {
        public IList<EnergyAnalysis> EnergyAnalyses { get; set; }
    }

    public class EnergyAnalysis
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }


    public class CompanyUser
    {
        [Key]
        public int Id { get;  set; }
        public string UserName { get;  set; }
        public string Email { get;  set; }
        [Display(Name = "Department")]
        public virtual int? DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }

        public virtual int? LanguageID { get; set; } = 1;
        [ForeignKey("LanguageID")]
        public virtual Language? Language { get; set; } 

        public Boolean ShowOnboarding { get; set; }
        public DateTime CreatedOn { get; set; } 
    }
}
