using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentCategoryListQueryHandler : IRequestHandler<DepartmentCategoryListQuery, DepartmentCategoryList>
    {
        private readonly ILogger<DepartmentCategoryListQueryHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;     
        private readonly ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> _translationService;
        public DepartmentCategoryListQueryHandler(ILogger<DepartmentCategoryListQueryHandler> logger, ICompanyUserRepository companyUserRepository,
            IEnergyAnalysisRepository energyAnalysisRepository,          
            ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> translationService)
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _energyAnalysisRepository = energyAnalysisRepository;
            _translationService = translationService;
        }
        public async Task<DepartmentCategoryList> Handle(DepartmentCategoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var departmentEnergyAnalysis = await _energyAnalysisRepository
                                                     .GetAllDepartmentUserEnergyAnalyses((int)user.DepartmentID);

                int activeusers = departmentEnergyAnalysis.Select(x => x.CompanyUserID).Distinct().Count();
                var departmentUser = await _companyUserRepository.GetCompanyUserByDepartmentId((int)user.DepartmentID);
                var departmentAnalysis = await _energyAnalysisRepository.GetAllEnergyAnalysisQuestions();
                int alluser = departmentUser.Count();
                var departmentCategory = new List<DepartmentCategory>();              

                if (request.Language == "en-US")
                {
                    var translatedData = await _translationService.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>(request.Language);

                    foreach (var analysis in departmentAnalysis)
                    {
                        int countOfResponse = departmentEnergyAnalysis.Where(x => x.EnergyAnalysisQuestionsID == analysis.Id).Count();
                        int percentage = (int)Math.Round((double)(100 * countOfResponse) / activeusers, 2);
                        if (percentage >= 50)
                        {
                            var translatedAnalysis = translatedData.FirstOrDefault(t => t.Id == analysis.Id);
                            if (translatedAnalysis != null)
                            {
                                departmentCategory.Add(new DepartmentCategory
                                {
                                    Name = translatedAnalysis.Name,
                                    Description = translatedAnalysis.Description,
                                    Id = analysis.Id,
                                    ImageUrl = analysis.ImageUrl
                                });
                            }
                        }
                    }
                }

                else
                {
                    foreach (var analysis in departmentAnalysis)
                    {
                        int countOfResponse = departmentEnergyAnalysis.Where(x => x.EnergyAnalysisQuestionsID == analysis.Id).Count();
                        int precentage = (int)Math.Round((double)(100 * countOfResponse) / activeusers, 2);
                        if (precentage >= 50)
                        {
                            departmentCategory.Add(new DepartmentCategory
                            {
                                Name = analysis.Name,
                                Description = analysis.Description,
                                Id = analysis.Id,
                                ImageUrl = analysis.ImageUrl
                            });
                        }
                    }
                }
                return new DepartmentCategoryList { DepartmentCategories = departmentCategory };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error Occured In {{0}}");
                _logger.LogError(ex, $"fout opgetreden in {{0}}");
                throw;
            }
        }
    }
}
