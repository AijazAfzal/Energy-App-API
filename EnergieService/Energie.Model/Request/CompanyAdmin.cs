using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class CompanyAdmin
  {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; } 
    [Required]
    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [StringLength(50)]
    public string CreatedBy { get; set; }
    [Required]
    [StringLength(50)]
    public DateTime CreatedOn { get; set; }

    [Required]
    [Display(Name = "Company")]
    public virtual int? CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public virtual Company? Company { get; set; }
  }
}
