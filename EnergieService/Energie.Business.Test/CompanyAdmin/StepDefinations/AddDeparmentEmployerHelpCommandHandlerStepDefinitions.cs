using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class AddDeparmentEmployerHelpCommandHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpRepository> _companyHelpRepository;
        private readonly Mock<ILogger<AddEmployerHelpForDeparmentCommandHandler>> _logger;
        private readonly ScenarioContext _scenariocontext;
        private readonly AddEmployerHelpForDeparmentCommandHandler _addDeparmentEmployerHelpCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public AddDeparmentEmployerHelpCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _companyHelpRepository = new Mock<ICompanyHelpRepository>();
            _logger = new Mock<ILogger<AddEmployerHelpForDeparmentCommandHandler>>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _addDeparmentEmployerHelpCommandHandler = new AddEmployerHelpForDeparmentCommandHandler(_companyHelpRepository.Object,_logger.Object, _localizer.Object); 

        }
        [Given(@"the command to add employer help")]
        public void GivenTheCommandToAddEmployerHelp()
        {

            var newcomapny =  Company.Create("ariqt");
            var newdepartment = new Department().SetCompanyDepartment("HR",1);
            newdepartment.Company = newcomapny;
            var helpcategory = new HelpCategory().SetHelpCategory("newcategory","descp");
            var employerhelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp
                                                                                  ("newhelp",
                                                                                   "descp",
                                                                                   "cont",
                                                                                   "reqvia",
                                                                                   "moreinfo",
                                                                                    1,
                                                                                    1
                                                                                  );
            employerhelp.HelpCategory = helpcategory;
            employerhelp.Department = newdepartment;
            _companyHelpRepository.Setup(x=>x.AddDepartmentEmployerHelpAsync(employerhelp));
            _localizer.Setup(x => x["Added_help_from_the_employer_with_employerHelpID"]).Returns(new LocalizedString("Added_help_from_the_employer_with_employerHelpID", "Added help from the employer"));
        }

        [When(@"the command is handled to add employer help")]
        public async void WhenTheCommandIsHandledToAddEmployerHelp()
        {
            var result = await _addDeparmentEmployerHelpCommandHandler.Handle(new Business.CompanyAdmin.Command.AddEmployerHelpForDeparmentCommand
            { Name= "newhelp",
                                                                                 Description= "descp",
                                                                                 Contribution="cont",
                                                                                 Requestvia= "reqvia",
                                                                                 MoreInformation= "moreinfo",
                                                                                 HelpCategoryId=1,
                                                                                 DepartmentId=1
                                                                               }, CancellationToken.None);
            _scenariocontext.Add("result",result); 


        }

        [Then(@"the employer help is added")]
        public void ThenTheEmployerHelpIsAdded()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Message, "Added help from the employer");
            //Assert.That(test.Message, Is.EqualTo($"Employer help added with " + test.Id + " this id")); 

        }
    }
}
