using Energie.Model.Response;
using MediatR;

namespace Energie.Business.SuperAdmin.Query
{
    public record GetEnergyAnalysisByIdQuery : IRequest<ListEnergyAnalysisQuestions>
    {
        public int Id { get; set; }
        public string Language { get; set; }
    }
}
