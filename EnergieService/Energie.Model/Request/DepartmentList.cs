using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class DepartmentList
    {
        public List<Department> Departments { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        // Foreign key   
        [Display(Name = "Company")]
        [Required]
        public virtual int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        [ForeignKey("AdminId")]
        public int CreatedBy { get;  set; }
        public virtual CompanyAdmin CompanyAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserCount { get; set; }
        //[JsonIgnore]
        //public List<CompanyUser> CompanyUser { get; set; }
    }
}
