using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.Command
{
    public class UpdateDepartmentTipCommand : IRequest<ResponseMessage>
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
