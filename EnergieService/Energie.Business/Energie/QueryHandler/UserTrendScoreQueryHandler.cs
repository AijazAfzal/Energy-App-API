using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{
    public class UserTrendScoreQueryHandler : IRequestHandler<UserTrendScoreQuery, TrendScoreResponse>
    {
        private readonly ILogger<UserTrendScoreQueryHandler> _logger;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ITranslationsRepository<Domain.Domain.Month> _translationService;
        public UserTrendScoreQueryHandler(ILogger<UserTrendScoreQueryHandler> logger
            , IEnergyScoreRepository energyScoreRepository, ITranslationsRepository<Domain.Domain.Month> translationService)
        {
            _logger = logger;
            _energyScoreRepository = energyScoreRepository;
            _translationService = translationService;
        }
        public async Task<TrendScoreResponse> Handle(UserTrendScoreQuery request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime now = DateTime.Now;
                int currentMonth = now.Month;
                int currentYear = now.Year;

                var monthList = await _energyScoreRepository.GetAllMonth();
                if(request.Language == "en-US")
                {
                    var translatedMonth = await _translationService.GetTranslatedDataAsync<Domain.Domain.Month>(request.Language);
                    monthList = translatedMonth;
                }
                var scoreList = await _energyScoreRepository.GetAllEnergyScores(request.UserEmail);

                var energyScoreForTrends = new List<EnergyScoreForTrend>();
                for (int i = 0; i < 18; i++)
                {
                    int monthIndex = currentMonth - i;
                    int year = currentYear;
                    int year2 = currentYear;
                    if (monthIndex <= 0)
                    {
                        monthIndex += 12;
                        year--;
                        year2--;
                        if (monthIndex <= 0 && year2 == 2022)
                        {
                            monthIndex += 12;
                            year2--;
                            year = year2;
                        }
                    }
                    var month = monthList.FirstOrDefault(m => m.Id == monthIndex);
                    if (month == null) continue;

                    var score = scoreList.FirstOrDefault(s => s.MonthId == monthIndex && s.Year == year);
                    if (score == null) continue;

                    energyScoreForTrends.Add(new EnergyScoreForTrend
                    {
                        score = score.Score,
                        month = month.Name.Substring(0, 3),
                        year = year
                    });

                    if (energyScoreForTrends.Count == 18) break;
                }

                if (energyScoreForTrends.Count != 18)
                {
                    return default;
                }

                double recentAverage = 0;
                int recentCount = 0;
                double lastAverage = 0;
                int lastCount = 0;

                foreach (var monthScore in energyScoreForTrends.Take(6))
                {
                    if (monthScore.score == 0) continue;
                    recentAverage += monthScore.score;
                    recentCount++;
                }

                foreach (var monthScore in energyScoreForTrends.Skip(12).Take(6))
                {
                    if (monthScore.score == 0) continue;
                    lastAverage += monthScore.score;
                    lastCount++;
                }

                if (recentCount >= 2 && lastCount >= 2)
                {
                    var trendScore = new TrendScoreResponse
                    {
                        resentAverage = Math.Round(recentAverage / recentCount, 2),
                        lastAverage = Math.Round(lastAverage / lastCount, 2),
                        difference = Math.Round(recentAverage / recentCount - lastAverage / lastCount, 2),
                        EnergyScoreForTrends = energyScoreForTrends
                    };

                    return trendScore;
                }
                else
                {
                    return default;
                }




                //DateTime datetime = DateTime.Now;
                //var currentMonth = datetime.Month;
                //var currentYear = datetime.Year;
                //var energyScoreForTrends = new List<EnergyScoreForTrend>();
                //var trendScore = new TrendScoreResponse();

                //var month = await _energyScoreRepository.GetAllMonth();
                //var score = await _energyScoreRepository.GetAllEnergyScores(request.UserEmail);
                //int count = 0;
                //if (score.Count >= 1)
                //{
                //    for (int i = currentMonth; count < 18; i--)
                //    {
                //        if (i == 0)
                //        {
                //            i = 12;
                //            currentYear -= 1;
                //        }
                //        count += 1;
                //        var filtermonth = month.Where(x => x.Id == i).FirstOrDefault();
                //        if (filtermonth != null)
                //        {
                //            var filterscore = score.Where(x => x.MonthId == i && x.Year == currentYear).Select(x => x.Score).FirstOrDefault();
                //            var filterYear = score.Where(x => x.MonthId == i && x.Year == currentYear).Select(x => x.Year).FirstOrDefault();
                //            if (filterYear == 0)
                //            {
                //                filterYear = currentYear;
                //            }
                //            energyScoreForTrends.Add(new EnergyScoreForTrend
                //            {
                //                score = filterscore,
                //                month = filtermonth.Name.Substring(0, 3),
                //                year = filterYear
                //            });
                //        }
                //    }

                //    if (energyScoreForTrends.Count == 18)
                //    {
                //        double energyScores = 0;
                //        int countOfFirstEnergyScore = 0;
                //        var firstAverage = energyScoreForTrends.Take(6).ToList();
                //        foreach (var monthScore in firstAverage)
                //        {
                //            if (monthScore.score != 0)
                //            {
                //                energyScores += monthScore.score;
                //                countOfFirstEnergyScore += 1;
                //            }
                //        }
                //        if (countOfFirstEnergyScore >= 2)
                //        {
                //            trendScore.resentAverage = Math.Round(energyScores / (double)countOfFirstEnergyScore, 2);
                //        }

                //        energyScores = 0;
                //        int countOfEnergyScore = 0;
                //        var secondaveragescore = energyScoreForTrends.Skip(12).Take(6).ToList();
                //        foreach (var score2 in secondaveragescore)
                //        {
                //            if (score2.score != 0)
                //            {
                //                energyScores += score2.score;
                //                countOfEnergyScore += 1;
                //            }
                //        }
                //        if (countOfEnergyScore >= 2)
                //            trendScore.lastAverage = Math.Round(energyScores / (double)countOfEnergyScore, 2);

                //        if (countOfFirstEnergyScore >= 2 && countOfEnergyScore >= 2)
                //            trendScore.difference = trendScore.resentAverage - trendScore.lastAverage;

                //        else
                //        {
                //            trendScore.lastAverage = 0;
                //            trendScore.resentAverage = 0;
                //        }
                //        trendScore.EnergyScoreForTrends = energyScoreForTrends;
                //        return trendScore;
                //    }
                //    else
                //    {
                //        return default;
                //    }
                //}
                //else
                //{
                //    return default;
                //}
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(UserTrendScoreQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(UserTrendScoreQueryHandler)); 
                throw;
            }
        }
    }
}
