using AutoMapper;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class AddCompanyHelpCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddCompanyHelpCommandHandler>> _logger;
        private readonly Mock<ICompanyHelpRepository> _companyHelpTipRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<ICompanyAdminRepository> _addCompanyAdminRepository;
        private readonly AddCompanyHelpCommandHandler addCompanyHelpCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public AddCompanyHelpCommandHandlerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _logger = new Mock<ILogger<AddCompanyHelpCommandHandler>>();
            _companyHelpTipRepository = new Mock<ICompanyHelpRepository>();
            _addCompanyAdminRepository = new Mock<ICompanyAdminRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            addCompanyHelpCommandHandler = new AddCompanyHelpCommandHandler(_logger.Object, _companyHelpTipRepository.Object, _addCompanyAdminRepository.Object,_localizer.Object);


        }

        [Given(@"the command to add company help tips")]
        public void GivenTheCommandToAddCompanyHelpTips()
        {
            var newcompany =  Domain.Domain.Company.Create("ariqt");
            var newadmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1, "admin", "abc@gmail.com");
            _addCompanyAdminRepository.Setup(x => x.GetCompanyAdminByEmailAsync("abc@gmail.com")).ReturnsAsync(newadmin);
            newadmin.Company = newcompany;
            var newhelpcategory = new Domain.Domain.HelpCategory().SetHelpCategory("new category", "descp");
            var companyhelp = new Domain.Domain.CompanyHelp().SetCompanyHelp
                                                              ("newhelp",
                                                                "new help descp",
                                                                "cont",
                                                                "req",
                                                                "reqvia", 
                                                                1,
                                                                1
                                                              ); 
            companyhelp.Company = newcompany;
            companyhelp.HelpCategory = newhelpcategory; 
           _companyHelpTipRepository.Setup(x => x.AddCompanyHelpAsync(companyhelp));
            _localizer.Setup(x => x["Help_tip_from_the_company_with_ID", companyhelp.Id]).Returns(new LocalizedString("Help_tip_from_the_company_with_ID", $"Help tip from the company with ID {companyhelp.Id}"));
        }

        [When(@"the command is handled to add company help tips")]
        public async void WhenTheCommandIsHandledToAddCompanyHelpTips()
        {
            var result = await addCompanyHelpCommandHandler.Handle
                (new Business.CompanyAdmin.Command.AddCompanyHelpCommand()
                {
                    CompanyHelpCategoryId = 1,
                    Conditions = "nwe",
                    Description = "descp",
                    Name = "newhelp",
                    OwnContribution = "cont",
                    Requestvia = "reqvia",
                    UserEmail = "abc@gmail.com"
                }
                , CancellationToken.None);
            _scenarioContext.Add("result", result);

        }
        [Then(@"company help tips added sucessfully")]
        public void ThenCompanyHelpTipsAddedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Help tip from the company with ID {test.Id}")); 

        }

        #region

        //[Given(@"the command to add company help request")]
        //public void GivenTheCommandToAddCompanyHelpRequest()
        //{
        //    var companyhelp = new CompanyHelp().SetCompanyHelp
        //                                        ("Test 1",
        //                                        "Test 1 Description",
        //                                        "Test 1 OwnContributio",
        //                                        "Test 1 Requirment",
        //                                        "Test 1 Requestvia",
        //                                        0);
        //    _companyHelpTipRepository.Setup(x => x.AddCompanyHelpAsync(companyhelp));
        //}
        //
        //[When(@"the command is handled to add company help by request")]
        //public async void WhenTheCommandIsHandledToAddCompanyHelpByRequest()
        //{
        //    var result = await addCompanyHelpTipCommandHandler.Handle
        //        (new Business.CompanyAdmin.Command.AddCompanyHelpTipCommand()
        //        {
        //            CompanyHelpCategoryId = 1,
        //            Name = "Test 1",
        //            Description = "Test 1 Description",
        //            OwnContribution = "Test 1 OwnContribution",
        //            Requestvia = "Test 1 Requestvia",
        //            Conditions = "Test 1 Requirment"
        //
        //        }
        //        , CancellationToken.None);
        //    _scenarioContext.Add("result", result);
        //}
        //
        //[Then(@"company help tip get error")]
        //public void ThenCompanyHelpTipGetError()
        //{
        //    var test = _scenarioContext.Get<ResponseMessage>("result");
        //    Assert.IsNotNull(test);
        //}
        //

        #endregion
    }
}
