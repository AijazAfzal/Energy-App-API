using AutoMapper;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.CompanyAdmin.CommandHandler;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class UpdateCompanyHelpCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<UpdateCompanyHelpCommandHandler>> _logger;
        private readonly Mock<ICompanyHelpRepository> _companyHelpTipRepository;
        private readonly Mock<ICompanyAdminRepository> _companyAdminRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly UpdateCompanyHelpCommandHandler _updateCompanyHelpCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;


        public UpdateCompanyHelpCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<UpdateCompanyHelpCommandHandler>>();
            _companyHelpTipRepository= new Mock<ICompanyHelpRepository>();
            _companyAdminRepository = new Mock<ICompanyAdminRepository>();
            _scenarioContext = scenariocontext;
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _updateCompanyHelpCommandHandler = new UpdateCompanyHelpCommandHandler(_logger.Object
                , _companyHelpTipRepository.Object
                , _companyAdminRepository.Object
                , _localizer.Object);

            

        }
        [Given(@"the command to update company help")]
        public  void GivenTheCommandToUpdateCompanyHelp()
        {
            var testadmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1,"testuser","test@yahoo.com");
            _companyAdminRepository.Setup(x=>x.GetCompanyAdminByEmailAsync("test@yahoo.com")).ReturnsAsync(testadmin);
            var testhelp = new CompanyHelp().SetCompanyHelp("testhelp","testdescp","testcont","req","reqvia",1,1);
            _companyHelpTipRepository.Setup(x=>x.GetCompanyHelpByIdAsync(1,1)).ReturnsAsync(testhelp);
            var updatedhelp=testhelp.UpdateCompanyHelp("newtesthelp", "newtestdescp", "newtestcont", "newreq", "newreqvia",2);
            _companyHelpTipRepository.Setup(x=>x.UpdateCompanyHelpAsync(updatedhelp));
            _localizer.Setup(x => x["Business_tips_with_ID_Update", updatedhelp.Id]).Returns(new LocalizedString("Business_tips_with_ID_Update", $"Business tips with ID {updatedhelp.Id} Update"));

        }

        [When(@"the command is handled to update company help")]
        public async void WhenTheCommandIsHandledToUpdateCompanyHelp()
        {
            var result = await _updateCompanyHelpCommandHandler.Handle(new Business.CompanyAdmin.Command.UpdateCompanyHelpCommand
            {
                Id = 1,
                CompanyHelpCategoryId = 1,
                Conditions = "Test cont",
                Description = "newtestdescp",
                Name = "newtesthelp",
                OwnContribution = "newtestcont",
                Requestvia = "newreqvia",
                UserEmail = "test@yahoo.com"
            }, CancellationToken.None); 

            _scenarioContext.Add("result",result); 


        }

        [Then(@"company help is updated sucessfully")]
        public void ThenCompanyHelpIsUpdatedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Business tips with ID {test.Id} Update"));    
        }
    }
}
