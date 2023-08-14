using AutoMapper;
using Energie.Api;
using Energie.Business.CompanyAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class CompanyHelpByIdQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<CompanyHelpByIdQueryHandler>> _logger;
        private readonly Mock<ICompanyHelpRepository> _companyHelpTipRepository;
        private readonly Mock<ICompanyAdminRepository> _companyAdminRepository;
        private readonly IMapper _mapper;
        private readonly ScenarioContext _scenarioContext;


        private readonly CompanyHelpByIdQueryHandler _CompanyHelpTipByIdQueryHandler;
        public CompanyHelpByIdQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<CompanyHelpByIdQueryHandler>>();
            _companyHelpTipRepository = new Mock<ICompanyHelpRepository>();
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
            _scenarioContext = scenariocontext;
            _CompanyHelpTipByIdQueryHandler = new CompanyHelpByIdQueryHandler(
                _logger.Object,
                _companyHelpTipRepository.Object,
                _companyAdminRepository.Object,
                _mapper);



        }
        [Given(@"the command to retreive  Company HelpTip")]
        public void GivenTheCommandToRetreiveCompanyHelpTip()
        {
            var companyAdmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1, "Test User", "testuser@gmail.com");
            var companyAdminResponse = _companyAdminRepository
                                        .Setup(x=> x.GetCompanyAdminByEmailAsync("testuser@gmail.com"))
                                        .ReturnsAsync(companyAdmin);
            var companyhelp = new Domain.Domain.CompanyHelp().SetCompanyHelp
                                                               ("Test Help",
                                                                 "Test help descp",
                                                                 "Test cont",
                                                                 "Test req",
                                                                 "Test reqvia",
                                                                 1,
                                                                 1
                                                               );
            _companyHelpTipRepository.Setup(x => x.GetCompanyHelpByIdAsync(1, 1)).ReturnsAsync(companyhelp);

        }

        [When(@"the command is handled to retreive  Company HelpTip")]
        public async void WhenTheCommandIsHandledToRetreiveCompanyHelpTip()
        {
            var result = await _CompanyHelpTipByIdQueryHandler.Handle(
                new Business.CompanyAdmin.Query.CompanyHelpByIdQuery 
                { 
                    Id = 1, 
                    UserEmail = "testuser@gmail.com"
                }, CancellationToken.None);
            _scenarioContext.Add("result", result);

        }

        [Then(@"Company HelpTip is retrieved sucessfully")]
        public void ThenCompanyHelpTipIsRetrievedSucessfully()
        {
            var test = _scenarioContext.Get<Domain.Domain.CompanyHelp>("result");
            Assert.IsNotNull(test); 


        }

    }
}
