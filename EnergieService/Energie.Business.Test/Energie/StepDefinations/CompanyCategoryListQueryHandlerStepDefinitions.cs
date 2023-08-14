using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class CompanyCategoryListQueryHandlerStepDefinitions
    {
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<ILogger<CompanyCategoryListQueryHandler>> _logger;
        private readonly Mock<ICompanyHelpCategoryRepository> _companyHelpCategoryRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly CompanyCategoryListQueryHandler _companyCategoryListQueryHandler;
        public CompanyCategoryListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _logger = new Mock<ILogger<CompanyCategoryListQueryHandler>>();
            _companyHelpCategoryRepository = new Mock<ICompanyHelpCategoryRepository>();
            _scenariocontext = scenariocontext;
            _companyCategoryListQueryHandler = new CompanyCategoryListQueryHandler(_companyUserRepository.Object,_logger.Object,_companyHelpCategoryRepository.Object); 

        }
        [Given(@"the command to get the  Company Help categories")]
        public  void GivenTheCommandToGetTheCompanyHelpCategories()
        {
            var company = Company.Create("ariqt");
            var newdepartment= new Department().SetCompanyDepartment("HR", 1);
            newdepartment.Company = company;
            var newuser = new CompanyUser().SetCompanyUser("shivaji","shivaji@gmail.com", 1); 
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("shivaji@gmail.com")).ReturnsAsync(newuser);
            newuser.Department = newdepartment;
            var companyhelpcategory = new CompanyHelpCategory().SetCompanyHelpCategory("newhelpcategory","descp",1); 
            companyhelpcategory.Company = company; 
            _companyHelpCategoryRepository.Setup(x => x.GetCompanyCategoryHelpByCompanyID(0))
                .ReturnsAsync(new List<CompanyHelpCategory>() { companyhelpcategory });      
           
        }

        [When(@"the command is handled to get Company Help categories")]
        public async void WhenTheCommandIsHandledToGetCompanyHelpCategories()
        {
            var result = await _companyCategoryListQueryHandler.Handle(new Business.Energie.Query.CompanyCategoryListQuery 
                                 { 
                                     UserEmail= "shivaji@gmail.com"  

            }
            ,CancellationToken.None);

            _scenariocontext.Add("result",result); 
            
        }

        [Then(@"Company Help categories list is retrieved")]
        public void ThenCompanyHelpCategoriesListIsRetrieved()
        {
            var test = _scenariocontext.Get<List<CompanyHelpCategory>>("result"); 
            Assert.IsNotNull(test); 
           
        }
    }
}
