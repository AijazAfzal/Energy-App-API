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
    public class LikeTip
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "CompanyUserID")]
        public virtual int CompanyUserID { get; private set; }
        [ForeignKey("CompanyUserID")]
        public virtual CompanyUser? CompanyUsers { get; set; }
        [Display(Name = "DepartmentTip")]
        public virtual int DepartmentTipId { get; private set; }
        [ForeignKey("DepartmentTipId")]
        public virtual DepartmentTip DepartmentTip { get; set; }
        public DateTime CreateDateTime { get; set; }

        //[JsonIgnore]
        //public List<CompanyUser>? CompanyUser { get; set; }
        //public List<DepartmentTip>? departmentTips { get; set; }

        public LikeTip SetLikeTip(int userId, int id)
        {
            CompanyUserID = userId;
            DepartmentTipId = id;
            CreateDateTime= DateTime.Now;
            return this;

        }
    }
}
