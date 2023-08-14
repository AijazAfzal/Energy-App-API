using Energie.Business.Energie.CommandHandler;
using Energie.Domain.ApplicationEnum;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddDepartmentEnergyPlanCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddDepartmentEnergyPlanCommandHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<IEnergyPlanRepository> _energyPlanRepository;
        private readonly Mock<IDepartmentTipRepository> _departmentTipRepository; 
        private readonly ScenarioContext _scenarioContext;
        private readonly AddDepartmentEnergyPlanCommandHandler _handler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public AddDepartmentEnergyPlanCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<AddDepartmentEnergyPlanCommandHandler>>();
            _scenarioContext = scenariocontext;
            _companyUserRepository = new Mock<ICompanyUserRepository>();    
           _energyPlanRepository = new Mock<IEnergyPlanRepository>();
            _departmentTipRepository = new Mock<IDepartmentTipRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _handler = new AddDepartmentEnergyPlanCommandHandler(_energyPlanRepository.Object
                                                                ,_companyUserRepository.Object
                                                                ,_logger.Object
                                                                ,_departmentTipRepository.Object
                                                                , _localizer.Object); 
            
        }
        #region Adding SuperAdmin Fav Tip To Energy Plan
        //Super Admin Fav Tip added to Department Energy Plan
        [Given(@"the command to add SuperAdmin favtip to Energy Plan")]
        public void GivenTheCommandToAddSuperAdminFavtipToEnergyPlan()
        {
            var testuser = new CompanyUser().SetCompanyUserForUnitTest(1,"testuser","test@gmail.com",1,1); 
            _companyUserRepository.Setup(x=>x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser);
            _departmentTipRepository.Setup(x => x.UserFavouriteDepartmenTiptForPlanAsync(1, 1)).ReturnsAsync(It.IsAny<int>());
            var testplan = new DepartmentEnergyPlan().SetDepartmentEnergyPlanforUnitTest(1, 1, 1, 1, DateTime.Now, true); 
            _energyPlanRepository.Setup(x=>x.AddDepartmentEnergyPlanAsync(testplan));
            _localizer.Setup(x => x["Energy_department_plan_added"]).Returns(new LocalizedString("Energy_department_plan_added", "Energy plan department successfully added"));



        }

        [When(@"the command is handled to add Energy Plan")]
        public async void WhenTheCommandIsHandledToAddEnergyPlan()
        {
            var result = await _handler.Handle(new Business.Energie.Command.AddDepartmentEnergyPlanCommand 
                                                                            {
                                                                              FavouriteTipId=1
                                                                             ,EndDate=DateTime.Now
                                                                             ,TipBy="SuperAdmin"
                                                                             , IsReminder=true
                                                                             , UserEmail= "test@gmail.com"
                                                                            },CancellationToken.None);
            _scenarioContext.Add("result",result); 
            
        }

        [Then(@"the Energy Plan is added sucessfully")]
        public void ThenTheEnergyPlanIsAddedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess); 
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Energy plan department successfully added"));

        }
        #endregion
        // CompanyAdmin fav tip added to plan
        #region Adding CompanyAdmin Fav Tip To Energy Plan
        [Given(@"the command to add CompanyAdmin favtip to Energy Plan")]
        public void GivenTheCommandToAddCompanyAdminFavtipToEnergyPlan()
        {
            var testuser = new CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "test@gmail.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser); 
            _departmentTipRepository.Setup(x => x.DeparmentFavouriteHelpAsync(1,1)).ReturnsAsync(It.IsAny<int>()); 
            var testplan = new DepartmentEnergyPlan().SetDepartmentEnergyPlanforUnitTest(1, 1, 1, 1, DateTime.Now, true); 
            _energyPlanRepository.Setup(x => x.AddDepartmentEnergyPlanAsync(testplan));
            _localizer.Setup(x => x["Energy_department_plan_added"]).Returns(new LocalizedString("Energy_department_plan_added", "Energy plan department successfully added"));


        }

        [When(@"the command is handled to add Energy Plann")]
        public async void WhenTheCommandIsHandledToAddEnergyPlann()
        {
         var resultt = await _handler.Handle(new Business.Energie.Command.AddDepartmentEnergyPlanCommand 
                                                                            {
                                                                              FavouriteTipId=1
                                                                             ,EndDate=DateTime.Now
                                                                             ,TipBy="CompanyAdmin"
                                                                             , IsReminder=true
                                                                             , UserEmail= "test@gmail.com"
                                                                            },CancellationToken.None);
            _scenarioContext.Add("resultt", resultt); 
        }

        [Then(@"the Energy Plan is added sucessfullyyy")]
        public void ThenTheEnergyPlanIsAddedSucessfullyyy()
        {
            var test = _scenarioContext.Get<ResponseMessage>("resultt");  
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Energy plan department successfully added"));  
        }

        #endregion
        #region Adding User Fav Tip To Energy Plan
        //User Fav tip added to Energy Plan 
        [Given(@"the command to add User favtip to Energy Plan")]
        public void GivenTheCommandToAddUserFavtipToEnergyPlan()
        {
            var testuser = new CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "test@gmail.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser);
            _departmentTipRepository.Setup(x => x.UserDepartmentFavouriteTip(1,1)).ReturnsAsync(It.IsAny<int>()); 
            var testplan = new DepartmentEnergyPlan().SetDepartmentEnergyPlanforUnitTest(1, 1, 1, 1, DateTime.Now, true);
            _energyPlanRepository.Setup(x => x.AddDepartmentEnergyPlanAsync(testplan));
            _localizer.Setup(x => x["Energy_department_plan_added"]).Returns(new LocalizedString("Energy_department_plan_added", "Energy plan department successfully added"));
        }

        [When(@"the command is handled to addd Energy Plan")]
        public async void WhenTheCommandIsHandledToAdddEnergyPlan()
        {
           var result = await _handler.Handle(new Business.Energie.Command.AddDepartmentEnergyPlanCommand 
                                                                            {
                                                                              FavouriteTipId=1
                                                                             ,EndDate=DateTime.Now
                                                                             ,TipBy="User"
                                                                             , IsReminder=true
                                                                             , UserEmail= "test@gmail.com"
                                                                            },CancellationToken.None);
            _scenarioContext.Add("result",result); 
        }

        [Then(@"the Energy Plan iss added sucessfullyyy")]
        public void ThenTheEnergyPlanIssAddedSucessfullyyy()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Energy plan department successfully added"));  
        }
        #endregion

        //Check EnergyPlan for SuperAdmin is exists or not
        [Given(@"The command to Check EnergyPlan for SuperAdmin is exists or not")]
        public void GivenTheCommandToCheckEnergyPlanForSuperAdminIsExistsOrNot()
        {

            var testuser = new CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "test@gmail.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser);

            _departmentTipRepository.Setup(x => x.DeparmentFavouriteHelpAsync(1, 1)).ReturnsAsync(It.IsAny<int>());

            var testplan = new DepartmentEnergyPlan().SetDepartmentEnergyPlanforUnitTest(1, 1, 1, 1, DateTime.Now, true);
            _energyPlanRepository.Setup(x => x.AddDepartmentEnergyPlanAsync(testplan));
            _energyPlanRepository.Setup(x => x.GetDepartmentEnergyPlanAsync(1, 1, 1))
                                  .ReturnsAsync(testplan);
            _localizer.Setup(x => x["Energy_plan_exists"]).Returns(new LocalizedString("Energy_plan_exists", "Energy plan exists"));

            //UserFavouriteDepartmenTiptForPlanAsync
        }

        [When(@"The command is handled to Check EnergyPlan for SuperAdmin is exists or not")]
        public async void WhenTheCommandIsHandledToCheckEnergyPlanForSuperAdminIsExistsOrNot()
        {
            var result = await _handler.Handle(new Business.Energie.Command.AddDepartmentEnergyPlanCommand
            {
                FavouriteTipId = 1,
                EndDate = DateTime.Now,
                TipBy = "SuperAdmin",
                IsReminder = true,
                UserEmail = "test@gmail.com"
            }, CancellationToken.None);
            _scenarioContext.Add("result", result);

        }

        [Then(@"If EnergyPlan is exist for SuperAdmin then response EnergyPlan already exists")]
        public void ThenIfEnergyPlanIsExistForSuperAdminThenResponseEnergyPlanAlreadyExists()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.False(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Energy plan exists"));
        }


        //Check EnergyPlan for CompanyAdmin is exists or not
        [Given(@"The command to Check EnergyPlan for CompanyAdmin is exists or not")]
        public void GivenTheCommandToCheckEnergyPlanForCompanyAdminIsExistsOrNot()
        {
            var testuser = new CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "test@gmail.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser);
            _departmentTipRepository.Setup(x => x.DeparmentFavouriteHelpAsync(1, 1)).ReturnsAsync(It.IsAny<int>());
            var testplan = new DepartmentEnergyPlan().SetDepartmentEnergyPlanforUnitTest(1, 1, 1, 1, DateTime.Now, true);
            _energyPlanRepository.Setup(x => x.AddDepartmentEnergyPlanAsync(testplan));
            _energyPlanRepository.Setup(x => x.GetDepartmentEnergyPlanAsync(1, 1, 2))
                                 .ReturnsAsync(testplan);

            _localizer.Setup(x => x["Energy_plan_exists"]).Returns(new LocalizedString("Energy_plan_exists", "Energy plan exists"));


        }

        [When(@"The command is handled to Check EnergyPlan for CompanyAdmin is exists or not")]
        public async void WhenTheCommandIsHandledToCheckEnergyPlanForCompanyAdminIsExistsOrNot()
        {

            var result = await _handler.Handle(new Business.Energie.Command.AddDepartmentEnergyPlanCommand
            {
                FavouriteTipId = 1,
                EndDate = DateTime.Now,
                TipBy = "CompanyAdmin",
                IsReminder = true,
                UserEmail = "test@gmail.com"
            }, CancellationToken.None);
            _scenarioContext.Add("result", result);

        }

        [Then(@"If EnergyPlan is exist for CompanyAdmin then response EnergyPlan already exists")]
        public void ThenIfEnergyPlanIsExistForCompanyAdminThenResponseEnergyPlanAlreadyExists()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.False(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Energy plan exists"));

        }



        //Check EnergyPlan for User is exists or not
        [Given(@"The command to Check EnergyPlan for User is exists or not")]
        public void GivenTheCommandToCheckEnergyPlanForUserIsExistsOrNot()
        {
            var testuser = new CompanyUser().SetCompanyUserForUnitTest(1, "testuser", "test@gmail.com", 1,1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(testuser);
            _departmentTipRepository.Setup(x => x.DeparmentFavouriteHelpAsync(1, 1)).ReturnsAsync(It.IsAny<int>());
            var testplan = new DepartmentEnergyPlan().SetDepartmentEnergyPlanforUnitTest(1, 1, 1, 1, DateTime.Now, true);
            _energyPlanRepository.Setup(x => x.AddDepartmentEnergyPlanAsync(testplan));
            _energyPlanRepository.Setup(x => x.GetDepartmentEnergyPlanAsync(1, 1, 3))
                                 .ReturnsAsync(testplan);
            _localizer.Setup(x => x["Energy_plan_exists"]).Returns(new LocalizedString("Energy_plan_exists", "Energy plan exists"));


        }

        [When(@"The command is handled to Check EnergyPlan for User is exists or not")]
        public async void WhenTheCommandIsHandledToCheckEnergyPlanForUserIsExistsOrNot()
        {

            var result = await _handler.Handle(new Business.Energie.Command.AddDepartmentEnergyPlanCommand
            {
                FavouriteTipId = 1,
                EndDate = DateTime.Now,
                TipBy = "User",
                IsReminder = true,
                UserEmail = "test@gmail.com"
            }, CancellationToken.None);
            _scenarioContext.Add("result", result);
        }

        [Then(@"If EnergyPlan is exist for User then response EnergyPlan already exists")]
        public void ThenIfEnergyPlanIsExistForUserThenResponseEnergyPlanAlreadyExists()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.False(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Energy plan exists"));
        }


    }


}

