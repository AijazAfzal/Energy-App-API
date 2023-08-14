using Energie.Model.Response;
using MediatR;

namespace Energie.Business.Energie.Query
{
    public record UserTrendScoreQuery : IRequest<TrendScoreResponse>
    {
        public string UserEmail { get; set; }
        public string Language { get; set; }    
    }
}
