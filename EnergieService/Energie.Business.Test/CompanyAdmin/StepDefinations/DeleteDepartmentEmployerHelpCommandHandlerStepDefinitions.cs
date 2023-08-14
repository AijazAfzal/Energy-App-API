using Energie.Business.CompanyAdmin.Command;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class DeleteDepartmentEmployerHelpCommandHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpRepository> _companyHelpRepository;
        private readonly Mock<ILogger<DeleteDepartmentEmployerHelpCommandHandler>> _logger;
        private readonly Mock<ICompanyAdminRepository> _companyAdminRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly DeleteDepartmentEmployerHelpCommandHandler _deleteDepartmentEmployerHelpCommandHandler;
        private DeleteDepartmentEmployerHelpCommand _deleteDepartmentEmployerHelpCommand;
        private ResponseMessage _responseMessage;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public DeleteDepartmentEmployerHelpCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _companyHelpRepository = new Mock<ICompanyHelpRepository>();
            _companyAdminRepository = new Mock<ICompanyAdminRepository>();
            _logger = new Mock<ILogger<DeleteDepartmentEmployerHelpCommandHandler>>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _deleteDepartmentEmployerHelpCommandHandler = new DeleteDepartmentEmployerHelpCommandHandler(
                   _companyHelpRepository.Object
                 , _logger.Object
                 , _companyAdminRepository.Object, _localizer.Object);
        }
        [Given(@"the command to delete employer help")]
        public void GivenTheCommandToDeleteEmployerHelp()
        {
            var companyadmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1, "testuser", "test@gmail.com");
            _companyAdminRepository.Setup(x => x.GetCompanyAdminByEmailAsync("test@gmail.com")).ReturnsAsync(companyadmin);
            _companyHelpRepository.Setup(x=>x.DeleteDepartmentEmployerHelpListAsync(1, 1));
            _localizer.Setup(x => x["Help_removed"]).Returns(new LocalizedString("Help_removed", "Help removed"));
        }
        [When(@"the command is handled to delete help")]
        public async void WhenTheCommandIsHandledToDeleteHelp()
        {
            var result = await _deleteDepartmentEmployerHelpCommandHandler.Handle(new Business.CompanyAdmin.Command.DeleteDepartmentEmployerHelpCommand { Id=1,UserEmail= "test@gmail.com" },CancellationToken.None);
            _scenariocontext.Add("result",result); 
            
        }

        [Then(@"employer help is deleted sucessfully")]
        public void ThenEmployerHelpIsDeletedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Message, "Help removed");

        }
    }
}
