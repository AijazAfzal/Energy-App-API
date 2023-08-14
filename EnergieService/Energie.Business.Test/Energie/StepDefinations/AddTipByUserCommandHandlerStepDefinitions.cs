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
    public class AddTipByUserCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddTipByUserCommandHandler>> _logger;
        private readonly Mock<IUserEnergyTipRepository> _userEnergyTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly AddTipByUserCommandHandler _addTipByUserCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public AddTipByUserCommandHandlerStepDefinitions(ScenarioContext scenariocontext) 
        {
            _scenariocontext = scenariocontext;
            _logger = new Mock<ILogger<AddTipByUserCommandHandler>>();
            _userEnergyTipRepository=new Mock<IUserEnergyTipRepository>(); 
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _addTipByUserCommandHandler = new AddTipByUserCommandHandler(_logger.Object,_userEnergyTipRepository.Object,_companyUserRepository.Object, _localizer.Object); 

        }
        [Given(@"the command to add tip by user")]
        public void GivenTheCommandToAddTipByUser()
        {
            var newuser = new CompanyUser().SetCompanyUser("user1","abc@gmail.com",1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(newuser);
            var newusertip = new UserTip().SetUserTip(1, 1,"tip descp");

            _localizer.Setup(x => x["User_tip_added"]).Returns(new LocalizedString("User_tip_added", "User tip added"));
            _userEnergyTipRepository.Setup(x=>x.AddUserFavouriteTipAsync(newusertip)); 



        }

        [When(@"the command is handled to add tip")]
        public async void WhenTheCommandIsHandledToAddTip()
        {
            var result = await _addTipByUserCommandHandler.Handle(new Business.Energie.Command.AddTipByUserCommand
            {
                categoryId = 1,
                description = "tip descp",
                UserEmail = "abc@gmail.com" 
            }
           ,CancellationToken.None);
           _scenariocontext.Add("result",result);

        }

        [Then(@"tip is added sucessfully")]
        public void ThenTipIsAddedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("User tip added")); 
        }
    }
}
