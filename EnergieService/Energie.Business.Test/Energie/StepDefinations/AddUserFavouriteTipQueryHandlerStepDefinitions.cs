using Azure.Core;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using TechTalk.SpecFlow;
using Energie.Infrastructure.Repository;
using Microsoft.Extensions.Localization;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddUserFavouriteTipQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddUserFavouriteTipQueryHandler>> _logger;
        private readonly Mock<IUserEnergyTipRepository> _userEnergyTipRepository;
        private readonly Mock<IClaimService> _tokenClaimService;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly AddUserFavouriteTipQueryHandler _addUserFavouriteTipQueryHandler;
        private ResponseMessage _respons;
        private AddUserFavouriteTipQuery _request;
        private UserFavouriteTip _userFavouriteTip;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public AddUserFavouriteTipQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<AddUserFavouriteTipQueryHandler>>();
            _userEnergyTipRepository = new Mock<IUserEnergyTipRepository>();
            _tokenClaimService = new Mock<IClaimService>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _scenariocontext = scenariocontext;
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _addUserFavouriteTipQueryHandler = new AddUserFavouriteTipQueryHandler(_logger.Object, _userEnergyTipRepository.Object, _tokenClaimService.Object, _companyUserRepository.Object, _localizer.Object);

        }
        [Given(@"the command to set user favourite tip")]
        public  void GivenTheCommandToSetUserFavouriteTip()
        {

            _request = new AddUserFavouriteTipQuery 
            {
                tipID = 1
            };


        }

        [When(@"the command is handled to set and return a response message")]
        public async void WhenTheCommandIsHandledToSetAndReturnAResponseMessage()
        {
            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("user@example.com");
            var user = new CompanyUser().SetCompanyUserForUnitTest(1, "Satya", "user@example.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);
            DateTime analysisDate = new DateTime(2023, 04, 12);
            var EnergyAnalysis = new EnergyAnalysis().SetEnergyAnalysis("Satya", analysisDate);

            var EnergyAnalysisQuestions = new EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("Satya", "gdvd", EnergyAnalysis.Id);

            var tip = new Tip().SetEnergyTip(EnergyAnalysisQuestions.Id, "Satya", "gdvd");

            var userfavouriteTip = new UserFavouriteTip().SetUserFavouriteTip(tip.Id, user.Id);
            _userEnergyTipRepository.Setup(x => x.GetFavouriteTipById(userfavouriteTip));
            _userEnergyTipRepository.Setup(x => x.SetUserFavouriteTip(userfavouriteTip));
            _localizer.Setup(x => x["Favorite_tip_added"]).Returns(new LocalizedString("Favorite_tip_added", "Favorite tip added"));
            _respons = await _addUserFavouriteTipQueryHandler.Handle(_request, CancellationToken.None);


        }

        [Then(@"User favourite tip is added and response message is returned")]
        public void ThenUserFavouriteTipIsAddedAndResponseMessageIsReturned()
        {

            Assert.True(_respons.IsSuccess);
            Assert.AreEqual("Favorite tip added",_respons.Message);

        }



        //Check Favourite Tip Already Exist
        [Given(@"The command to check user favourite tip is exist or not")]
        public void GivenTheCommandToCheckUserFavouriteTipIsExistOrNot()
        {
            _request = new AddUserFavouriteTipQuery
            {
                tipID = 1
            };
        }

        [When(@"The command is handled to Check user favourite tip is exist or not")]
        public async void WhenTheCommandIsHandledToCheckUserFavouriteTipIsExistOrNot()
        {
            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("user@example.com");
            var user = new CompanyUser().SetCompanyUserForUnitTest(1, "Satya", "user@example.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);
            DateTime analysisDate = new DateTime(2023, 04, 12);
            var EnergyAnalysis = new EnergyAnalysis().SetEnergyAnalysis("Satya", analysisDate);

            var EnergyAnalysisQuestions = new EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("Satya", "gdvd", EnergyAnalysis.Id);

            var tip = new Tip().SetEnergyTip(EnergyAnalysisQuestions.Id, "Satya", "gdvd");

            var userfavouriteTip = new UserFavouriteTip().SetUserFavouriteTip(_request.tipID, user.Id);

            _userEnergyTipRepository.Setup(x => x.GetFavouriteTipById(It.IsAny<UserFavouriteTip>()))
             .ReturnsAsync((UserFavouriteTip input) =>
             {

                 if (input.TipId == userfavouriteTip.TipId && input.CompanyUserId == userfavouriteTip.CompanyUserId)
                 {
                     return new UserFavouriteTip();

                 }
                 else
                 {
                     return null;
                 }
             });
            _localizer.Setup(x => x["Favorite_tip_exists"]).Returns(new LocalizedString("Favorite_tip_exists", "Favorite tip exists"));

            _respons = await _addUserFavouriteTipQueryHandler.Handle(_request, CancellationToken.None);

        }

        [Then(@"If exist return resopnse favourite Tip already exist")]
        public void ThenIfExistReturnResopnseFavouriteTipAlreadyExist()
        {
            Assert.AreEqual("Favorite tip exists", _respons.Message);
        }

    }
}
