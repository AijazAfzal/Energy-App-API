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
    public class AddCompanyHelpCommand : IRequest<ResponseMessage>
    {
        [Required]
        public int CompanyHelpCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string OwnContribution { get; set; }
        [Required]
        public string Requestvia { get; set; }
        [Required]
        public string Conditions { get; set; }
        public string UserEmail { get; set; }
    }
}
