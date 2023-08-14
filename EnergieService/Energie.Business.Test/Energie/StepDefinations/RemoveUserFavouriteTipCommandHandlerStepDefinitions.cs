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
    public class RemoveUserFavouriteTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<RemoveUserFavouriteTipCommandHandler>> _logger;
        private readonly Mock<IUserEnergyTipRepository> _userEnergyTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly RemoveUserFavouriteTipCommandHandler _removeUserFavouriteTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public RemoveUserFavouriteTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _logger = new Mock<ILogger<RemoveUserFavouriteTipCommandHandler>>();
            _userEnergyTipRepository= new Mock<IUserEnergyTipRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _removeUserFavouriteTipCommandHandler = new RemoveUserFavouriteTipCommandHandler(_logger.Object,_userEnergyTipRepository.Object,_companyUserRepository.Object, _localizer.Object ); 
           

            
        }

        //Removing Super Admin Favourite Tip
        [Given(@"the command to remove Super Admin favourite tip")]
        public void GivenTheCommandToRemoveSuperAdminFavouriteTip()
        {
            var user = new CompanyUser().SetCompanyUser("abc","abc@gmail.com",1);
            var requireduser = _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user); 
            _userEnergyTipRepository.Setup(x=>x.RemoveSuperAdminFavouriteTipAsync(1,1));
            _localizer.Setup(x => x["Favorite_tip_removed"]).Returns(new LocalizedString("Favorite_tip_removed", "Favorite tip removed"));
        }

        [When(@"the command is handled to remove favaourite tip")]
        public async void WhenTheCommandIsHandledToRemoveFavaouriteTip()
        {
            var result = await _removeUserFavouriteTipCommandHandler.Handle(new Business.Energie.Command.RemoveUserFavouriteTipCommand
                                { TipId=1,
                                  TipBy="SuperAdmin",
                                  UserEmail= "abc@gmail.com" 
            }
            ,CancellationToken.None);
            _scenariocontext.Add("result",result); 
           
        }

        [Then(@"the favourite tip is removed")]
        public void ThenTheFavouriteTipIsRemoved()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.AreEqual("Favorite tip removed", test.Message);
            Assert.IsNotNull(test); 

        }


        // Removing Employer Favourite Tip
        [Given(@"the command to remove Employer favourite tip")]
        public void GivenTheCommandToRemoveEmployerFavouriteTip()
        {
            var user = new CompanyUser().SetCompanyUser("xyz", "xyz@gmail.com", 2);
            var requireduser = _companyUserRepository.Setup(x => x.GetCompanyUserAsync("xyz@gmail.com")).ReturnsAsync(user);
            _userEnergyTipRepository.Setup(x=>x.RemoveEmployerFavouriteHelpAsync(2,2));
            _localizer.Setup(x => x["Favorite_tip_removed"]).Returns(new LocalizedString("Favorite_tip_removed", "Favorite tip removed"));

        }

        [When(@"the command is handled to remove the favaourite tip")]
        public async void WhenTheCommandIsHandledToRemoveTheFavaouriteTip() 
        {
            var result = await _removeUserFavouriteTipCommandHandler.Handle(new Business.Energie.Command.RemoveUserFavouriteTipCommand 
                               { TipId=2,
                                 TipBy="CompanyAdmin",
                                 UserEmail= "xyz@gmail.com"
                                }
            ,CancellationToken.None);
            _scenariocontext.Add("result",result); 
           
        }

        [Then(@"the favourite tip is deleted")]
        public void ThenTheFavouriteTipIsDeleted()
        {
            var test=_scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.AreEqual("Favorite tip removed", test.Message);
            Assert.IsNotNull(test); 


        }



        //Remove User favourite tip

        [Given(@"the command to remove User favourite tip")]
        public void GivenTheCommandToRemoveUserFavouriteTip()
        {
            var user = new CompanyUser().SetCompanyUser("ghk", "ghk@gmail.com", 3);
            var requireduser = _companyUserRepository.Setup(x => x.GetCompanyUserAsync("ghk@gmail.com")).ReturnsAsync(user);
            _userEnergyTipRepository.Setup(x=>x.RemoveUserFavouriteHelpAsync(3,3));
            _localizer.Setup(x => x["Favorite_tip_removed"]).Returns(new LocalizedString("Favorite_tip_removed", "Favorite tip removed"));

        }

        [When(@"the command is handled to remove user favaourite tip")]
        public async void WhenTheCommandIsHandledToRemoveUserFavaouriteTip()
        {
            var result = await _removeUserFavouriteTipCommandHandler.Handle(new Business.Energie.Command.RemoveUserFavouriteTipCommand
            {
                TipId = 3,
                TipBy = "User",
                UserEmail = "ghk@gmail.com" 
            }
           , CancellationToken.None);
            _scenariocontext.Add("result", result); 


        }

        [Then(@"the favourite tip is removed sucessfully")]
        public void ThenTheFavouriteTipIsRemovedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.AreEqual("Favorite tip removed", test.Message);
            Assert.IsNotNull(test); 
        }
    }
}
