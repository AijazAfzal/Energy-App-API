using AutoMapper;
using Energie.Api;
using Energie.Business.CompanyAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class GetDepartmentListQueryHandlerStepDefinitions
    {
        private readonly Mock<IDepartmentRepository> _departmentRepository;
        private readonly Mock<ILogger<GetDepartmentListQueryHandler>> _logger;
        private readonly Mock<ICompanyAdminRepository> _addCompanyAdminRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly GetDepartmentListQueryHandler _getDepartmentListQueryHandler;

        public GetDepartmentListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _departmentRepository = new Mock<IDepartmentRepository>();
            _logger = new Mock<ILogger<GetDepartmentListQueryHandler>>();
            _addCompanyAdminRepository = new Mock<ICompanyAdminRepository>();
            _getDepartmentListQueryHandler = new GetDepartmentListQueryHandler(_departmentRepository.Object, _logger.Object, _addCompanyAdminRepository.Object);

        }
        [Given(@"the command to get all departments")]
        public  void GivenTheCommandToGetAllDepartments()
        {
            var newcompany = Company.Create("abc");
            var newcompanyadmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(newcompany.Id,"abcd", "xyz@gmail.com");  
            _addCompanyAdminRepository.Setup(x => x.GetCompanyAdminByEmailAsync("xyz@gmail.com")).ReturnsAsync(newcompanyadmin);
            var departmentlist = new List<Department> { new Department().SetCompanyDepartment("new department", newcompany.Id) };
            _departmentRepository.Setup(x => x.GetDepartmentListAsync(newcompany.Id)).ReturnsAsync(departmentlist);   

        }

        [When(@"the command is handled to get all departments")]
        public async void WhenTheCommandIsHandledToGetAllDepartments()
        {
            var result = await _getDepartmentListQueryHandler.Handle(new Business.CompanyAdmin.Query.GetDepartmentListQuery {UserEmail= "xyz@gmail.com" },CancellationToken.None);
            _scenarioContext.Add("result",result);   

        }

        [Then(@"departments list is retreived")]
        public void ThenDepartmentsListIsRetreived()
        {
            var test = _scenarioContext.Get<List<Department>>("result"); 
            Assert.IsNotNull(test); 

        }
    }
}
