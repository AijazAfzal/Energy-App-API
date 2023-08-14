using Energie.Model.Request;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class AddEnergyAnalysisScoreCommand : IRequest<UserEnergyAnalysisResponseList>
    {
        public int[] EnergyAnalysisRecord { get; set; }
        public string UserEmail { get; set; }
    }
}
