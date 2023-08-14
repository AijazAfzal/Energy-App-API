using AutoMapper;
using Energie.Api;
using Energie.Business.SuperAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.SuperAdmin.StepDefinations
{
    [Binding]
    public class GetEnergySourceQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<GetEnergySourceQueryHandler>> _logger;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly ScenarioContext _scenarioContext;
        private IMapper _mapper;
        private readonly GetEnergySourceQueryHandler _getEnergySourceQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysis>> _translationService;
        public GetEnergySourceQueryHandlerStepDefinitions(ScenarioContext scenarioco0ntext)
        {
            _logger = new Mock<ILogger<GetEnergySourceQueryHandler>>();
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            _scenarioContext = scenarioco0ntext;
            if (_mapper == null)

            {

                var mappingConfig = new MapperConfiguration(mc =>
                {

                    mc.AddProfile(new MappingProfile());

                });

                IMapper mapper = mappingConfig.CreateMapper();

                _mapper = mapper;

            }
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysis>>();

            _getEnergySourceQueryHandler = new GetEnergySourceQueryHandler(_logger.Object, _energyAnalysisRepository.Object, _mapper, _translationService.Object);


        }

        [Given(@"the command to retrieve the EnergyAnalysis List")]
        public void GivenTheCommandToRetrieveTheEnergyAnalysisList()
        {
            
            _energyAnalysisRepository.Setup(x => x.GetAllEnergySourceAsync()).ReturnsAsync(new List<Domain.Domain.EnergyAnalysis> { new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now) });   
            


        }

        [When(@"the command is handled to get the list")]
        public async void WhenTheCommandIsHandledToGetTheList()
        {
            var result = await _getEnergySourceQueryHandler.Handle(new Business.SuperAdmin.Query.GetEnergySourceQuery(),CancellationToken.None); 
            _scenarioContext.Add("result",result); 
           
        }

        [Then(@"the list is retrieved")]
        public void ThenTheListIsRetrieved()
        {
            var test = _scenarioContext.Get<EnergyAnalysisList>("result");
            Assert.IsNotNull(test); 
        }
    }
}
