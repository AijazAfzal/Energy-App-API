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
    public class DepartmentFavouriteTip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "DepartmentTip")]
        public virtual int? DepartmentTipId { get; private set; }  
        [ForeignKey("DepartmentTipId")]
        public virtual DepartmentTip? DepartmentTip { get; set; }
        [Required]
        [Display(Name = "CompanyUser")]
        public virtual int? CompanyUserId { get; private set; }
        [ForeignKey("CompanyUserId")]
        public virtual CompanyUser? CompanyUser { get; set; }
        public DateTime Created { get; set; }
        [JsonIgnore]
        public List<LikeTip> LikeTips { get; set; }  




        public DepartmentFavouriteTip SetDepartmentFavouriteTip(int departmentTipId, int userId)
        {
            DepartmentTipId= departmentTipId;
            CompanyUserId = userId;
            Created = DateTime.Now;
            return this;
        }
    }
}
