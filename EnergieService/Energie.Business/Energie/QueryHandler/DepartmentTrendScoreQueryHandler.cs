using Energie.Business.Energie.Query;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentTrendScoreQueryHandler : IRequestHandler<DepartmentTrendScoreQuery, TrendScoreResponse>
    {
        private readonly ILogger<DepartmentTrendScoreQueryHandler> _logger;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ITranslationsRepository<Domain.Domain.Month> _translationService;
        public DepartmentTrendScoreQueryHandler(ILogger<DepartmentTrendScoreQueryHandler> logger
            , IEnergyScoreRepository energyScoreRepository
            , ICompanyUserRepository companyUserRepository
            , ITranslationsRepository<Domain.Domain.Month> translationService)
        {
            _companyUserRepository = companyUserRepository;
            _logger = logger;
            _energyScoreRepository = energyScoreRepository;
            _translationService = translationService;
        }
        public async Task<TrendScoreResponse> Handle(DepartmentTrendScoreQuery request, CancellationToken cancellationToken)
        {
            var departmentTrendScore = new TrendScoreResponse();
            try
            {
                DateTime datetime = DateTime.Now;
                var currentMonth = datetime.Month;
                var currentYear = datetime.Year;
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var months = await _energyScoreRepository.GetAllMonth();
                if(request.Language == "en-US")
                {
                    var translatedMonth = await _translationService.GetTranslatedDataAsync<Domain.Domain.Month>(request.Language);
                    months = translatedMonth;

                }
                var departmentScore = await _energyScoreRepository.GetAllDepartmentEnergyScores((int)user.DepartmentID);
                var activeuser = departmentScore.Select(c => c.CompanyUserID).Distinct().Count();
                var departmentUser = await _companyUserRepository.GetCompanyUserByDepartmentId((int)user.DepartmentID);
                var responsebe = departmentScore.Where(x => x.Year == currentYear && x.MonthId == currentMonth);
                var today = DateTime.Today;
                var monthStart = new DateTime(today.Year, today.Month, 1);
                var adddays = monthStart.AddDays(13);
                var response = responsebe.Where(x => x.CreatedOn.Date <= adddays.Date).Select(x => x.CreatedOn);

                if (activeuser >= 5 && response.Count() >= 5)
                {
                    var energyScoresList = new List<EnergyScoreForTrend>();
                    int count = 0;
                    for (int i = currentMonth; count < 18; i--)
                    {
                        if (i == 0)
                        {
                            i = 12;
                            currentYear -= 1;
                        }
                        count += 1;
                        var filtermonth = months.Where(x => x.Id == i).FirstOrDefault();
                        if (filtermonth != null)
                        {
                            filtermonth.Name = filtermonth.Name.Substring(0, 3);
                            var filterscore = departmentScore.Where(x => x.MonthId == i && x.Year == currentYear).Select(x => x.Score).ToList();
                            var total = filterscore.Sum();
                            double num3 = (double)total / filterscore.Count();
                            var filterYear = departmentScore.Where(x => x.MonthId == i && x.Year == currentYear).Select(x => x.Year).FirstOrDefault();
                            if (filterYear == 0)
                            {
                                filterYear = currentYear;
                            }
                            energyScoresList.Add(new EnergyScoreForTrend
                            {
                                score = num3,
                                month = filtermonth.Name,
                                year = filterYear
                            });
                        }
                    }
                    if (energyScoresList.Count == 18)
                    {
                        double energyScores = 0;
                        int countOfEnergyScore = 0;
                        var firstAverage = energyScoresList.Take(6).ToList();
                        foreach (var monthScore in firstAverage)
                        {
                            if (monthScore.score != 0)
                            {
                                energyScores += monthScore.score;
                                countOfEnergyScore += 1;
                            }
                        }
                        if (countOfEnergyScore >= 2)
                        {
                            departmentTrendScore.resentAverage = Math.Round(energyScores / (double)countOfEnergyScore, 2);
                        }
                        else
                        {
                            departmentTrendScore.resentAverage = 0;
                        }
                        energyScores = 0;
                        countOfEnergyScore = 0;
                        var secondaveragescore = energyScoresList.Skip(12).Take(6).ToList();
                        foreach (var score2 in secondaveragescore)
                        {
                            if (score2.score != 0)
                            {
                                energyScores += score2.score;
                                countOfEnergyScore += 1;
                            }
                        }
                        if (countOfEnergyScore >= 2)
                        {
                            departmentTrendScore.lastAverage = Math.Round(energyScores / (double)countOfEnergyScore, 2);
                        }
                        else
                        {
                            departmentTrendScore.lastAverage = 0;
                        }
                        departmentTrendScore.difference = departmentTrendScore.resentAverage - departmentTrendScore.lastAverage;
                        var lastScore = energyScoresList.Skip(17).Take(1).FirstOrDefault();
                        if (lastScore.score == 0)
                            return default;

                        departmentTrendScore.EnergyScoreForTrends = energyScoresList;
                        return departmentTrendScore;
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(DepartmentTrendScoreQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(DepartmentTrendScoreQueryHandler)); 
                throw;
            }

        }
    }
}
