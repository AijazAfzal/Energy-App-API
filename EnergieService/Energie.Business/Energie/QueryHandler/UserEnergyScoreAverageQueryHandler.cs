using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class UserEnergyScoreAverageQueryHandler : IRequestHandler<UserEnergyScoreAverageQuery, EnergyScoreAverage>
    {
        private readonly ILogger<UserEnergyScoreAverageQueryHandler> _logger;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ITranslationsRepository<Domain.Domain.Month> _translationService;
        public UserEnergyScoreAverageQueryHandler(ILogger<UserEnergyScoreAverageQueryHandler> logger
            , IEnergyScoreRepository energyScoreRepository, ITranslationsRepository<Domain.Domain.Month> translationService)
        {
            _logger = logger;
            _energyScoreRepository = energyScoreRepository;
            _translationService = translationService;
        }

        public async Task<EnergyScoreAverage> Handle(UserEnergyScoreAverageQuery request, CancellationToken cancellationToken)
        {
            EnergyScoreAverage energyScoreAverage = new EnergyScoreAverage();
            try
            {
                
                var months = await _energyScoreRepository.GetAllMonth();
                if (request.Language == "en-US")
                {
                    var translatedMonth = await _translationService.GetTranslatedDataAsync<Domain.Domain.Month>(request.Language);
                    months = translatedMonth;

                }
                var energyScore = await _energyScoreRepository.GetAllEnergyScores(request.UserEmail);
                DateTime datetime = DateTime.Now;
                var currentMonth = datetime.Month;
                var currentYear = datetime.Year;
                var listOFmonths = new List<EnergyScore>();
                int count = 0;
                if(energyScore.Count > 1)
                {
                    for (int i = currentMonth; count < 12; i--)
                    {
                        if (i == 0)
                        {
                            i = 12;
                            currentYear = currentYear - 1;
                        }
                        count += 1;
                        var filtermonth = months.Where(x => x.Id == i).FirstOrDefault();
                        if (filtermonth != null)
                        {
                            var filterScore = energyScore.Where(x => x.MonthId == i && x.Year == currentYear).Select(x => x.Score).FirstOrDefault();
                            listOFmonths.Add(new EnergyScore
                            {
                                score = filterScore,
                                month = filtermonth.Name,
                            });
                        }
                    }
                    double energyScores = 0;
                    int countOfEnergyScore = 0;
                    foreach (var monthScore in listOFmonths)
                    {
                        if (monthScore.score != 0)
                        {
                            energyScores += monthScore.score;
                            countOfEnergyScore += 1;
                        }
                    }
                    if (countOfEnergyScore > 1 && countOfEnergyScore <= 12)
                    {
                        double average = energyScores / (double)countOfEnergyScore;
                        energyScoreAverage.UserAverageScore = Math.Round(average, 2);
                        return energyScoreAverage;
                    }
                    else
                    {
                        return energyScoreAverage;
                    }
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(UserEnergyScoreAverageQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(UserEnergyScoreAverageQueryHandler));
                throw; 
            }
        }
    }
}
