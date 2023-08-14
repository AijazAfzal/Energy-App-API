using Energie.Model.Response;
using MediatR;

namespace Energie.Business.SuperAdmin.Query
{
    public record TipListQuery : IRequest<EnergyTipList>
    {
    }
}
