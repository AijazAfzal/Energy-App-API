using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{
    public class UserEnergyScoreQueryHandler : IRequestHandler<UserEnergyScoreQuery, ListEnergyScore>
    {
        private readonly ILogger<UserEnergyScoreQueryHandler> _logger;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ITranslationsRepository<Domain.Domain.Month> _translationService;
        public UserEnergyScoreQueryHandler(ILogger<UserEnergyScoreQueryHandler> logger
            , IEnergyScoreRepository energyScoreRepository, ITranslationsRepository<Domain.Domain.Month> translationService)
        {
            _logger = logger;
            _energyScoreRepository = energyScoreRepository;
            _translationService = translationService;
        }
        public async Task<ListEnergyScore> Handle(UserEnergyScoreQuery request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime datetime = DateTime.Now;
                var currentMonth = datetime.Month;
                var currentYear = datetime.Year;
                var energyScore = new List<EnergyScore>();
                int count = 0;
                var month = await _energyScoreRepository.GetAllMonth();
                if (request.Language == "en-US")
                {
                    var translatedMonth = await _translationService.GetTranslatedDataAsync<Domain.Domain.Month>(request.Language);
                    month = translatedMonth;

                } 
                var score = await _energyScoreRepository.GetAllEnergyScores(request.UserEmail);
                var userId = score.Select(x => x.CompanyUserID).FirstOrDefault();
                if(score == null || score.Count == 0)
                {
                    return default;
                }
                for (int i = currentMonth; count < 12; i--)
                {
                    if (i == 0)
                    {
                        i = 12;
                        currentYear = currentYear - 1;
                    }
                    count += 1;
                    var filtermonth = month.Where(x => x.Id == i).FirstOrDefault();
                    if (filtermonth != null)
                    {
                        filtermonth.Name = filtermonth.Name.Substring(0, 3);
                        var filterscore = score.Where(x => x.MonthId == i && x.Year == currentYear && x.CompanyUserID == userId).Select(x => x.Score).FirstOrDefault();
                        energyScore.Add(new EnergyScore
                        {
                            score = filterscore,
                            month = filtermonth.Name,
                        });
                    }
                }
                return new ListEnergyScore { EnergyScores = energyScore };
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, $"Error occured in {{0}}", nameof(UserEnergyScoreQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(UserEnergyScoreQueryHandler)); 
                throw;
            }
        }
    }
}
