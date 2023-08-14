using Energie.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Energie.Business.CompanyAdmin.Command
{
    public class UpdateEmployerHelpForDeparmentCommand : IRequest<ResponseMessage>
    {
        [Required]
        public int Id { get; set; }
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
