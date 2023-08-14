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
    public class DepartmentEmployerCategoryListQueryHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpCategoryRepository> _companyHelpCategoryRepository;
        private readonly Mock<ILogger<DepartmentEmployerCategoryListQueryHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly DepartmentEmployerCategoryListQueryHandler _departmentEmployerCategoryListQueryHandler;
        public DepartmentEmployerCategoryListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _companyHelpCategoryRepository = new Mock<ICompanyHelpCategoryRepository>();
            _logger = new Mock<ILogger<DepartmentEmployerCategoryListQueryHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _departmentEmployerCategoryListQueryHandler = new DepartmentEmployerCategoryListQueryHandler(_companyHelpCategoryRepository.Object, _logger.Object, _companyUserRepository.Object);

        }
        [Given(@"the command to get employer help list")]
        public void GivenTheCommandToGetEmployerHelpList()
        {
            var helpcategory = new HelpCategory().SetHelpCategory("newcategory", "new descp");
            _companyHelpCategoryRepository.Setup(x => x.GetEmployerHelpCategoriesAsync()).ReturnsAsync(new List<HelpCategory>() { helpcategory });
        }

        [When(@"the command is handled to get thee listt")]
        public async void WhenTheCommandIsHandledToGetTheeListt()
        {
            var result = await _departmentEmployerCategoryListQueryHandler.Handle(new Business.Energie.Query.DepartmentEmployerCategoryListQuery { }, CancellationToken.None);
            _scenariocontext.Add("result", result);

        }

        [Then(@"the list is returned")]
        public void ThenTheListIsReturned()
        {
            var test = _scenariocontext.Get<CompanyCategoryUserList>("result");
            Assert.IsNotNull(test);

        }
    }
}
