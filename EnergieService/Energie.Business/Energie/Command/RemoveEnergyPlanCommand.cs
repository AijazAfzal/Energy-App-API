using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class RemoveEnergyPlanCommand : IRequest<ResponseMessage>
    {
        public int EnergyPlanId { get; set; }
    }
}
