using Energie.Business.Energie.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class UpdateCompanyUserLanguageCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<UpdateCompanyUserLanguageCommandHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly UpdateCompanyUserLanguageCommandHandler _languageCommandHandler;
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public UpdateCompanyUserLanguageCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<UpdateCompanyUserLanguageCommandHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _languageCommandHandler = new UpdateCompanyUserLanguageCommandHandler(_logger.Object,_companyUserRepository.Object,
_localizer.Object); 
            _scenarioContext = scenariocontext; 
            
        }
        [Given(@"the command to update user language")]
        public void GivenTheCommandToUpdateUserLanguage()
        {
            var testuser = new CompanyUser().SetCompanyUserForUnitTest(1,"testuser","test@gmail.com",1,1);
            _companyUserRepository.Setup(x=>x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser);
            var updateduserlanguage = testuser.UpdateCompanyUserLanguage(2);
            _companyUserRepository.Setup(x=>x.UpdateCompanyUserLanguage(updateduserlanguage));
            _localizer.Setup(x => x["Language_user_updated"]).Returns(new LocalizedString("Language_user_updated", "Language for Company User updated sucessfully"));

        }

        [When(@"the command is handled to update user language")]
        public async void WhenTheCommandIsHandledToUpdateUserLanguage()
        {
            var result = await _languageCommandHandler.Handle(new Business.Energie.Command.UpdateCompanyUserLanguageCommand { LanguageId=2,UserEmail= "test@gmail.com" },CancellationToken.None);
            _scenarioContext.Add("result",result); 
            
        }

        [Then(@"the user language is updated sucessfully")]
        public void ThenTheUserLanguageIsUpdatedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Language for Company User updated sucessfully"));  

        }
    }
}
