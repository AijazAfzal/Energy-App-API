using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Command
{
    public class AddEmployerHelpForDeparmentCommand : IRequest<ResponseMessage>
    {
        [Required]
        public int HelpCategoryId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Contribution { get; set; }
        [Required]
        public string Requestvia { get; set; }
        [Required]
        public string MoreInformation { get; set; }
    }
}
