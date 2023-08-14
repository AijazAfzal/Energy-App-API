using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Energie.Domain.Domain
{
    public class Department
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        [StringLength(50)]
        public string Name { get; private set; }
        [Required]
        [Display(Name = "Company")]
        public virtual int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        [Required]
        [StringLength(50)]
        public DateTime CreatedOn { get; private set; }

        [JsonIgnore]
        public List<CompanyUser>? CompanyUser { get; set; }
        public Department SetCompanyDepartment(string name, int companyid)
        {
            Name = name;
            CompanyId = companyid;
            CreatedOn = DateTime.Now;
            return this;
        }
    }
}
