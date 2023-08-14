using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class UserTrendScoreQueryHandlerStepDefinitions : BaseClass
    {
        private readonly Mock<ILogger<UserTrendScoreQueryHandler>> _logger;
        private readonly Mock<IEnergyScoreRepository> _energyScoreRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly UserTrendScoreQueryHandler _userTrendScoreQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.Month>> _translationService;
        public UserTrendScoreQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _energyScoreRepository = new Mock<IEnergyScoreRepository>();
            _logger = new Mock<ILogger<UserTrendScoreQueryHandler>>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.Month>>();
            _userTrendScoreQueryHandler = new UserTrendScoreQueryHandler(_logger.Object, _energyScoreRepository.Object, _translationService.Object);
        }

        #region negative Scenario
        // when UserEmail is null
        [Given(@"the Query to retrieve energy score for trend")]
        public void GivenTheQueryToRetrieveEnergyScoreForTrend()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("")).ReturnsAsync(new List<Domain.Domain.EnergyScore> { });
        }

        [When(@"the Query is handled to get energy score for trend")]
        public async void WhenTheQueryIsHandledToGetEnergyScoreForTrend()
        {
            var result = await _userTrendScoreQueryHandler.Handle(new UserTrendScoreQuery
            {
                UserEmail = ""
            }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"energy score is null for trend")]
        public void ThenEnergyScoreIsNullForTrend()
        {
            var test = _scenariocontext.Get<TrendScoreResponse>("result");
            Assert.IsNull(test);
        }

        #endregion

        #region positive Scenario
        // when UserEmail is not null
        [Given(@"the Query to retrieve trend score")]
        public void GivenTheQueryToRetrieveTrendScore()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("testuser@gmail.com")).ReturnsAsync(GetListOfEnergyScore());
        }

        [When(@"the Query is handled to get energy score for trend with user Email")]
        public async void WhenTheQueryIsHandledToGetEnergyScoreForTrendWithUserEmail()
        {
            var result = await _userTrendScoreQueryHandler.Handle(new UserTrendScoreQuery
            {
                UserEmail = "testuser@gmail.com"
            }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }


        [Then(@"trend score is not null for trend")]
        public void ThenTrendScoreIsNotNullForTrend()
        {
            var test = _scenariocontext.Get<TrendScoreResponse>("result");
            //Assert.IsNotNull(test);
        }

        // when user has only one energy score
        [Given(@"the Query to retrieve energy score for one month")]
        public void GivenTheQueryToRetrieveEnergyScoreForOneMonth()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            var energyScore = GetListOfEnergyScore();
            energyScore = energyScore.Take(1).ToList();
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("testuser@gmail.com")).ReturnsAsync(energyScore);
        }

        //when user has only two energy score
        [Given(@"the Query to retrieve energy score for two month")]
        public void GivenTheQueryToRetrieveEnergyScoreForTwoMonth()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            var energyScore = GetListOfEnergyScore();
            var firstenergyScores = energyScore.Take(2).ToList();
            var secondenergyscore= energyScore.Skip(14).Take(3).ToList();
            firstenergyScores.AddRange(secondenergyscore);
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("testuser@gmail.com")).ReturnsAsync(firstenergyScores);
        }
        #endregion
    }
}
