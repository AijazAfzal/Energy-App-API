using AutoMapper;
using Energie.Api;
using Energie.Business.CompanyAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class CompanyHelpListQueryHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpRepository> _companyHelpTipRepository;
        private readonly Mock<ILogger<CompanyHelpListQueryHandler>> _logger;
        private readonly Mock<ICompanyAdminRepository> _companyAdminRepository;
        private readonly IMapper _mapper;
        private readonly ScenarioContext _scenarioContext;
        private readonly CompanyHelpListQueryHandler _companyHelpTipListQueryHandler;
        private CompanyHelpList result;
        public CompanyHelpListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _companyHelpTipRepository = new Mock<ICompanyHelpRepository>();
            _logger = new Mock<ILogger<CompanyHelpListQueryHandler>>();
            _companyAdminRepository = new Mock<ICompanyAdminRepository>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _companyHelpTipListQueryHandler = new CompanyHelpListQueryHandler(
                _companyHelpTipRepository.Object,
                _logger.Object,
                _companyAdminRepository.Object,
                _mapper);

        }
        [Given(@"the command to get company help List")]
        public void GivenTheCommandToGetCompanyHelpList()
        {
            var company = Domain.Domain.Company.Create("ariqt");
            var companyAdmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1, "user", "abcde@gmail.com");
            var helpCategory = new HelpCategory().SetHelpCategory("mental health", "descp");
            var companyHelp = new Domain.Domain.CompanyHelp().SetCompanyHelp("newhelp", "new help descp", "cont", "req", "reqvia", 1, 1);
            companyHelp.Company = company;
            companyHelp.HelpCategory = helpCategory;

            var admin = _companyAdminRepository.Setup(x => x.GetCompanyAdminByEmailAsync("abcde@gmail.com")).ReturnsAsync(companyAdmin);
            _companyHelpTipRepository.Setup(x => x.GetCompanyHelpsAsync(1)).ReturnsAsync(new List<Domain.Domain.CompanyHelp>() { companyHelp });

        }
        [When(@"the command is handled to get List")]
        public async void WhenTheCommandHandledToGetList()
        {
             result = await _companyHelpTipListQueryHandler.Handle(
                new Business.CompanyAdmin.Query.CompanyHelpListQuery
                {
                    userEmail = "abcde@gmail.com"
                }, CancellationToken.None);
            _scenarioContext.Add("result", result);

        }
        [Then(@"the company help List is retreived")]
        public void ThenTheCompanyHelpListIsRetreived()
        {         
            Assert.IsNotNull(result);
            Assert.AreEqual("newhelp", result.CompanyHelpTips[0].Name);
            Assert.AreEqual("new help descp", result.CompanyHelpTips[0].Description);
            Assert.AreEqual("cont", result.CompanyHelpTips[0].OwnContribution);
            Assert.AreEqual(1, result.CompanyHelpTips[0].HelpCategoryId);
            Assert.AreEqual(1,result.CompanyHelpTips.Count); 

        }
    }
}
