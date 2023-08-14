using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using EnergyScore = Energie.Model.Response.EnergyScore;

namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentMonthlyScoreQueryHandler : IRequestHandler<DepartmentMonthlyScoreQuery, DepartmentMonthlyScore>
    {
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ILogger<DepartmentMonthlyScoreQueryHandler> _logger;
        private readonly ITranslationsRepository<Domain.Domain.Month> _translationService;
        public DepartmentMonthlyScoreQueryHandler(IEnergyScoreRepository energyScoreRepository
            , ICompanyUserRepository companyUserRepository
            , ILogger<DepartmentMonthlyScoreQueryHandler> logger
            , ITranslationsRepository<Domain.Domain.Month> translationService
            )
        {
            _energyScoreRepository = energyScoreRepository;
            _companyUserRepository = companyUserRepository;
            _logger = logger;
            _translationService = translationService;
        }

        public async Task<DepartmentMonthlyScore> Handle(DepartmentMonthlyScoreQuery request, CancellationToken cancellationToken)
        {
            try
            {
                DepartmentMonthlyScore departmentMonthlyScore = new DepartmentMonthlyScore();
                DateTime datetime = DateTime.Now;
                var currentMonth = datetime.Month;
                var currentYear = datetime.Year;
                var energyScore = new List<EnergyScore>();
                int count = 0;
                var department = await _energyScoreRepository.GetDepartmentIdByUser(request.UserEmail);
                var departmentScore = await _energyScoreRepository.GetDepartmentScoreList(department.Id);
                departmentMonthlyScore.activeusers = departmentScore.Select(x => x.CompanyUserID).Distinct().Count();
                var departmentUser = await _companyUserRepository.GetCompanyUserByDepartmentId(department.Id);
                var usercounts = departmentUser.Count;
                var months = await _energyScoreRepository.GetAllMonth();

                if(request.Language == "en-US")
                {
                    var translatedMonths = await _translationService.GetTranslatedDataAsync<Domain.Domain.Month>(request.Language);
                    months = translatedMonths;
                }
                var scoreCount = 0;

                if (departmentMonthlyScore.activeusers >= 5)
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
                            double filterscore = departmentScore.Where(x => x.MonthId == i && x.Year == currentYear).Sum(x => x.Score);
                            scoreCount = departmentScore.Where(x => x.MonthId == i && x.Year == currentYear).Count();
                            var monthScore = filterscore / (double)scoreCount;
                            monthScore = double.IsNaN(monthScore) ? 0 : monthScore;
                            monthScore = Convert.ToDouble(monthScore);
                            energyScore.Add(new EnergyScore
                            {

                                score = Math.Round(monthScore, 2),
                                month = filtermonth.Name.Substring(0, 3),
                            });

                        }
                    }

                    departmentMonthlyScore.alluser = usercounts;
                    departmentMonthlyScore.Percentage = Math.Round((double)(100 * departmentMonthlyScore.activeusers) / usercounts, 2);
                    departmentMonthlyScore.EnergyScores = energyScore;
                }
                else
                {
                    departmentMonthlyScore.alluser = usercounts;
                    departmentMonthlyScore.Percentage = Math.Round((double)(100 * departmentMonthlyScore.activeusers) / usercounts, 2);
                    departmentMonthlyScore.EnergyScores = energyScore;

                }
                return departmentMonthlyScore;

                
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(DepartmentMonthlyScoreQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(DepartmentMonthlyScoreQueryHandler)); 
                throw;
            }
        }
    }
}
