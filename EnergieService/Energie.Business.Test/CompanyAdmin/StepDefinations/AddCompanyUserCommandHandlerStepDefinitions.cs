using AutoMapper;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Security.Cryptography.X509Certificates;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class AddCompanyUserCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddCompanyUserCommandHandler>> _logger;
        public readonly Mock<ICreateCompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly AddCompanyUserCommandHandler _addCompanyUserCommandHandler;
        private readonly ResponseMessage _responsemessage;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public AddCompanyUserCommandHandlerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _logger = new Mock<ILogger<AddCompanyUserCommandHandler>>();
            _companyUserRepository =new Mock<ICreateCompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _responsemessage = new ResponseMessage();
            _addCompanyUserCommandHandler = new AddCompanyUserCommandHandler(_logger.Object, _companyUserRepository.Object, _localizer.Object);

        }

        [Given(@"the command to add company user")]
        public  void GivenTheCommandToAddCompanyUser()
        {
            var b2CCompanyUser = new B2CCompanyUser().SetCompanyUser("abc", "abc@gmail.com", 1);
            _localizer.Setup(x => x["User_added"]).Returns(new LocalizedString("User_added", "User added"));
            _companyUserRepository.Setup(x => x.CreateCompanyUserAsync(b2CCompanyUser)); 
        }

        [When(@"the command is handled to add company user")]
        public async void WhenTheCommandIsHandledToAddCompanyUser()
        {
            var result = await _addCompanyUserCommandHandler.Handle(new Business.CompanyAdmin.Command.AddCompanyUserCommand
            {
                UserName= "abc",
                Email = "abc@gmail.com",
                DepartmentId= 1
            },CancellationToken.None); 
            _scenarioContext.Add("result",result); 
        }

        [Then(@"company user is added sucessfully")]
        public void ThenCompanyUserIsAddedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Message, "User added");
            
        }
    }
}
