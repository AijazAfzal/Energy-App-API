using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class UpdateDepartmentTipRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EnergyAnalysisQuestionId { get; set; } 
        [Required]
        public string DepartmentTip { get; set; }
        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
    }
}
