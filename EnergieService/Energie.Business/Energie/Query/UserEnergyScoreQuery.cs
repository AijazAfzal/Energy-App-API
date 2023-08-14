using Energie.Model.Response;
using MediatR;

namespace Energie.Business.Energie.Query
{
    public record UserEnergyScoreQuery : IRequest<ListEnergyScore>
    {
        public string UserEmail { get; set; }
        public string Language { get;set; }
    }
}
