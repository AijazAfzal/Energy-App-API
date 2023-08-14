using AutoMapper;
using Energie.Business.SuperAdmin.Query;
using Energie.Business.SuperAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.SuperAdmin.StepDefinations
{
    [Binding]
    public class CategoryListQueryHandlerStepDefinitions
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<CategoryListQueryHandler>> _logger;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly CategoryListQueryHandler _categoryListQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.UserEnergyAnalysis>> _translationService;


        public CategoryListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CategoryListQueryHandler>>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _scenarioContext = scenariocontext;
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.UserEnergyAnalysis>>();
            _categoryListQueryHandler = new CategoryListQueryHandler(
                  _mapper.Object
                , _logger.Object
                , _categoryRepository.Object
                , _energyAnalysisRepository.Object, _translationService.Object);

        }

        [Given(@"the command to get category list")]
        public void GivenTheCommandToGetCategoryList()
        {
            var newuser = new CompanyUser().SetCompanyUser("TestUser", "TestUser@gmail.com", 1);
            var energyanalysis = new EnergyAnalysis().SetEnergyAnalysis("TestEnergyAnalysis", DateTime.Now);
            var newenergyanalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(1, 1);
            newenergyanalysis.EnergyAnalysisQuestions = new EnergyAnalysisQuestions().
                                                            SetEnergyAnalysisQuestions("TestEnergyAnalysisQuestion", "TestEnergyAnalysisQuestion Description", 1);
            newenergyanalysis.CompanyUser = newuser;
            _energyAnalysisRepository.Setup(x => x.UserEnergyAnalysisAsync("TestUser@gmail.com"))
                .ReturnsAsync(new List<UserEnergyAnalysis>() { newenergyanalysis });

            
        }

        [When(@"the command is handled to get category list")]
        public async void WhenTheCommandIsHandledToGetCategoryList()
        {
            var result = await _categoryListQueryHandler.Handle(new CategoryListQuery { UserEmail = "TestUser@gmail.com" }, CancellationToken.None);
            _scenarioContext.Add("result", result);
        }

        [Then(@"the category list is retrieved")]
        public void ThenTheCategoryListIsRetrieved()
        {
            var test = _scenarioContext.Get<CategoryList>("result");
            Assert.IsNotNull(test);

        }



        [Given(@"The command to get translated category list")]
        public void GivenTheCommandToGetTranslatedCategoryList()
        {
            var newuser = new CompanyUser().SetCompanyUser("TestUser", "TestUser@gmail.com", 1);
            var energyanalysis = new EnergyAnalysis().SetEnergyAnalysis("TestEnergyAnalysis", DateTime.Now);
            var newenergyanalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(1, 1);
            newenergyanalysis.EnergyAnalysisQuestions = new EnergyAnalysisQuestions().
                                                            SetEnergyAnalysisQuestions("TestEnergyAnalysisQuestion", "TestEnergyAnalysisQuestion Description", 1);
            newenergyanalysis.CompanyUser = newuser;
            _energyAnalysisRepository.Setup(x => x.UserEnergyAnalysisAsync("TestUser@gmail.com"))
                .ReturnsAsync(new List<UserEnergyAnalysis>() { newenergyanalysis });

            var translatedData = new List<Domain.Domain.UserEnergyAnalysis>()
            {
                new Domain.Domain.UserEnergyAnalysis().SetUserEnergyAnalysis(1, 1)
            };
            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.UserEnergyAnalysis>("en-US")).ReturnsAsync(translatedData);
        }

        [When(@"The command is handled to get translated category list")]
        public async void WhenTheCommandIsHandledToGetTranslatedCategoryList()
        {
            var result = await _categoryListQueryHandler.Handle(new CategoryListQuery { UserEmail = "TestUser@gmail.com" }, CancellationToken.None);
            _scenarioContext.Add("result", result);
        }

        [Then(@"The translated category list is retrieved")]
        public void ThenTheTranslatedCategoryListIsRetrieved()
        {
            var test = _scenarioContext.Get<CategoryList>("result");
            Assert.IsNotNull(test);
        }

    }
}
