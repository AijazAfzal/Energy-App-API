using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class GetUserDepartmentTipListQueryHandlerStepDefinitions
    {
        private readonly Mock<IUserDepartmentTipRepository> _userDepartmentTipRepository;
        private readonly Mock<ILogger<GetUserDepartmentTipListQueryHandler>> _logger; 
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly GetUserDepartmentTipListQueryHandler _getUserDepartmentTipListQueryHandler; 
        public GetUserDepartmentTipListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext; 
            _companyUserRepository= new Mock<ICompanyUserRepository>();
            _userDepartmentTipRepository= new Mock<IUserDepartmentTipRepository>();
            _logger = new Mock<ILogger<GetUserDepartmentTipListQueryHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _getUserDepartmentTipListQueryHandler = new GetUserDepartmentTipListQueryHandler(_userDepartmentTipRepository.Object,_logger.Object,_companyUserRepository.Object); 


        }
        [Given(@"the command to get the department tip list")]
        public void GivenTheCommandToGetTheDepartmentTipList()
        {
            var company = Domain.Domain.Company.Create("ariqt");
            var department = new Domain.Domain.Department().SetCompanyDepartment("Hr",1);
            department.Company = company; 
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("abc","abc@gmail.com", 1);
            user.Department = department;
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now); 
            var questions = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("cbf","dfg", 1); 
            questions.EnergyAnalysis = energyanalysis; 
            var userdepartmenttip = new Domain.Domain.UserDepartmentTip().SetUserDepartmentTip(1,"klm",1);  
            userdepartmenttip.CompanyUser = user; 
            userdepartmenttip.EnergyAnalysisQuestions=questions; 
            

            _userDepartmentTipRepository.Setup(x => x.GetUserDepartmentTipListAsync(1)).ReturnsAsync(new List<Domain.Domain.UserDepartmentTip>() { userdepartmenttip }); ; 


            
        }

        [When(@"the command is handled to get ther department tip list")]
        public async void WhenTheCommandIsHandledToGetTherDepartmentTipList()
        {
            var result = await _getUserDepartmentTipListQueryHandler.Handle(new Business.Energie.Query.GetUserDepartmentTipListQuery {UserEmail="abc@gmail.com" },CancellationToken.None); 
            _scenariocontext.Add("result",result); 
           
        }

        [Then(@"tip list is retrieved")]
        public void ThenTipListIsRetrieved()
        {
            var test = _scenariocontext.Get<UserDepartmentTipList>("result");
            Assert.IsNotNull(test); 
            
        }
    }
}
