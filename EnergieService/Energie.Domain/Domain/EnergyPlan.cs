using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Energie.Domain.Domain
{
    public class EnergyPlan
    {
        [Key]
        public int Id { get; set; }
        [Required]
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
        [Required]
        public DateTime UpdatedDate { get; private set; }
        public EnergyPlan SetEnergyPlan(  int favouriteTipId
                                        , int typeId
                                        , int UserId
                                        , int responsiblePersonId
                                        , DateTime endDate
                                        , bool isReminder )
        {
            FavouriteTipId = favouriteTipId;
            TipTypeId = typeId;
            CompanyUserId = UserId;
            ResponsiblePersonId = responsiblePersonId;
            PlanEndDate = endDate;
            PlanStatusId = 1;
            IsReminder = isReminder;
            CreatedDate = DateTime.Now;
            return this;
        }

        public EnergyPlan UpdateEnergyPlan(DateTime endDate, int statusId, bool isReminder)
        {
            PlanEndDate = endDate;
            PlanStatusId = statusId;
            IsReminder = isReminder;
            return this;
        }
    }
}
