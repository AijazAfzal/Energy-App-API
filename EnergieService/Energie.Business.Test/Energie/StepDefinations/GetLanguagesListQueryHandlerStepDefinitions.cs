using AutoMapper;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class GetLanguagesListQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<GetLanguagesListQueryHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly IMapper _mapper;
        private readonly ScenarioContext _scenariocontext; 
        private readonly GetLanguagesListQueryHandler _handler; 
        public GetLanguagesListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<GetLanguagesListQueryHandler>>();
            _scenariocontext = scenariocontext;
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            if (_mapper == null) { var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }); IMapper mapper = mappingConfig.CreateMapper(); _mapper = mapper; }
            _handler = new GetLanguagesListQueryHandler(_logger.Object,_companyUserRepository.Object,_mapper); 
        }
        [Given(@"the command to get list of languages")]
        public void GivenTheCommandToGetListOfLanguages()
        {
            var language=new Domain.Domain.Language().SetLanguage("en","french"); 
            _companyUserRepository.Setup(x=>x.GetLanguagesAsync()).ReturnsAsync(new List<Domain.Domain.Language>() { language });  
           
        }

        [When(@"the command is handled")]
        public async void WhenTheCommandIsHandled()
        {
            var result = await _handler.Handle(new Business.Energie.Query.GetLanguagesListQuery { },CancellationToken.None);
            _scenariocontext.Add("result",result); 
           
        }

        [Then(@"languages are retrieved sucessfully")]
        public void ThenLanguagesAreRetrievedSucessfully()
        {
            var test = _scenariocontext.Get<LanguageList>("result");
            Assert.IsNotNull(test);  
            
        }
    }
}
