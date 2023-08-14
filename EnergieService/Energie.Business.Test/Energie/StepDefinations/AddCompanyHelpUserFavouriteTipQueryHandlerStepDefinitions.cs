using Azure.Core;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
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
using TechTalk.SpecFlow.CommonModels;



namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddCompanyHelpUserFavouriteTipQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddCompanyHelpUserFavouriteTipQueryHandler>> _logger;
        private readonly Mock<IUserEnergyTipRepository> _userEnergyTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private AddCompanyHelpUserFavouriteTipQueryHandler _handler;
        private UserFavouriteHelp _userFavouriteHelp;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        private ResponseMessage _result;
        private AddCompanyHelpUserFavouriteTipQuery query;

        public AddCompanyHelpUserFavouriteTipQueryHandlerStepDefinitions()
        {
            _logger = new Mock<ILogger<AddCompanyHelpUserFavouriteTipQueryHandler>>();
            _userEnergyTipRepository = new Mock<IUserEnergyTipRepository>();    
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _handler = new AddCompanyHelpUserFavouriteTipQueryHandler(_logger.Object, _userEnergyTipRepository.Object, _companyUserRepository.Object,_localizer.Object);

        }


        [Given(@"Add CompanyHelp UserFavouriteTipQuery request")]
        public void GivenAddCompanyHelpUserFavouriteTipQueryRequest()
        {
            query = new AddCompanyHelpUserFavouriteTipQuery
            {
                Id = 1,
                UserEmail = "user@example.com",

            };
        }

        [When(@"The request is handled AddCompanyHelpUserFavouriteTipQuery")]
        public async void WhenTheRequestIsHandledAddCompanyHelpUserFavouriteTipQuery()
        {

           var user = new CompanyUser().SetCompanyUser("Satya", "user@example.com", 1);
           _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);

            var company =  Company.Create("xyz");
            var helpCategory = new HelpCategory().SetHelpCategory("test category", "test description");

            var companyHelp = new CompanyHelp().SetCompanyHelp("abc", "test description", "string ownContribution", "string requirement", " string requestvia", helpCategory.Id, company.Id);

           var userFavouriteTip = new UserFavouriteHelp().SetUserFavouriteHelp(companyHelp.Id, user.Id);
           _userEnergyTipRepository.Setup(repo => repo.GetUserFavouriteHelpByIdAsync(userFavouriteTip));
            _localizer.Setup(x => x["Help_Tip_Added"]).Returns(new LocalizedString("Help_Tip_Added", "Help Tip Added"));
            _result = await _handler.Handle(query, CancellationToken.None);
        }

        [Then(@"Company Help User Favourite Tip added sucessfully")]
        public void ThenCompanyHelpUserFavouriteTipAddedSucessfully()
        {
          
            Assert.NotNull(_result);
            Assert.AreEqual(_result.Message, "Help Tip Added");
        }


        //Check favourite tip is already exist or not
        [Given(@"Execute UserFavouriteTipQuery Request To Check Tip is Already Exist or Not")]
        public async void GivenExecuteUserFavouriteTipQueryRequestToCheckTipIsAlreadyExistOrNot()
        {
           
            query = new AddCompanyHelpUserFavouriteTipQuery
            {
                Id =1,
                UserEmail = "user@example.com",

            };
        }

        [When(@"If UserFavouriteTip Exist then execute RemoveEmployerFavouriteHelpAsync")]
        public async void WhenIfUserFavouriteTipExistThenExecuteRemoveEmployerFavouriteHelpAsync()
        {
            
            var user = new CompanyUser().SetCompanyUserForUnitTest(1, "Satya", "user@example.com", 1, 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);

            var company = Company.Create("xyz");
            var helpCategory = new HelpCategory().SetHelpCategory("test category", "test description");

            var companyHelp = new CompanyHelp().SetCompanyHelpTest(1,"abc", "test description", "string ownContribution", "string requirement", " string requestvia", helpCategory.Id, company.Id);

            var userFavouriteTip = new UserFavouriteHelp().SetUserFavouriteHelp(companyHelp.Id, user.Id);

            _userEnergyTipRepository.Setup(x => x.GetUserFavouriteHelpByIdAsync(It.IsAny<UserFavouriteHelp>()))
                .ReturnsAsync((UserFavouriteHelp userFavouriteHelp) =>
                {
                    if (userFavouriteHelp.CompanyHelpID == userFavouriteTip.CompanyHelpID && userFavouriteHelp.CompanyUserId == userFavouriteTip.CompanyUserId)
                    {
                        return userFavouriteTip;
                    }
                    return null;
                });

            _userEnergyTipRepository.Setup(x => x.RemoveEmployerFavouriteHelpAsync(userFavouriteTip.Id, user.Id)).Returns(Task.CompletedTask);
            _localizer.Setup(x => x["Help_Tip_Removed"]).Returns(new LocalizedString("Help_Tip_Removed", "Help Tip Removed"));

            _result = await _handler.Handle(query, CancellationToken.None);



        }
        [Then(@"RemoveEmployerFavouriteHelpAsync Execute Successfully If Favourite Tip is Already Exist")]
        public void ThenRemoveEmployerFavouriteHelpAsyncExecuteSuccessfullyIfFavouriteTipIsAlreadyExist()
        {
           
            Assert.NotNull(_result);
            Assert.AreEqual(_result.Message, "Help Tip Removed");
        }

    }
}