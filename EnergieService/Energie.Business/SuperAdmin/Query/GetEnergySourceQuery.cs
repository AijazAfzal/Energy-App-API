using Energie.Model.Response;
using MediatR;

namespace Energie.Business.SuperAdmin.Query
{
    public record GetEnergySourceQuery : IRequest<EnergyAnalysisList>
    {
        public string Language { get; set; }
    }
}
