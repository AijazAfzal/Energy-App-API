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
    public class RemoveDepartmentFavouriteTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<RemoveDepartmentFavouriteTipCommandHandler>> _logger;
        private readonly Mock<IDepartmentTipRepository> _departmentTipRepository;
        private readonly Mock<ICompanyHelpCategoryRepository> _companyHelpCategoryRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly RemoveDepartmentFavouriteTipCommandHandler _removeDepartmentFavouriteTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public RemoveDepartmentFavouriteTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<RemoveDepartmentFavouriteTipCommandHandler>>();
            _departmentTipRepository = new Mock<IDepartmentTipRepository>();
            _companyHelpCategoryRepository = new Mock<ICompanyHelpCategoryRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _scenariocontext = scenariocontext;
            _removeDepartmentFavouriteTipCommandHandler = new RemoveDepartmentFavouriteTipCommandHandler(_logger.Object, _departmentTipRepository.Object, _companyUserRepository.Object, _companyHelpCategoryRepository.Object, _localizer.Object);

        }
        [Given(@"the command to remove department favourite tip added by superAdmin")]
        public void GivenTheCommandToRemoveDepartmentFavouriteTipAddedBySuperAdmin()
        {
            var user = new CompanyUser().SetCompanyUser("newuser", "newuser@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("newuser@gmail.com")).ReturnsAsync(user);
            _departmentTipRepository.Setup(x => x.RemovedDepartmentFavouriteTipsAsync(1, 1));
            _localizer.Setup(x => x["Department_tip_removed"]).Returns(new LocalizedString("Department_tip_removed", "Department tip removed"));
        }

        [When(@"the command is handled to remove favourite tip")]
        public async void WhenTheCommandIsHandledToRemoveFavouriteTip()
        {
            var result = await _removeDepartmentFavouriteTipCommandHandler.Handle(new Business.Energie.Command.RemoveDepartmentFavouriteTipCommand { DepartmentTipId = 1, TipBy = "SuperAdmin", UserEmail = "newuser@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"the tip is removed sucessfullyy")]
        public void ThenTheTipIsRemovedSucessfullyy()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.AreEqual("Department tip removed", test.Message);
            Assert.IsNotNull(test); 
            
        }


        //If tip added by CompanyAdmin
        [Given(@"the command to remove department favourite tip added by CompanyAdmin")]
        public void GivenTheCommandToRemoveDepartmentFavouriteTipAddedByCompanyAdmin()
        {
            var user = new CompanyUser().SetCompanyUser("newuser", "newuser@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("newuser@gmail.com")).ReturnsAsync(user);
            _companyHelpCategoryRepository.Setup(x => x.DeleteDepartmentFavouriteForUserAsync(1, 1));
            _localizer.Setup(x => x["Department_tip_removed"]).Returns(new LocalizedString("Department_tip_removed", "Department tip removed"));
        }

        [When(@"the command is handled to remove favourite tipp")]
        public async void WhenTheCommandIsHandledToRemoveFavouriteTipp()
        {
            var result = await _removeDepartmentFavouriteTipCommandHandler.Handle(new Business.Energie.Command.RemoveDepartmentFavouriteTipCommand { DepartmentTipId = 1, TipBy = "CompanyAdmin", UserEmail = "newuser@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"the tip is removed sucessfullyyy")]
        public void ThenTheTipIsRemovedSucessfullyyy()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.AreEqual("Department tip removed", test.Message);
            Assert.IsNotNull(test);
        }


        //If tip added by User

        [Given(@"the command to remove department favourite tip added by User")]
        public void GivenTheCommandToRemoveDepartmentFavouriteTipAddedByUser()
        {
            var user = new CompanyUser().SetCompanyUser("newuser", "newuser@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("newuser@gmail.com")).ReturnsAsync(user);
            _departmentTipRepository.Setup(x => x.RemoveUserDepartmentFavouriteTipsAsync(1, 1));
            _localizer.Setup(x => x["Department_tip_removed"]).Returns(new LocalizedString("Department_tip_removed", "Department tip removed"));

        }

        [When(@"the command is handled to remove favourite tippp")]
        public async void WhenTheCommandIsHandledToRemoveFavouriteTippp()
        {
            var result = await _removeDepartmentFavouriteTipCommandHandler.Handle(new Business.Energie.Command.RemoveDepartmentFavouriteTipCommand { DepartmentTipId = 1, TipBy = "User", UserEmail = "newuser@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);

        }

        [Then(@"the tip is deleted sucessfullyy")]
        public void ThenTheTipIsDeletedSucessfullyy()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.AreEqual("Department tip removed", test.Message);
            Assert.IsNotNull(test); 
        }
    }
}
