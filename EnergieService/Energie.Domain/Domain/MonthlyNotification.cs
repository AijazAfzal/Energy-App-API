using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MediatR;

namespace Energie.Domain.Domain
{
    public class MonthlyNotification
    {
        [Key]
        public int ID { get; private set; }
        [Display(Name = "CompanyUserID")]
        public virtual int? CompanyUserID { get; private set; }
        [ForeignKey("CompanyUserID")]
        public virtual CompanyUser? CompanyUser { get; set; }
        [Display(Name = "MonthId")]
        [Range(1, 12)]
        public virtual int MonthId { get; private set; }
        [ForeignKey("MonthId")]
        public virtual Month? Month { get; set; }
        [Required]
        public int Year { get; private set; }

        [Required]
        public bool PopUp { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public MonthlyNotification SetFlagForPopUp(int userId, int monthId, int year, bool popup)
        {
            CompanyUserID = userId;
            MonthId = monthId;
            Year = year;
            //IsActive = isActive;
            PopUp = popup;
            CreatedOn = DateTime.Now;
            return this;
        }
    }
}
