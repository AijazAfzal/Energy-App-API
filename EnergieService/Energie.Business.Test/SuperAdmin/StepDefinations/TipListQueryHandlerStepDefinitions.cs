using Energie.Business.SuperAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.SuperAdmin.StepDefinations
{
    [Binding]
    public class TipListQueryHandlerStepDefinitions
    {
        private readonly Mock<IEnergyTipRepository> _energyTipRepository;
        private readonly Mock<ILogger<TipListQueryHandler>> _logger;
        private readonly ScenarioContext _scenarioContext;
        private readonly TipListQueryHandler _tipListQueryHandler;
        public TipListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _energyTipRepository = new Mock<IEnergyTipRepository>();
            _logger = new Mock<ILogger<TipListQueryHandler>>();
            _scenarioContext = scenariocontext;
            _tipListQueryHandler = new TipListQueryHandler(_energyTipRepository.Object, _logger.Object);


        }
        [Given(@"the command to retreive Energy Tipp List")]
        public void GivenTheCommandToRetreiveEnergyTippList()
        {
            var tip = new Domain.Domain.Tip().SetEnergyTip(1, "hjh", "lkl");
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("sdf", "sdf@gmail.com", 1);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("new analysis", DateTime.Now);
            var energyanalysisquestions = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("abc", "descp", 1);
            tip.EnergyAnalysisQuestions = energyanalysisquestions;
            _energyTipRepository.Setup(x => x.GetEnergyTipAsync())
                .ReturnsAsync(new List<Tip>() { tip });

        }

        [When(@"the command is handled to get Energy Tip List")]
        public async void WhenTheCommandIsHandledToGetEnergyTipList()
        {
            var result = await _tipListQueryHandler.Handle(new Business.SuperAdmin.Query.TipListQuery { }, CancellationToken.None);
            _scenarioContext.Add("result", result);

        }

        [Then(@"Energy Tip List is retrieved")]
        public void ThenEnergyTipListIsRetrieved()
        {
            var test = _scenarioContext.Get<EnergyTipList>("result");
            Assert.IsNotNull(test);
        }
    }
}
