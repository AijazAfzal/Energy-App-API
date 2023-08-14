using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{

    public class DepartmentEnergyAnalysisQueryHandler : IRequestHandler<DepartmentEnergyAnalysisQuery, DepartmentEnergyAnalysisRequestList>
    {
        private readonly IClaimService _tokenClaimService;
        private readonly ILogger<DepartmentAverageForUserQueryHandler> _logger;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> _translationService;
        public DepartmentEnergyAnalysisQueryHandler(IClaimService tokenClaimService
            , ILogger<DepartmentAverageForUserQueryHandler> logger
            , IEnergyAnalysisRepository energyAnalysisRepository
            , ICompanyUserRepository companyUserRepository
            , IEnergyScoreRepository energyScoreRepository
            , ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> translationService            )
        {
            _tokenClaimService = tokenClaimService;
            _logger = logger;
            _energyScoreRepository = energyScoreRepository;
            _energyAnalysisRepository = energyAnalysisRepository;
            _companyUserRepository = companyUserRepository;
            _translationService = translationService;
        }
        public async Task<DepartmentEnergyAnalysisRequestList> Handle(DepartmentEnergyAnalysisQuery request, CancellationToken cancellationToken)
        {
            var userEmail = _tokenClaimService.GetUserEmail();
            try
            {
                DepartmentEnergyAnalysisRequestList departmentEnergyAnalysisRequestList = new();
                var companyDepartment = await _energyScoreRepository.GetDepartmentIdByUser(userEmail);
                var departmentEnergyAnalysis = await _energyAnalysisRepository.GetAllDepartmentUserEnergyAnalyses(companyDepartment.Id);

                departmentEnergyAnalysisRequestList.activeusers = departmentEnergyAnalysis.Select(x => x.CompanyUserID).Distinct().Count();
                var departmentUser = await _companyUserRepository.GetCompanyUserByDepartmentId(companyDepartment.Id);
                var departmentAnalysis = await _energyAnalysisRepository.GetAllEnergyAnalysisQuestions();
                departmentEnergyAnalysisRequestList.alluser = departmentUser.Count();
                var departmentEnergyAnalysisRequest = new List<DepartmentEnergyAnalysisRequest>();
                departmentEnergyAnalysisRequestList.Percentage = (int)Math.Round((double)(100 * departmentEnergyAnalysisRequestList.activeusers) / departmentEnergyAnalysisRequestList.alluser, 2);
                var filledEnergyAnalysisUser = departmentEnergyAnalysis.Select(x => x.EnergyAnalysisQuestionsID).Distinct().ToList();

                if(request.Language == "en-US")
                {
                    var translatedEnergyAnalysisQuestions = await _translationService.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>(request.Language);

                    departmentAnalysis = translatedEnergyAnalysisQuestions;
                }


                if (departmentEnergyAnalysisRequestList.activeusers >= 5)
                {
                    foreach (var analysis in departmentAnalysis)
                    {
                        int countOfResponse = departmentEnergyAnalysis.Where(x => x.EnergyAnalysisQuestionsID == analysis.Id).Count();
                        int precentage = (int)Math.Round((double)(100 * countOfResponse) / departmentEnergyAnalysisRequestList.activeusers, 2);

                            departmentEnergyAnalysisRequest.Add(new DepartmentEnergyAnalysisRequest
                            {
                                energyAnalysis = analysis.EnergyAnalysis.Name,
                                energyAnalysisQuestions = analysis.Name,
                                Precentage = precentage,
                            });
                        
                    }
                    departmentEnergyAnalysisRequest.OrderByDescending(x => x.Precentage);
                    departmentEnergyAnalysisRequestList.DepartmentEnergyAnalysisRequests = departmentEnergyAnalysisRequest;
                    return departmentEnergyAnalysisRequestList;
                }
                else
                {
                    departmentEnergyAnalysisRequestList.DepartmentEnergyAnalysisRequests = departmentEnergyAnalysisRequest;
                    return departmentEnergyAnalysisRequestList;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error Occured In {{0}}", userEmail, ex.Message);
                _logger.LogError($"fout opgetreden in {{0}}", userEmail, ex.Message); 
                throw;
            }
        }
    }
}
