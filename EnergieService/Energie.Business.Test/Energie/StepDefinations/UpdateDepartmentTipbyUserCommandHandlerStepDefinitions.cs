using Energie.Business.Energie.Command;
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
    public class UpdateDepartmentTipbyUserCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<UpdateDepartmentTipbyUserCommandHandler>> _logger;
        private readonly Mock<IUserDepartmentTipRepository> _userDepartmentTipRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly UpdateDepartmentTipbyUserCommandHandler _updateDepartmentTipbyUserCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public UpdateDepartmentTipbyUserCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _logger = new Mock<ILogger<UpdateDepartmentTipbyUserCommandHandler>>();
            _userDepartmentTipRepository= new Mock<IUserDepartmentTipRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _updateDepartmentTipbyUserCommandHandler = new UpdateDepartmentTipbyUserCommandHandler(_logger.Object,_userDepartmentTipRepository.Object, _localizer.Object); 
          
        }

        //Tip updated sucessfully   
        [Given(@"the command to update department tipp")]
        public void GivenTheCommandToUpdateDepartmentTipp()
        {
            var newtip = new UserDepartmentTip().SetUserDepartmentTip(1,"new tip descp",1);
           _userDepartmentTipRepository.Setup(x => x.GetUserDepartmentTipbyIdAsync(1)).ReturnsAsync(newtip);
            newtip.UpdateDepartmentTip("tip descp 1");
            _userDepartmentTipRepository.Setup(x=>x.UpdateUserDepartmentTipAsync(newtip));
            _localizer.Setup(x => x["User_departmenttip_with_id_updated", newtip.Id]).Returns(new LocalizedString("User_departmenttip_with_id_updated", "User departmenttip updated"));

        }

        [When(@"the command is handled to update tip")]
        public async void WhenTheCommandIsHandledToUpdateTip()
        {
            var result = await _updateDepartmentTipbyUserCommandHandler.Handle(new Business.Energie.Command.UpdateDepartmentTipbyUserCommand 
                                { Id=1,
                                 Description= "tip descp 1" 

                                }
                                 ,CancellationToken.None); 
            _scenariocontext.Add("result",result); 

        }

        [Then(@"the tip is updated sucessfully")]
        public void ThenTheTipIsUpdatedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("User departmenttip updated"));  

        }



        //If tiptobeupdated is null the return something failed
        [Given(@"The command to check tiptobeupdated is null or not")]
        public void GivenTheCommandToCheckTiptobeupdatedIsNullOrNot()
        {         
             var newtip = new UserDepartmentTip().SetUserDepartmentTip(1, "new tip descp", 1);
            _userDepartmentTipRepository.Setup(x => x.GetUserDepartmentTipbyIdAsync(1)).ReturnsAsync(newtip);
            newtip.UpdateDepartmentTip("tip descp 1");
            _userDepartmentTipRepository.Setup(x => x.UpdateUserDepartmentTipAsync(newtip));
            _localizer.Setup(x => x["Something_failed"]).Returns(new LocalizedString("Something_failed", "Something failed"));

        }

        [When(@"The command is handled check tiptobeupdated is null or not")]
        public async void WhenTheCommandIsHandledCheckTiptobeupdatedIsNullOrNot()
        {
            var result = await _updateDepartmentTipbyUserCommandHandler.Handle(new UpdateDepartmentTipbyUserCommand(),CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"If null then return message something failed")]
        public void ThenIfNullThenReturnMessageSomethingFailed()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.NotNull(test);
            Assert.That(test.Message, Is.EqualTo("Something failed"));
        }

    }
}
