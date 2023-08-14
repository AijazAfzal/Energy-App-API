using Energie.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Energie.Business.Energie.Command
{
    public class DeleteDepartmentTipbyUserCommand : IRequest<ResponseMessage>
    {
        [Required]
        public int Id { get; set; }
        public string UserEmail { get; set; }

    }
}
