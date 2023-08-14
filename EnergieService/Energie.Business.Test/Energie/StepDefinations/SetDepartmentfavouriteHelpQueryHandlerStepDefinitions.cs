using Azure.Core;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class SetDepartmentfavouriteHelpQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<SetDepartmentfavouriteHelpQueryHandler>> _logger;
        private readonly Mock<ICompanyHelpCategoryRepository> _companyHelpCategoryRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly SetDepartmentfavouriteHelpQueryHandler _setDepartmentfavouriteHelpQueryHandler;
        private SetDepartmentfavouriteHelpQuery _request;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public SetDepartmentfavouriteHelpQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<SetDepartmentfavouriteHelpQueryHandler>>();
            _companyHelpCategoryRepository = new Mock<ICompanyHelpCategoryRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _scenariocontext = scenariocontext;
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _setDepartmentfavouriteHelpQueryHandler = new SetDepartmentfavouriteHelpQueryHandler(_logger.Object, _companyHelpCategoryRepository.Object, _companyUserRepository.Object, _localizer.Object);

        }

        //Favouirite Help added sucessfully when  departmenthelp null in SetDepartmentfavouriteHelpQueryHandler
        [Given(@"the command to add favourite help")]
        public void GivenTheCommandToAddFavouriteHelp()
        {
            var newdepartment = new Department().SetCompanyDepartment("Hr", 1);
            var user = new CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            user.Department = newdepartment;
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user);
            var companydepthellp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp","descp","cont","reqvia","moreinfo",1,1); 
            var deptfavhelp = new DepartmentFavouriteHelp().SetDepartmentFavouriteHelp(1, 1);
            deptfavhelp.CompanyUser = user;
            deptfavhelp.CompanyDepartmentHelps = companydepthellp; 
            _companyHelpCategoryRepository.Setup(x => x.AddDepartmentFavouriteHelpAsync(deptfavhelp));
            _localizer.Setup(x => x["FavoriteHelp_added"]).Returns(new LocalizedString("FavoriteHelp_added", "Favorite Help added"));

        }

        [When(@"the command is handled to add help")]
        public async void WhenTheCommandIsHandledToAddHelp()
        {
            var result = await _setDepartmentfavouriteHelpQueryHandler.Handle(new Business.Energie.Query.SetDepartmentfavouriteHelpQuery { Id = 1, UserEmail = "abc@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"help is added sucessfully")]
        public void ThenHelpIsAddedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Favorite Help added")); 
        }



        //FavouriteHelp removed sucessfully when departmenthelp is not null in SetDepartmentfavouriteHelpQueryHandler
        [Given(@"The command to remove favourite help")]
        public void GivenTheCommandToRemoveFavouriteHelp()
        {

            _request = new SetDepartmentfavouriteHelpQuery
            {
                Id = 1,
                UserEmail = "abc@gmail.com"

            };
            var newdepartment = new Department().SetCompanyDepartment("Hr", 1);
            var user = new CompanyUser().SetCompanyUserForUnitTest(1,"newuser", "abc@gmail.com", 1,1); 
            user.Department = newdepartment;
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user);
            var companydepthellp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp", "descp", "cont", "reqvia", "moreinfo", 1, 1);
            var deptfavhelp = new DepartmentFavouriteHelp().SetDepartmentFavouriteHelp(1, 1);
            deptfavhelp.CompanyUser = user;
            deptfavhelp.CompanyDepartmentHelps = companydepthellp;
            _companyHelpCategoryRepository.Setup(x => x.GetDepartmentFavouriteForUserAsync(1, 1)).ReturnsAsync(deptfavhelp); 
            _companyHelpCategoryRepository.Setup(x => x.DeleteDepartmentFavouriteForUserAsync(_request.Id, user.Id));
            _localizer.Setup(x => x["FavoriteHelp_Removed"]).Returns(new LocalizedString("FavoriteHelp_Removed", "Favorite Help Removed"));
        }

        [When(@"The command is handled to remove favourite help")]
        public async void WhenTheCommandIsHandledToRemoveFavouriteHelp()
        {
          
            var result = await _setDepartmentfavouriteHelpQueryHandler.Handle(new Business.Energie.Query.SetDepartmentfavouriteHelpQuery { Id = 1, UserEmail = "abc@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"Favourite Help is removed sucessfully")]
        public void ThenFavouriteHelpIsRemovedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Favorite Help Removed")); 
        }


    }
}