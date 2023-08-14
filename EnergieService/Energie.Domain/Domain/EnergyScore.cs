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
    public class EnergyScore
    {
        [Key]
        public int Id { get; private set; }
        [Required(ErrorMessage = "EnergyScore Required")]
        [Range(1, 10)]
        public int Score { get; private set; }
        [Display(Name = "MonthId")]
        [Range(1, 12)]
        public virtual int MonthId { get; private set; }
        [ForeignKey("MonthId")]
        public virtual Month? Month { get; set; }
        public int Year { get; private set; }
        [Display(Name = "CompanyUserID")]
        public virtual int? CompanyUserID { get; private set; }
        [ForeignKey("CompanyUserID")]
        public virtual CompanyUser? CompanyUser { get; set; }
        public DateTime CreatedOn { get; private set; }
        public EnergyScore SetEnergyScore(int score, int companyUserID)
        {
            DateTime datetime = DateTime.Now;
            MonthId = datetime.Month;
            Year = datetime.Year;
            Score = score;
            CompanyUserID = companyUserID;
            CreatedOn = DateTime.Now;
            return this;
        }

        // created this method for use in unit test list 
        public EnergyScore SetEnergyScoreForList(int score, int monthId, int year, int companyUserID)
        {
            MonthId = monthId;
            Year = year;
            Score = score;
            CompanyUserID = companyUserID;
            CreatedOn = DateTime.Now;
            return this;
        }
    }
}
