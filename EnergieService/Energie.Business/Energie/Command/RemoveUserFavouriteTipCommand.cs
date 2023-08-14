using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class RemoveUserFavouriteTipCommand : IRequest<ResponseMessage>
    {
        public int TipId { get; set; }
        public string TipBy { get; set; }
        public string UserEmail { get; set; }
    }
}
