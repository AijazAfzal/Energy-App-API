using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class UpdateDeparmentEmployerHelpCommandHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpRepository> _companyHelpRepository;
        private readonly Mock<ILogger<UpdateEmployerHelpForDeparmentCommandHandler>> _logger;
        private readonly ScenarioContext _scenariocontext;
        private readonly UpdateEmployerHelpForDeparmentCommandHandler _updateDeparmentEmployerHelpCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public UpdateDeparmentEmployerHelpCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _companyHelpRepository = new Mock<ICompanyHelpRepository>();
            _logger = new Mock<ILogger<UpdateEmployerHelpForDeparmentCommandHandler>>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _updateDeparmentEmployerHelpCommandHandler = new UpdateEmployerHelpForDeparmentCommandHandler(_companyHelpRepository.Object, _logger.Object, _localizer.Object);

        }
        [Given(@"the command to update employer help")]
        public void GivenTheCommandToUpdateEmployerHelp()
        {
            var department = new Department().SetCompanyDepartment("HR", 1);
            var helpcategory = new HelpCategory().SetHelpCategory("new category", "descp");
            var companydepartmenthelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp", "descp", "cont", "reqvia", "moreinfo", 1, 1);
            var employerhelptobeupdated = _companyHelpRepository.Setup(x => x.GetEmployerDepartmentHelpbyId(1)).ReturnsAsync(companydepartmenthelp);
            var updatinghelp = companydepartmenthelp.UpdateCompanyDepartmentHelp("newupdatedhelp", "newdescp", "contt", "reqviaa", "moreinfoo", 1, 1);
            _companyHelpRepository.Setup(x => x.UpdateEmployerDepartmentHelp(updatinghelp));
            _localizer.Setup(x => x["Employer_helps_with_id_update", updatinghelp.Id]).Returns(new LocalizedString("Employer_helps_with_id_update", $"Employer helps with id {updatinghelp.Id} update"));

        }

        [When(@"the command is handled to update employer help")]
        public async void WhenTheCommandIsHandledToUpdateEmployerHelp()
        {
            var result = await _updateDeparmentEmployerHelpCommandHandler.Handle(new Business.CompanyAdmin.Command.UpdateEmployerHelpForDeparmentCommand
            {
                Id = 1,
                HelpCategoryId = 1,
                DepartmentId = 1,
                Contribution = "contt",
                Requestvia = "reqviaa",
                Description = "newdescp",
                MoreInformation = "moreinfoo"


            }, CancellationToken.None);
            _scenariocontext.Add("result",result); 
        }

        [Then(@"the employer help is updated sucessfully")]
        public void ThenTheEmployerHelpIsUpdatedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Employer helps with id {test.Id} update"));  

        }
    }
}
