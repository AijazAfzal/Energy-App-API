using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class UserEnergyScoreQueryHandlerStepDefinitions : BaseClass
    {
        private readonly Mock<ILogger<UserEnergyScoreQueryHandler>> _logger;
        private readonly Mock<IEnergyScoreRepository> _energyScoreRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly UserEnergyScoreQueryHandler _userEnergyScoreQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.Month>> _translationService;
        public UserEnergyScoreQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _energyScoreRepository = new Mock<IEnergyScoreRepository>();
            _logger = new Mock<ILogger<UserEnergyScoreQueryHandler>>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.Month>>();
            _userEnergyScoreQueryHandler = new UserEnergyScoreQueryHandler(_logger.Object, _energyScoreRepository.Object, _translationService.Object);
        }
        #region negative scenario
        // When UserEmail is null
        [Given(@"the Query to retrieve energy score")]
        public void GivenTheQueryToRetrieveEnergyScore()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("")).ReturnsAsync(new List<Domain.Domain.EnergyScore> { });
        }

        [When(@"the Query is handled to get energy score")]
        public async void WhenTheQueryIsHandledToGetEnergyScore()
        {
            var result = await _userEnergyScoreQueryHandler.Handle(new UserEnergyScoreQuery { UserEmail = "" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"energy score is null")]
        public void ThenEnergyScoreIsNull()
        {
            var test = _scenariocontext.Get<ListEnergyScore>("result");
            Assert.IsNull(test);
        }
        #endregion

        #region positive scenario
        //When UserEmail is not null 
        [Given(@"the Query to retrieve months and user energy score")]
        public void GivenTheQueryToRetrieveMonthsAndUserEnergyScore()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("testuser@gmail.com"))
                                  .ReturnsAsync(GetListOfEnergyScore);
        }

        [When(@"the Query is handled to get months and user energy score")]
        public async void WhenTheQueryIsHandledToGetMonthsAndUserEnergyScore()
        {
            var result = await _userEnergyScoreQueryHandler.Handle(new UserEnergyScoreQuery { UserEmail = "testuser@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"energy score is not null")]
        public void ThenEnergyScoreIsNotNull()
        {
            var response = _scenariocontext.Get<ListEnergyScore>("result");
            Assert.IsNotNull(response);
        }

        #endregion       
    }
}
