using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentAverageForUserQueryHandler : IRequestHandler<DepartmentAverageForUserQuery, DepartmentScoreAverage>
    {
        private readonly ILogger<DepartmentAverageForUserQueryHandler> _logger;
        private readonly IClaimService _tokenClaimService;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ITranslationsRepository<Domain.Domain.Month> _translationService;
        public DepartmentAverageForUserQueryHandler(IClaimService tokenClaimService
            , ILogger<DepartmentAverageForUserQueryHandler> logger
            , IEnergyScoreRepository energyScoreRepository
            , ICompanyUserRepository companyUserRepository
            , ITranslationsRepository<Domain.Domain.Month> translationService
             )
        {
            _tokenClaimService = tokenClaimService;
            _logger = logger;
            _energyScoreRepository = energyScoreRepository;
            _companyUserRepository = companyUserRepository;
            _translationService = translationService;

        }

        public async Task<DepartmentScoreAverage> Handle(DepartmentAverageForUserQuery request, CancellationToken cancellationToken)
        {
            var userEmail = _tokenClaimService.GetUserEmail();
            try
            {
                var departmentScoreAverage = new DepartmentScoreAverage();

                var months = await _energyScoreRepository.GetAllMonth();
                if(request.Language == "en-US")
                {
                    var translatedMonth = await _translationService.GetTranslatedDataAsync<Domain.Domain.Month>(request.Language);
                    months = translatedMonth;
                }
                var department = await _energyScoreRepository.GetDepartmentIdByUser(userEmail);
                var departmentScore = await _energyScoreRepository.GetAllDepartmentEnergyScores(department.Id);
                var activeuser = departmentScore.Select(c => c.CompanyUserID).Distinct().Count();
                var departmentUser = await _companyUserRepository.GetCompanyUserByDepartmentId(department.Id);
                var userCount = departmentUser.Count();
                DateTime datetime = DateTime.Now;
                var currentMonth = datetime.Month;
                var currentYear = datetime.Year;
                var listOFmonths = new List<DepartmentScoreAverageMonths>();
                int count = 0;
                if (activeuser >= 5)
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
                            var filterScore = departmentScore.Where(x => x.MonthId == i && x.Year == currentYear).Sum(x => x.Score);
                            var userCounts = departmentScore.Where(x => x.MonthId == i && x.Year == currentYear).Count();
                            double averageUserScore = filterScore / (double)userCounts;
                            listOFmonths.Add(new DepartmentScoreAverageMonths
                            {
                                score = (int)averageUserScore,
                                month = filtermonth.Name,
                            });
                        }
                    }
                    double departmentScores = 0;
                    int countOfEnergyScore = 0;
                    foreach (var monthScore in listOFmonths)
                    {
                        if (monthScore.score != 0)
                        {
                            departmentScores += monthScore.score;
                            countOfEnergyScore += 1;
                        }
                    }
                    if (countOfEnergyScore > 1 && countOfEnergyScore <= 12)
                    {
                        double average;
                        var lastScore = listOFmonths.Skip(11).Take(1).FirstOrDefault();
                        if (lastScore?.score == -2147483648 || lastScore?.score == 0)
                        {
                            average = departmentScores / (double)countOfEnergyScore;
                            departmentScoreAverage.alluser = userCount;
                            departmentScoreAverage.activeusers = activeuser;
                            departmentScoreAverage.Percentage = Math.Round((100 * activeuser) / (double)userCount);
                            return departmentScoreAverage;
                        }
                        average = departmentScores / (double)countOfEnergyScore;
                        departmentScoreAverage.AverageDepartmentScore = Math.Round(average, 2);
                        departmentScoreAverage.alluser = userCount;
                        departmentScoreAverage.activeusers = activeuser;
                        departmentScoreAverage.DepartmentScoreAverageMonths = listOFmonths;
                        departmentScoreAverage.Percentage = Math.Round((100 * activeuser) / (double)userCount);
                        return departmentScoreAverage;
                    }
                    return departmentScoreAverage;

                }
                departmentScoreAverage.alluser = userCount;
                departmentScoreAverage.activeusers = activeuser;
                departmentScoreAverage.Percentage = Math.Round((double)(activeuser * 100) / userCount, 2);
                return departmentScoreAverage;

            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error occured in {{0}}", userEmail, ex.Message);
                _logger.LogError($"fout opgetreden in {{0}}", userEmail, ex.Message);  
                throw;
            }

        }
    }
}
