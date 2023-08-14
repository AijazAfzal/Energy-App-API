using AutoMapper;
using Azure.Core;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.CommandHandler;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model;
using Energie.Model.Request;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using NUnit.Framework;
using System;
using System.Reflection.Metadata;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddDepartmentFavouriteTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddDepartmentFavouriteTipCommandHandler>> _logger;
        private readonly Mock<IDepartmentTipRepository> _departmentTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly AddDepartmentFavouriteTipCommandHandler _addDepartmentFavouriteTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        private AddDepartmentFavouriteTipCommand _command;
        private ResponseMessage _response;
        public AddDepartmentFavouriteTipCommandHandlerStepDefinitions(ScenarioContext scenarioContext)
        {
            
            _logger = new Mock<ILogger<AddDepartmentFavouriteTipCommandHandler>>();
            _scenarioContext = scenarioContext;
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _departmentTipRepository = new Mock<IDepartmentTipRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _addDepartmentFavouriteTipCommandHandler = new AddDepartmentFavouriteTipCommandHandler(_logger.Object
                , _departmentTipRepository.Object
                , _companyUserRepository.Object, _localizer.Object);
        }

        //Favourites tips added sucessfully
        [Given(@"AddDepartmentFavouriteTipCommand request")]
        public  void GivenAddDepartmentFavouriteTipCommandRequest()
        {
            var newuser = new CompanyUser().SetCompanyUser("abc","abcd@gmail.com",1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abcd@gmail.com")).ReturnsAsync(newuser);
            var setdepartmentTip = new Domain.Domain.DepartmentFavouriteTip().SetDepartmentFavouriteTip(1, 1);
            _departmentTipRepository.Setup(x => x.AddUserFavouriteDepartmentTipAsync(setdepartmentTip));
           _localizer.Setup(x => x["Department_favorite_tip_with_ID_added_for_the_user"]).Returns(new LocalizedString("Department_favorite_tip_with_ID_added_for_the_user", "Department tip is added by the user"));
          

        }
        [When(@"the command is handled to AddUserFavouriteDepartmentTipAsync")]
        public async void WhenTheCommandIsHandledToAddUserFavouriteDepartmentTipAsync()
        {
            var result = await _addDepartmentFavouriteTipCommandHandler.Handle(new Business.Energie.Command.AddDepartmentFavouriteTipCommand { Id = 1, UserEmail = "abcd@gmail.com" }, CancellationToken.None);
            _scenarioContext.Add("result", result);
        }


        [Then(@"the favourites tips added sucessfully")]
        public void ThenTheFavouritesTipsAddedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Department tip is added by the user")); 
        }



        // Removed Department favourite tip when existuserDepartmentTip is not null 

        [Given(@"Department Favourite Tip Command request")]
        public void GivenDepartmentFavouriteTipCommandRequest()
        {
            _command = new AddDepartmentFavouriteTipCommand
            {
                Id = 1,
                UserEmail = "test@gmail.com"
            };
        }

        [When(@"The command is handled to Removed Department favourite tip when existuserDepartmentTip is not null")]
        public async void WhenTheCommandIsHandledToRemovedDepartmentFavouriteTipWhenExistuserDepartmentTipIsNotNull()
        {
           
            var newuser = new CompanyUser().SetCompanyUser("abc", "test@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(newuser);
            _departmentTipRepository.Setup(x => x.UserFavouriteDepartmentTipAsync(newuser.Id, _command.Id)).ReturnsAsync(new Domain.Domain.DepartmentFavouriteTip());
            _localizer.Setup(x => x["Department_favorite_tip_Removed"]).Returns(new LocalizedString("Department_favorite_tip_Removed", "Department favorite tip Removed"));
            _response = await _addDepartmentFavouriteTipCommandHandler.Handle(_command, CancellationToken.None);

        }

        [Then(@"Department Favourite Tip Removed Successfully")]
        public void ThenDepartmentFavouriteTipRemovedSuccessfully()
        {
            Assert.NotNull(_response);
            Assert.AreEqual("Department favorite tip Removed", _response.Message);
        }

    }
}
