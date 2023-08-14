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
    public class UserEnergyScoreAverageQueryHandlerStepDefinitions : BaseClass
    {
        private readonly Mock<ILogger<UserEnergyScoreAverageQueryHandler>> _logger;
        private readonly Mock<IEnergyScoreRepository> _energyScoreRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly UserEnergyScoreAverageQueryHandler _userEnergyScoreAverageQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.Month>> _translationService;
        public UserEnergyScoreAverageQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _energyScoreRepository = new Mock<IEnergyScoreRepository>();
            _logger = new Mock<ILogger<UserEnergyScoreAverageQueryHandler>>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.Month>>();
            _userEnergyScoreAverageQueryHandler = new UserEnergyScoreAverageQueryHandler(_logger.Object, _energyScoreRepository.Object, _translationService.Object);
        }


        #region negative scenario
        //When UserEmail is null 
        [Given(@"the Query to retrieve all months and userEnergy score")]
        public void GivenTheQueryToRetrieveAllMonthsAndUserEnergyScore()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            var newuser = new Domain.Domain.CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "testuser@gmail.com", 1,1);
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("")).ReturnsAsync(new List<Domain.Domain.EnergyScore> { });
        }

        [When(@"the Query is handled to all months and userEnergy score")]
        public async void WhenTheQueryIsHandledToAllMonthsAndUserEnergyScore()
        {
            var result = await _userEnergyScoreAverageQueryHandler.Handle(new UserEnergyScoreAverageQuery
            {
               UserEmail = ""
            }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"userEnergy energy score is null")]
        public void ThenUserEnergyEnergyScoreIsNull()
        {
            var test = _scenariocontext.Get<EnergyScoreAverage>("result");
            Assert.IsNull(test);
        }

        #endregion

        #region positive scenario
        //when userEmail is not null
        [Given(@"the query to get all month and EnergyScore for user")]
        public void GivenTheQueryToGetAllMonthAndEnergyScoreForUser()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            var newuser = new Domain.Domain.CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "testuser@gmail.com", 1,1); 
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("testuser@gmail.com")).ReturnsAsync(GetListOfEnergyScore);
        }

        [When(@"the query is handled to all months and energyScore for user")]
        public async void WhenTheQueryIsHandledToAllMonthsAndEnergyScoreForUser()
        {
            var result = await _userEnergyScoreAverageQueryHandler.Handle(new UserEnergyScoreAverageQuery
            {
                UserEmail = "testuser@gmail.com"
            }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"energyScore is not null")]
        public void ThenEnergyScoreIsNotNull()
        {
            var test = _scenariocontext.Get<EnergyScoreAverage>("result");
            Assert.IsNotNull(test);
        }


        #endregion

        #region Scenario 3rd and 4th

        //when user has only One month score

        [Given(@"the query to get all month and one month EnergyScore for user")]
        public void GivenTheQueryToGetAllMonthAndOneMonthEnergyScoreForUser()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            var newuser = new Domain.Domain.CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "testuser@gmail.com", 1,1);
            var userScore = GetListOfEnergyScore();
            userScore = userScore.Take(1).ToList();
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("testuser@gmail.com")).ReturnsAsync(userScore);
        }


        //when user has only two month score
        [Given(@"the query to get all month and two month EnergyScore for user")]
        public void GivenTheQueryToGetAllMonthAndTwoMonthEnergyScoreForUser()
        {
            _energyScoreRepository.Setup(x => x.GetAllMonth()).ReturnsAsync(GetListOfmonths());
            var newuser = new Domain.Domain.CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "testuser@gmail.com", 1,1); 
            var userScore = GetListOfEnergyScore();
            userScore = userScore.Take(2).ToList(); 
            _energyScoreRepository.Setup(x => x.GetAllEnergyScores("testuser@gmail.com")).ReturnsAsync(userScore);
        }


        #endregion
    }
}
