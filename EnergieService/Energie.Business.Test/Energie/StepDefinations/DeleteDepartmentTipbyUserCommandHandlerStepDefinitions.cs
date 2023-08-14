using Energie.Business.Energie.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
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
    public class DeleteDepartmentTipbyUserCommandHandlerStepDefinitions
    {
        private readonly Mock<IUserDepartmentTipRepository> _userDepartmentTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<ILogger<DeleteDepartmentTipbyUserCommandHandler>> _logger;
        private readonly ScenarioContext _scenarioContext;
        private readonly DeleteDepartmentTipbyUserCommandHandler _deleteDepartmentTipbyUserCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public DeleteDepartmentTipbyUserCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _userDepartmentTipRepository= new Mock<IUserDepartmentTipRepository>();
            _logger = new Mock<ILogger<DeleteDepartmentTipbyUserCommandHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _deleteDepartmentTipbyUserCommandHandler = new DeleteDepartmentTipbyUserCommandHandler(
                _userDepartmentTipRepository.Object,
                _logger.Object,
                _companyUserRepository.Object, _localizer.Object); 

        }
        [Given(@"the command to delete tip by user")]
        public void GivenTheCommandToDeleteTipByUser()
        {
            _userDepartmentTipRepository.Setup(x=>x.DeleteUserDepartmentTipAsync(1));
            _localizer.Setup(x => x["Department_tip_removed"]).Returns(new LocalizedString("Department_tip_removed", "Department tip removed"));
        }

        [When(@"the command is handled to delete  tip")]
        public async void WhenTheCommandIsHandledToDeleteTip()
        {
            var result = await _deleteDepartmentTipbyUserCommandHandler.Handle(new Business.Energie.Command.DeleteDepartmentTipbyUserCommand { Id=1},CancellationToken.None);
            _scenarioContext.Add("result",result); 
           
        }

        [Then(@"the tip is deleted sucessfully")]
        public void ThenTheTipIsDeletedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Department tip removed"));  

        }
    }
}
