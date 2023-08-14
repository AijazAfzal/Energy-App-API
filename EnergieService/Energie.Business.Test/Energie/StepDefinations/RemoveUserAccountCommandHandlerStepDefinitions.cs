using Energie.Business.Energie.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Energie.Model.Response;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class RemoveUserAccountCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<RemoveUserAccountCommandHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<ICreateCompanyUserRepository> _createCompanyUserRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly RemoveUserAccountCommandHandler _removeUserAccountCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public RemoveUserAccountCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<RemoveUserAccountCommandHandler>>();
            _createCompanyUserRepository = new Mock<ICreateCompanyUserRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _scenarioContext = scenariocontext;
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _removeUserAccountCommandHandler = new RemoveUserAccountCommandHandler(_logger.Object,_companyUserRepository.Object,_createCompanyUserRepository.Object, _localizer.Object); 
            
        }
        [Given(@"the command to delete Energy App User")]
        public void GivenTheCommandToDeleteEnergyAppUser()
        {
            var testuser = new Domain.Domain.CompanyUser().SetCompanyUserForUnitTest(1,"testuser","test@gmail.com",1,1); 
            _companyUserRepository.Setup(x=>x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser); 
            _createCompanyUserRepository.Setup(x=>x.DeleteUserAccountAsync(1));
            _localizer.Setup(x => x["User_successfully_removed", testuser.Id]).Returns(new LocalizedString("User_successfully_removed", "User successfully removed"));

        }

        [When(@"the command is handled to delete account")]
        public async void WhenTheCommandIsHandledToDeleteAccount()
        {
            var result = await _removeUserAccountCommandHandler.Handle(new Business.Energie.Command.RemoveUserAccountCommand {UserEmail="test@gmail.com" },CancellationToken.None); 
            _scenarioContext.Add("result",result); 
        }

        [Then(@"the user account is deleted sucessfully")]
        public void ThenTheUserAccountIsDeletedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("User successfully removed"));  
        }
           
    }
}
