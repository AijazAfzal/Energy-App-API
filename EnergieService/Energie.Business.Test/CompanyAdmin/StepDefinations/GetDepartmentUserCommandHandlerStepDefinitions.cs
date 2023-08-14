using AutoMapper;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class GetDepartmentUserCommandHandlerStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock <ICreateCompanyUserRepository> _companyUserRepository;
        private readonly Mock <IDepartmentRepository>_departmentRepository; 
        private readonly Mock <ILogger<GetDepartmentUserCommandHandler>> _logger;
        private readonly GetDepartmentUserCommandHandler _getDepartmentUserCommand;

        public GetDepartmentUserCommandHandlerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _departmentRepository=new Mock<IDepartmentRepository>();
            _logger=new Mock<ILogger<GetDepartmentUserCommandHandler>>();
            _companyUserRepository = new Mock<ICreateCompanyUserRepository>();
            _getDepartmentUserCommand = new GetDepartmentUserCommandHandler(_departmentRepository.Object,_logger.Object); 
           
        }
        [Given(@"the command to get all users")]
        public  void GivenTheCommandToGetAllUsers() 
        {

            var departmentusers = new List<Domain.Domain.CompanyUser>()
            {
                new Domain.Domain.CompanyUser().SetCompanyUser("abc","abc@gmail.com",1)
               
            };

            _departmentRepository.Setup(x => x.GetDepartmentUserAsync(1)).ReturnsAsync(departmentusers); 
        }

        [When(@"the command is handled to get all users")]
        public async void WhenTheCommandIsHandledToGetAllUsers()
        {
            var result = await _getDepartmentUserCommand.Handle(new Business.CompanyAdmin.Command.GetDepartmentUserCommand {DepartmentId=1 },CancellationToken.None); 
            _scenarioContext.Add("result", result);  
        }

        [Then(@"list of users are retrieved")]
        public  void ThenListOfUsersAreRetrieved()
        {
            var test = _scenarioContext.Get<List<Domain.Domain.CompanyUser>> ("result");    
            Assert.IsNotNull(test); 
         }
    }
}
