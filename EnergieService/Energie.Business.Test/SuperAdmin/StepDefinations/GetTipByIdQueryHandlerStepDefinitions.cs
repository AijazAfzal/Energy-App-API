using AutoMapper;
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
    public class GetTipByIdQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<GetTipByIdQueryHandler>> _logger;
        private readonly Mock<IEnergyTipRepository> _energyTipRepository;
        private readonly ScenarioContext _scenarioContext;
        private Mock<IMapper> _mapper;
        private readonly GetTipByIdQueryHandler _GetTipByIdQueryHandler;

        public GetTipByIdQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<GetTipByIdQueryHandler>>();
            _energyTipRepository = new Mock<IEnergyTipRepository>();
            _scenarioContext = scenariocontext;
            _mapper = new Mock<IMapper>();
            _GetTipByIdQueryHandler = new GetTipByIdQueryHandler(_logger.Object, _energyTipRepository.Object);

        }
        [Given(@"the command to get energy tip by Id")]
        public void GivenTheCommandToGetEnergyTipById()
        {
            var newenergytip = new Tip();
            _energyTipRepository.Setup(x => x.GetEnergyTipByIdAsync(1)).ReturnsAsync(newenergytip);


        }

        [When(@"the command is handled to get Energy Tip")]
        public async void WhenTheCommandIsHandledToGetEnergyTip()
        {
            var result = await _GetTipByIdQueryHandler.Handle(new Business.SuperAdmin.Query.GetTipByIdQuery { id = 1 }, CancellationToken.None);
            _scenarioContext.Add("result", result);

        }

        [Then(@"the Energy Tip is retireved sucessfully")]
        public void ThenTheEnergyTipIsRetirevedSucessfully()
        {
            var test = _scenarioContext.Get<EnergyTip>("result");
            Assert.IsNotNull(test);

        }
    }
}
