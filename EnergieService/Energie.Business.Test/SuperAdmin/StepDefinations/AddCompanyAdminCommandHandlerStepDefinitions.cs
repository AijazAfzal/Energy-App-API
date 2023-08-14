using AutoMapper;
using Energie.Business.SuperAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class AddCompanyAdminCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddCompanyAdminCommandHandler>> _logger;
        private readonly Mock<ICompanyAdminRepository> _addCompanyAdminRepository;
        private readonly Mock<ICompanyRepository<Company>> _companyRepository;
        private readonly ScenarioContext _scenarioContext;
        private Mock<IMapper> _mapper;
        private readonly AddCompanyAdminCommandHandler addCompanyAdminCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public AddCompanyAdminCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<AddCompanyAdminCommandHandler>>();
            _addCompanyAdminRepository = new Mock<ICompanyAdminRepository>();
            _companyRepository = new Mock<ICompanyRepository<Company>>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            addCompanyAdminCommandHandler = new AddCompanyAdminCommandHandler(_logger.Object, _addCompanyAdminRepository.Object, _companyRepository.Object,_localizer.Object);

        }
        [Given(@"the command to add Company Admin")]
        public void GivenTheCommandToAddCompanyAdmin()
        {
            var newcompany =  Company.Create("abc");
            var company = _companyRepository.Setup(x => x.GetCompanyByID(1)).ReturnsAsync(newcompany);
            var setcompanyadmin = new AddB2CCompanyAdmin().SetCompanyAdmin(1, newcompany.Name, "Test", "Test@gmail.com");
            _addCompanyAdminRepository.Setup(x => x.CreateCompanyAdmin(setcompanyadmin));
            _localizer.Setup(x => x["Company_administrator_added"]).Returns(new LocalizedString("Company_administrator_added", "Company administrator added"));
        }

        [When(@"the command is handled to add Company Admin")]
        public async void WhenTheCommandIsHandledToAddCompanyAdmin()
        {
            var result = await addCompanyAdminCommandHandler.Handle(new Business.SuperAdmin.Command.AddCompanyAdminCommand
            {
                CompanyId = 1,
                Email = "Test@gmail.com",
                UserName = "Test"
            }
            , CancellationToken.None);
            _scenarioContext.Add("result", result);

        }

        [Then(@"Company Admin added sucessfully")]
        public void ThenCompanyAdminAddedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Company administrator added")); 

        }
    }
}
