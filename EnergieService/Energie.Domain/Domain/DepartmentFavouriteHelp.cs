using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class DepartmentFavouriteHelp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "CompanyDepartmentHelps")]
        public virtual int CompanyDepartmentHelpId { get; private set; }
        [ForeignKey("CompanyDepartmentHelpId")]
        public virtual CompanyDepartmentHelp CompanyDepartmentHelps { get; set; }
        [Required]
        [Display(Name = "CompanyUser")]
        public virtual int? CompanyUserId { get; private set; }
        [ForeignKey("CompanyUserId")]
        public virtual CompanyUser? CompanyUser { get; set; }
        public DateTime CreatedDate { get; set; }


        public DepartmentFavouriteHelp SetDepartmentFavouriteHelp(int helpId, int userID)
        {
            CompanyDepartmentHelpId = helpId;
            CompanyUserId = userID;
            CreatedDate = DateTime.Now;
            return this;
        }
    }
}
