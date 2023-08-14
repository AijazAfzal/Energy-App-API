using Energie.Business.Energie.Query;
using Energie.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class GetEnergyPlanListTestQueryHandler : IRequestHandler<GetEnergyPlanListTestQuery, List<object>>
    {
        private readonly IEnergyPlanRepository _energyPlanRepository;
        public GetEnergyPlanListTestQueryHandler(IEnergyPlanRepository energyPlanRepository)
        {
            _energyPlanRepository = energyPlanRepository;
        }
        public async Task<List<object>> Handle(GetEnergyPlanListTestQuery request, CancellationToken cancellationToken)
        {
            var response = await _energyPlanRepository.GetEnergyPlanAsync();
            return (List<object>)response;

        }
    }
}
