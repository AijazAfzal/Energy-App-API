using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Energie.Domain.Domain
{
    public class DepartmentEnergyPlan
    {
        [Key]
        public int Id { get; set; }

        public int FavouriteTipId { get; set; }
        [Required]
        [Display(Name = "TipType")]
        public virtual int TipTypeId { get; private set; }
        [ForeignKey("TipTypeId")]
        public virtual TipType? TipType { get; set; }
        [Display(Name = "CompanyUser")]
        public virtual int? ResponsiblePersonId { get; private set; }
        [Display(Name = "ResponsiblePersonId")]
        public virtual CompanyUser? ResponsiblePerson { get; set; }
        [Required]
        [Display(Name = "CompanyUser")]
        public virtual int CompanyUserId { get; private set; }
        [Required]
        [Display(Name = "CompanyUserId")]
        public virtual CompanyUser? CompanyUser { get; set; }
        [Required]
        public DateTime PlanEndDate { get; private set; }
        [Required]
        [Display(Name = "PlanStatus")]
        public virtual int PlanStatusId { get; private set; }
        [Required]
        [Display(Name = "PlanStatusId")]
        public virtual PlanStatus? PlanStatus { get; set; }
        [Required]
        public bool IsReminder { get; private set; }
        [Required]
        public DateTime CreatedDate { get; private set; }


        public DepartmentEnergyPlan SetDepartmentEnergyPlan(int favtipId, int tiptypeId, int userId, int responsiblePersonId, DateTime planenddate, bool isReminder)
        {
            FavouriteTipId = favtipId;
            TipTypeId = tiptypeId;
            CompanyUserId = userId;
            ResponsiblePersonId = responsiblePersonId;
            PlanEndDate = planenddate;
            PlanStatusId = 1;
            IsReminder = isReminder;
            CreatedDate = DateTime.Now;
            return this;
        }

        public DepartmentEnergyPlan SetDepartmentEnergyPlanforUnitTest(int id, int favtipId, int tiptypeId, int userId, DateTime planenddate, bool isReminder)
        {
            Id = id;
            FavouriteTipId = favtipId;
            TipTypeId = tiptypeId;
            CompanyUserId = userId;
            PlanEndDate = planenddate;
            PlanStatusId = 1;
            IsReminder = isReminder;
            CreatedDate = DateTime.Now;
            return this;
        }

        public DepartmentEnergyPlan UpdateDepartmentPlan(int statusId, bool isReminder, DateTime endDate)
        {
            PlanEndDate = endDate;
            PlanStatusId = statusId;
            IsReminder = isReminder;
            return this;
        }




    }
}
