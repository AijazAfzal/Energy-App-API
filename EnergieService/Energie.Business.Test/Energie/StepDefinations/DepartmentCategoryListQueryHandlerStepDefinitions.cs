using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using CompanyUser = Energie.Domain.Domain.CompanyUser;
using EnergyAnalysis = Energie.Domain.Domain.EnergyAnalysis;
using EnergyAnalysisQuestions = Energie.Domain.Domain.EnergyAnalysisQuestions;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class DepartmentCategoryListQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<DepartmentCategoryListQueryHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly DepartmentCategoryListQueryHandler _departmentCategoryListQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions>> _translationService;
        public DepartmentCategoryListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _logger = new Mock<ILogger<DepartmentCategoryListQueryHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions>>();
            _departmentCategoryListQueryHandler = new DepartmentCategoryListQueryHandler(_logger.Object, _companyUserRepository.Object, _energyAnalysisRepository.Object, _translationService.Object);

        }
        //Department Category List retrieved sucessfully
        [Given(@"the command to retrieve category list")]
        public void GivenTheCommandToRetrieveCategoryList()
        {
            var user = new CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user);
            var energyanalysis = new EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energyanalysisquestions = new EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("newanalysisquestions", "descp", energyanalysis.Id);
            energyanalysisquestions.EnergyAnalysis = energyanalysis;
            var userenergyanalyis = new UserEnergyAnalysis().SetUserEnergyAnalysis(energyanalysisquestions.Id, user.Id);
            userenergyanalyis.CompanyUser = user;
            userenergyanalyis.EnergyAnalysisQuestions = energyanalysisquestions;

            _energyAnalysisRepository.Setup(x => x.GetAllDepartmentUserEnergyAnalyses(1)).ReturnsAsync(new List<UserEnergyAnalysis>() { userenergyanalyis });
            _companyUserRepository.Setup(x => x.GetCompanyUserByDepartmentId(1)).ReturnsAsync(new List<CompanyUser>() { user });
            _energyAnalysisRepository.Setup(x => x.GetAllEnergyAnalysisQuestions()).ReturnsAsync(new List<EnergyAnalysisQuestions>() { energyanalysisquestions });

        }

        [When(@"the command is handled to get department Category List")]
        public async void WhenTheCommandIsHandledToGetDepartmentCategoryList()
        {
            var result = await _departmentCategoryListQueryHandler.Handle(new Business.Energie.Query.DepartmentCategoryListQuery {UserEmail= "abc@gmail.com" },CancellationToken.None);
            _scenariocontext.Add("result",result); 
             
        }

        [Then(@"list is retrived sucessfully")]
        public void ThenListIsRetrivedSucessfully()
        {
            var test = _scenariocontext.Get<DepartmentCategoryList>("result"); 
            Assert.IsNotNull(test);

        }

        //Translated Department Category List retrieved sucessfully
        [Given(@"The command to retrieve translated category list")]
        public void GivenTheCommandToRetrieveTranslatedCategoryList()
        {
            var user = new CompanyUser().SetCompanyUserForUnitTest(1,"newuser", "abc@gmail.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user);
            var energyanalysis = new EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energyanalysisquestions = new EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("newanalysisquestions", "descp", energyanalysis.Id);
            energyanalysisquestions.EnergyAnalysis = energyanalysis;
            var userenergyanalyis = new UserEnergyAnalysis().SetUserEnergyAnalysis(energyanalysisquestions.Id, user.Id);
            userenergyanalyis.CompanyUser = user;
            userenergyanalyis.EnergyAnalysisQuestions = energyanalysisquestions;

            _energyAnalysisRepository.Setup(x => x.GetAllDepartmentUserEnergyAnalyses(1)).ReturnsAsync(new List<UserEnergyAnalysis>() { userenergyanalyis });
            _companyUserRepository.Setup(x => x.GetCompanyUserByDepartmentId(1)).ReturnsAsync(new List<CompanyUser>() { user });
            _energyAnalysisRepository.Setup(x => x.GetAllEnergyAnalysisQuestions()).ReturnsAsync(new List<EnergyAnalysisQuestions>() { energyanalysisquestions });

            var translatedData = new List<Domain.Domain.EnergyAnalysisQuestions>()
            {
                new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("Translated analysis questions", "Description", energyanalysis.Id)
            };

            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>("en-US")).ReturnsAsync(translatedData);

        }

        [When(@"The command is handled to get translated department Category List")]
        public async void WhenTheCommandIsHandledToGetTranslatedDepartmentCategoryList()
        {
            var result = await _departmentCategoryListQueryHandler.Handle(new Business.Energie.Query.DepartmentCategoryListQuery { UserEmail = "abc@gmail.com",Language = "en-US" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"Translated list is retrived sucessfully")]
        public void ThenTranslatedListIsRetrivedSucessfully()
        {
            var test = _scenariocontext.Get<DepartmentCategoryList>("result");
            Assert.IsNotNull(test);
        }

    }
}
