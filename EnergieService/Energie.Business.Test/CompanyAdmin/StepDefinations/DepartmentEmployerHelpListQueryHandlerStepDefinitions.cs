using Energie.Business.CompanyAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class DepartmentEmployerHelpListQueryHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpRepository> _companyHelpRepository;
        private readonly Mock<ICompanyAdminRepository> _addCompanyAdminRepository;
        private readonly Mock<ILogger<DepartmentEmployerHelpListQueryHandler>> _logger;
        private readonly ScenarioContext _scenariocontext;
        private readonly DepartmentEmployerHelpListQueryHandler _departmentEmployerHelpListQueryHandler;


        //private readonly ICompanyHelpRepository _companyHelpRepository;
        //private readonly ICompanyAdminRepository _companyAdminRepository;
        //private readonly ILogger<DepartmentEmployerHelpListQueryHandler> _logger;


        public DepartmentEmployerHelpListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _companyHelpRepository = new Mock<ICompanyHelpRepository>();
            _addCompanyAdminRepository = new Mock<ICompanyAdminRepository>();
            _logger = new Mock<ILogger<DepartmentEmployerHelpListQueryHandler>>();
            _departmentEmployerHelpListQueryHandler = new DepartmentEmployerHelpListQueryHandler(_companyHelpRepository.Object, _addCompanyAdminRepository.Object, _logger.Object);

        }
        [Given(@"the command to retrieve employer help list")]
        public void GivenTheCommandToRetrieveEmployerHelpList()
        {
            var department = new Domain.Domain.Department().SetCompanyDepartment("hr", 1);
            var companyadmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1, "newadmin", "admin@gmail.com");
            _addCompanyAdminRepository.Setup(x => x.GetCompanyAdminByEmailAsync("admin@gmail.com")).ReturnsAsync(companyadmin);
            var helpcategory = new HelpCategory().SetHelpCategory("newhelpcategory", "descpp");
            var companydepartmenthelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("codepthelp", "descp", "cont", "reqvia", "moreinfo", 1, 1);
            companydepartmenthelp.Department = department;
            companydepartmenthelp.HelpCategory = helpcategory;
            _companyHelpRepository.Setup(x => x.DepartmentEmployerHelpListAsync(1)).ReturnsAsync(new List<CompanyDepartmentHelp>() { companydepartmenthelp });

        }

        [When(@"the command is handled to get the listt")]
        public async void WhenTheCommandIsHandledToGetTheListt()
        {
            var result = await _departmentEmployerHelpListQueryHandler.Handle(new Business.CompanyAdmin.Query.DepartmentEmployerHelpListQuery { UserEmail = "admin@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"the list is retirevd sucesssfully")]
        public void ThenTheListIsRetirevdSucesssfully()
        {
            var test = _scenariocontext.Get<DepartmentEmployerHelpList>("result");
            Assert.IsNotNull(test);

        }
    }
}
