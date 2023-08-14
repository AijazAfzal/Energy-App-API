using AutoMapper;
using Energie.Api;
using Energie.Business.SuperAdmin.QueryHandler;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.SuperAdmin.StepDefinations
{
    [Binding]
    public class CompanyListQueryHandlerStepDefinitions
    {
        private readonly Mock<ICompanyRepository<Domain.Domain.Company>> _companyRepository;
        private readonly Mock<ILogger<CompanyListQueryHandler>> _logger;
        private readonly ScenarioContext _scenarioContext;
        private readonly CompanyListQueryHandler _companyListQueryHandler;
        private readonly IMapper _mapper;
        public CompanyListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _companyRepository = new Mock<ICompanyRepository<Domain.Domain.Company>>();
            _logger = new Mock<ILogger<CompanyListQueryHandler>>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper(); _mapper = mapper;
            }
            _companyListQueryHandler = new CompanyListQueryHandler(_companyRepository.Object, _logger.Object, _mapper);

        }
        [Given(@"the command to retreive company list")]
        public void GivenTheCommandToRetreiveCompanyList()
        {

            _companyRepository.Setup(x => x.CompanyListAsync())
                                           .ReturnsAsync(new List<Domain.Domain.Company> {  Domain.Domain.Company.Create("abc") });

        }

        [When(@"the command is handled to get company list")]
        public async void WhenTheCommandIsHandledToGetCompanyList()
        {
            var result = await _companyListQueryHandler.Handle(new Business.SuperAdmin.Query.CompanyListQuery(), CancellationToken.None);
            _scenarioContext.Add("result", result);
        }

        [Then(@"the company list is retrieved")]
        public void ThenTheCompanyListIsRetrieved()
        {
            var test = _scenarioContext.Get<CompanyList>("result");
            Assert.IsNotNull(test);

        }
    }
}
