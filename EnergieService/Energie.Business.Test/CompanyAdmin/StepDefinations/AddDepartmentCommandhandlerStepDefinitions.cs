using AutoMapper;
using Azure.Core;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model;
using Energie.Model.Request;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using TechTalk.SpecFlow;
using Department = Energie.Domain.Domain.Department;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class AddDepartmentCommandhandlerStepDefinitions
    {
        private readonly Mock<IDepartmentRepository> _departmentRepository;
        private readonly Mock <ICompanyAdminRepository> _addCompanyAdminRepository;
        private readonly Mock<ILogger<AddDepartmentCommandhandler>> _logger;
        private readonly Mock <IClaimService> _tokenClaimService;
        private readonly ScenarioContext _scenarioContext;
        private readonly AddDepartmentCommandhandler _addDepartmentCommandhandler;
        private  AddDepartmentCommand _command;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public AddDepartmentCommandhandlerStepDefinitions
            (ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _logger = new Mock<ILogger<AddDepartmentCommandhandler>>();
            _tokenClaimService=new Mock<IClaimService>();
            _departmentRepository=new Mock<IDepartmentRepository>();
            _addCompanyAdminRepository=new Mock<ICompanyAdminRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _addDepartmentCommandhandler = new AddDepartmentCommandhandler(_departmentRepository.Object, _logger.Object,_addCompanyAdminRepository.Object, _localizer.Object); 
        }
        [Given(@"the command to add department")]
        public void GivenTheCommandToAddDepartment()
        {
            _command = new AddDepartmentCommand { UserEmail= "Test@gmail.com", DepartmentName = "Test" };

            var departments = new List<string>(new string[] { "Test1", "Test2", "Test3" });

            var newuser = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1, "Test", "Test@gmail.com");
            _addCompanyAdminRepository.Setup(x => x.GetCompanyAdminByEmailAsync("Test@gmail.com")).ReturnsAsync(newuser);
            _departmentRepository.Setup(x => x.GetDepartmentByCompanyIdAsync(1)).ReturnsAsync(departments);
            var newdepartment = new Department().SetCompanyDepartment("Test", 1);
           _departmentRepository.Setup(x => x.AddDepartmentAsync(newdepartment)).ReturnsAsync(It.IsAny<int>());
            _localizer.Setup(x => x["DepartmentName_with_ID_added", _command.DepartmentName]).Returns(new LocalizedString("DepartmentName_with_ID_added", $"DepartmentName with {_command.DepartmentName} added"));

        }

        [When(@"the command is handled to add department")]
        public async  void WhenTheCommandIsHandledToAddDepartment()
        {
             var result = await  _addDepartmentCommandhandler.Handle
                (_command, CancellationToken.None);
            _scenarioContext.Add("result", result); 
         }

        [Then(@"the department is added sucessfully")]
        public void ThenTheDepartmentIsAddedSucessfully() 
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Message, $"DepartmentName with {_command.DepartmentName} added");


        }
    }
}
