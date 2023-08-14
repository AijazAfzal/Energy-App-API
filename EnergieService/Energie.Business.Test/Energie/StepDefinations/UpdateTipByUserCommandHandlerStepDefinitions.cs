using Energie.Business.Energie.Command;
using Energie.Business.Energie.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class UpdateTipByUserCommandHandlerStepDefinitions
    {
        private readonly Mock <ILogger<UpdateTipByUserCommandHandler>> _logger;
        private readonly Mock<IUserEnergyTipRepository> _userEnergyTipRepository;
        private readonly Mock <ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly UpdateTipByUserCommandHandler _updateTipByUserCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public UpdateTipByUserCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _logger = new Mock<ILogger<UpdateTipByUserCommandHandler>>();
            _userEnergyTipRepository = new Mock<IUserEnergyTipRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _updateTipByUserCommandHandler = new UpdateTipByUserCommandHandler(_logger.Object,_userEnergyTipRepository.Object,_companyUserRepository.Object, _localizer.Object); 
           
        }

        //Tip updated sucessfully by user
        [Given(@"the command to update tip")]
        public void GivenTheCommandToUpdateTip()
        {
            var newtip = new UserTip().SetUserTip(1,1,"newtip descp");
            _userEnergyTipRepository.Setup(x => x.GetUserFavouriteTipByIdAsync(1)).ReturnsAsync(newtip);
            var latesttip=newtip.UpdateUserTip(1,"latest descp");
            _userEnergyTipRepository.Setup(x => x.UpdateUserFavouriteTipAsync(newtip)).ReturnsAsync(latesttip);
            _localizer.Setup(x => x["User_tip_updated"]).Returns(new LocalizedString("User_tip_updated", "User tip updated"));

        }

        [When(@"the command is handled to update tipp")]
        public async void WhenTheCommandIsHandledToUpdateTipp()
        {
            var result = await _updateTipByUserCommandHandler.Handle(new Business.Energie.Command.UpdateTipByUserCommand { Id=1,categoryId=1,description= "latest descp" },CancellationToken.None);
            _scenariocontext.Add("result",result); 
           
        }

        [Then(@"the tipp is updated sucessfully")]
        public void ThenTheTippIsUpdatedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("User tip updated"));  

        }



        // Check tipupdatebyuser of UpdateTipByUserCommandHandler is null or not.If null then return message Something went wrong
        [Given(@"The command to check tipupdatebyuser is null or not")]
        public void GivenTheCommandToCheckTipupdatebyuserIsNullOrNot()
        {
            var newtip = new UserTip().SetUserTip(1, 1, "newtip descp");
            _userEnergyTipRepository.Setup(x => x.GetUserFavouriteTipByIdAsync(1)).ReturnsAsync(newtip);
            var latesttip = newtip.UpdateUserTip(1, "latest descp");
            _userEnergyTipRepository.Setup(x => x.UpdateUserFavouriteTipAsync(newtip));
            _localizer.Setup(x => x["Something_wrong"]).Returns(new LocalizedString("Something_wrong", "Something went wrong"));
        }

        [When(@"The command is handled to check tipupdatebyuser is null or not")]
        public async void WhenTheCommandIsHandledToCheckTipupdatebyuserIsNullOrNot()
        {
            var result = await _updateTipByUserCommandHandler.Handle(new Business.Energie.Command.UpdateTipByUserCommand { Id = 1, categoryId = 1, description = "latest descp" }, CancellationToken.None);
            _scenariocontext.Add("result", result);

        }

        [Then(@"If tipupdatebyuser is null the return Something went wrong")]
        public void ThenIfTipupdatebyuserIsNullTheReturnSomethingWentWrong()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.NotNull(test);
            Assert.That(test.Message, Is.EqualTo("Something went wrong"));
        }

    }
}
