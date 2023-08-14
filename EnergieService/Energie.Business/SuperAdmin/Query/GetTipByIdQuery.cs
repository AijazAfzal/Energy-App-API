using Energie.Model.Response;
using MediatR;

namespace Energie.Business.SuperAdmin.Query
{
    public record GetTipByIdQuery : IRequest<EnergyTip>
    {
        public int id { get; set; }
    }
}
