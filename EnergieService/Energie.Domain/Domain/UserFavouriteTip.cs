using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class UserFavouriteTip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tips")]
        public virtual int? TipId { get; private set; }
        [ForeignKey("TipId")]
        public virtual Tip? Tips { get; set; }
        [Required]
        [Display(Name = "CompanyUser")]
        public virtual int? CompanyUserId { get; private set; }
        [ForeignKey("CompanyUserId")]
        public virtual CompanyUser? CompanyUser { get; set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        
        public UserFavouriteTip SetUserFavouriteTip(int tipsId, int userId)
        {
            TipId = tipsId;
            CompanyUserId = userId;
            CreatedOn = DateTime.Now;
            return this;
        }

        public UserFavouriteTip SetUserFavouriteTipforUnitTest(int id,int tipsId, int userId)
        {
            Id = id; 
            TipId = tipsId;
            CompanyUserId = userId;
            CreatedOn = DateTime.Now;
            return this;
        }
    }
}
