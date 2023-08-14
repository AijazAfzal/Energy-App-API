using Energie.Business.SuperAdmin.Query;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.SuperAdmin.QueryHandler
{
    public class TipListQueryHandler : IRequestHandler<TipListQuery, EnergyTipList>
    {
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly ILogger<TipListQueryHandler> _logger;
        public TipListQueryHandler(IEnergyTipRepository energyTipRepository,
            ILogger<TipListQueryHandler> logger)
        {
            _energyTipRepository = energyTipRepository;
            _logger = logger;
        }
        public async Task<EnergyTipList> Handle(TipListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tipList = await _energyTipRepository.GetEnergyTipAsync();
                var list = tipList.Select
                                    (x => new Model.Response.EnergyTip
                                    {
                                        sourceId = (int)x.EnergyAnalysisQuestions.EnergyAnalysisID,
                                        categoryId = (int)x.EnergyAnalysisQuestionsId,
                                        Id = x.Id,
                                        Name = x.Name,
                                        Description = x.Description,
                                        CreatedDate = x.CreatedOn.Date
                                    }).ToList();
                return new EnergyTipList { EnergyTips = list };
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(TipListQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(TipListQueryHandler));
                throw;
            }
        }
    }
}
