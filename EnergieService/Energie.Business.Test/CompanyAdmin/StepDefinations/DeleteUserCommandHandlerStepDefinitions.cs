using AutoMapper;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Cryptography.X509Certificates;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class DeleteUserCommandHandlerStepDefinitions
    {
        private readonly Mock <ICreateCompanyUserRepository> _companyUserRepository;
        private readonly Mock <ILogger<DeleteUserCommandHandler>> _logger;
        private readonly ScenarioContext _scenarioContext;
        private readonly DeleteUserCommandHandler deleteUserCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public DeleteUserCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext= scenariocontext;
            _companyUserRepository=new Mock<ICreateCompanyUserRepository>();
            _logger = new Mock<ILogger<DeleteUserCommandHandler>>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            deleteUserCommandHandler = new DeleteUserCommandHandler(_companyUserRepository.Object,_logger.Object, _localizer.Object); 

        }
        [Given(@"the command to delete User")]
        public void GivenTheCommandToDeleteUser()
        {
            _companyUserRepository.Setup(x => x.DeleteUserAccountAsync(1));
            _localizer.Setup(x => x["User_deleted"]).Returns(new LocalizedString("User_deleted", "User deleted"));
        }

        [When(@"the command is handled to delete User")]
        public async void WhenTheCommandIsHandledToDeleteUser()
        {
            var result = await deleteUserCommandHandler.Handle(new Business.CompanyAdmin.Command.DeleteUserCommand { UserId=1},CancellationToken.None); 
            _scenarioContext.Add("result",result); 
            
        }

        [Then(@"User deleted sucessfully")]
        public void ThenUserDeletedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("User deleted"));   

        }
    }
}
