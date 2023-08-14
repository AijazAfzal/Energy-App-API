using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class UpdateTipByUserCommand : IRequest<ResponseMessage>
    {
        public int Id { get; set; }
        public int categoryId { get; set; }
        public string description { get; set; }
    }
}
