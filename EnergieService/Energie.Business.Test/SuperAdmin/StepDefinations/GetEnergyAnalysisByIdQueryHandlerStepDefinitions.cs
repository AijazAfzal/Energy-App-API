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
    public class GetEnergyAnalysisByIdQueryHandlerStepDefinitions
    {
        private readonly  IMapper _mapper;
        private readonly Mock <ILogger<GetEnergyAnalysisByIdQueryHandler>> _logger;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly GetEnergyAnalysisByIdQueryHandler _getEnergyAnalysisByIdQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions>> _translationsService;

        public GetEnergyAnalysisByIdQueryHandlerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            if (_mapper == null) { var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }); IMapper mapper = mappingConfig.CreateMapper(); _mapper = mapper; }
            _logger = new Mock<ILogger<GetEnergyAnalysisByIdQueryHandler>>();
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            _translationsService = new Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions>>();
            _getEnergyAnalysisByIdQueryHandler = new GetEnergyAnalysisByIdQueryHandler(_logger.Object,_energyAnalysisRepository.Object,_mapper, _translationsService.Object); 
        }

        //Energy Analysis Questions List retreived sucessfully
        [Given(@"the command to get Energy Analysis Questions List by Id")]
        public void GivenTheCommandToGetEnergyAnalysisQuestionsListById()
        {

            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("new analysis", DateTime.Now);
            var energyanalysisquestions = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("energyanalysisquestions","descp",1);
            energyanalysisquestions.EnergyAnalysis = energyanalysis;
            _energyAnalysisRepository.Setup(x => x.GetAnalysisQuestionsByIdAsync(1)).ReturnsAsync(new List<Domain.Domain.EnergyAnalysisQuestions>() { energyanalysisquestions });        
            
        }

        [When(@"the command is handled to retrieve the list")]
        public async void WhenTheCommandIsHandledToRetrieveTheList()
        {
            var result = await _getEnergyAnalysisByIdQueryHandler.Handle(new Business.SuperAdmin.Query.GetEnergyAnalysisByIdQuery { Id=1},CancellationToken.None);   
            _scenarioContext.Add("result",result); 
            
        }

        [Then(@"the Energy Analysis Questions By Id is retreived")]
        public void ThenTheEnergyAnalysisQuestionsByIdIsRetreived()
        {
            var test = _scenarioContext.Get<ListEnergyAnalysisQuestions>("result");  
            Assert.IsNotNull(test); 
            
        }

        //Translated Energy Analysis Questions List retreived sucessfully
        [Given(@"The command to get translated Energy Analysis Questions List by Id")]
        public void GivenTheCommandToGetTranslatedEnergyAnalysisQuestionsListById()
        {

            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("new analysis", DateTime.Now);
            var energyanalysisquestions = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("energyanalysisquestions", "descp", 1);
            energyanalysisquestions.EnergyAnalysis = energyanalysis;
            _energyAnalysisRepository.Setup(x => x.GetAnalysisQuestionsByIdAsync(1)).ReturnsAsync(new List<Domain.Domain.EnergyAnalysisQuestions>() { energyanalysisquestions });

            var translatedData = new List<Domain.Domain.EnergyAnalysisQuestions>()
            {
                new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("energyanalysisquestions", "descp", 1)
            };
            _translationsService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>("en-US")).ReturnsAsync(translatedData);
        }

        [When(@"The command is handled to retrieve the translated list")]
        public async void WhenTheCommandIsHandledToRetrieveTheTranslatedList()
        {
            var result = await _getEnergyAnalysisByIdQueryHandler.Handle(new Business.SuperAdmin.Query.GetEnergyAnalysisByIdQuery { Id = 1 }, CancellationToken.None);
            _scenarioContext.Add("result", result);
        }

        [Then(@"The translated Energy Analysis Questions By Id is retreived")]
        public void ThenTheTranslatedEnergyAnalysisQuestionsByIdIsRetreived()
        {
            var test = _scenarioContext.Get<ListEnergyAnalysisQuestions>("result");
            Assert.IsNotNull(test);

        }

    }
}
