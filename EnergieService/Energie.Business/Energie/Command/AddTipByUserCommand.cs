using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class AddTipByUserCommand : IRequest<ResponseMessage>
    {
        public int categoryId { get; set; }
        public string description { get; set; }
        public string UserEmail { get; set; }
    }
}
