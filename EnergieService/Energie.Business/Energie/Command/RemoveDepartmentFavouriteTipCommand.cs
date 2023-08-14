using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class RemoveDepartmentFavouriteTipCommand : IRequest<ResponseMessage>
    {
        public int DepartmentTipId { get; set; }

        public string TipBy { get; set; }

        public string UserEmail { get; set; }
    }
}
