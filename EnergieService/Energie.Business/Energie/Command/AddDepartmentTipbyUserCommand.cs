using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class AddDepartmentTipbyUserCommand : IRequest<ResponseMessage>
    {
        public int categoryId { get; set; }
        public string Description { get; set; }

        public string UserEmail { get; set; }
    }
}
