using AutoMapper;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
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
    public class GetCompanyUserQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<GetCompanyUserQueryHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly IMapper _mapper;
        private readonly ScenarioContext _scenariocontext;
        private readonly GetCompanyUserQueryHandler _handler;    
        public GetCompanyUserQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<GetCompanyUserQueryHandler>>();
            _companyUserRepository=new Mock<ICompanyUserRepository>();
            _scenariocontext=scenariocontext;
            if (_mapper == null) { var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }); IMapper mapper = mappingConfig.CreateMapper(); _mapper = mapper; }
            _handler = new GetCompanyUserQueryHandler(_logger.Object,_companyUserRepository.Object,_mapper); 

        }
        [Given(@"the command to get company user")]
        public void GivenTheCommandToGetCompanyUser()
        {
            var testuser = new Domain.Domain.CompanyUser().SetCompanyUserForUnitTest(1,"testuser","test@yahoo.com",1,1); 
            _companyUserRepository.Setup(x=>x.GetCompanyUserAsync("test@yahoo.com")).ReturnsAsync(testuser);  
           
        }

        [When(@"the command is handled to get user")]
        public async void WhenTheCommandIsHandledToGetUser()
        {
            var result = await _handler.Handle(new Business.Energie.Query.GetCompanyUserQuery { UserEmail = "test@yahoo.com" }, CancellationToken.None);
            _scenariocontext.Add("result",result);
           
        }

        [Then(@"user is retrieved sucessfully")]
        public void ThenUserIsRetrievedSucessfully()
        {
            var test = _scenariocontext.Get<Model.Response.CompanyUser>("result");
            Assert.IsNotNull(test);

           
        }
    }
}
