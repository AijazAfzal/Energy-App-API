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
    public class UserFavouriteHelp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "CompanyHelp")]
        public virtual int? CompanyHelpID { get; private set; }
        [ForeignKey("CompanyHelpID")]
        public virtual CompanyHelp? CompanyHelp { get; set; }
        [Required]
        [Display(Name = "CompanyUser")]
        public virtual int? CompanyUserId { get; private set; }
        [ForeignKey("CompanyUserId")]
        public virtual CompanyUser? CompanyUser { get; set; }
        [Required]
        public DateTime CreatedOn { get; private set; }

        public UserFavouriteHelp SetUserFavouriteHelp(int companyHelpId, int companyUserId)
        {
            CompanyHelpID = companyHelpId;
            CompanyUserId = companyUserId;
            CreatedOn = DateTime.Now;
            return this;
        }
    }
}
