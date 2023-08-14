using AutoMapper;
using Energie.Business.SuperAdmin.Command;
using Energie.Business.SuperAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Energie.Model.Request;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class AddDepartmentTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<AddDepartmentTipCommandHandler>> _logger;
        private readonly Mock<IEnergyTipRepository> _energyTipRepository;
        private readonly ScenarioContext _scenarioContext;
        private Mock<IMapper> _mapper;
        private readonly AddDepartmentTipCommandHandler _addDepartmentTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public AddDepartmentTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<AddDepartmentTipCommandHandler>>();
            _energyTipRepository = new Mock<IEnergyTipRepository>();
            _scenarioContext = scenariocontext;
            _mapper = new Mock<IMapper>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _addDepartmentTipCommandHandler = new AddDepartmentTipCommandHandler(_energyTipRepository.Object, _logger.Object, _localizer.Object);

        }
        [Given(@"the command to add department tip")]
        public void GivenTheCommandToAddDepartmentTip()
        {
            var newdepartmenttip = new Domain.Domain.DepartmentTip()
                                       .SetDepartMentTip("Test Department Tip", "Test Department Tip Description", 1);
            _energyTipRepository.Setup(x => x.AddDepartmentTipAsync(newdepartmenttip));
            _localizer.Setup(x => x["Department_added", newdepartmenttip.ID]).Returns(new LocalizedString("Department_added", $"Department added with {newdepartmenttip.ID}"));
        }

        [When(@"the command is is handled to add department tip")]
        public async void WhenTheCommandIsIsHandledToAddDepartmentTip()
        {
            var result = await _addDepartmentTipCommandHandler.Handle(new AddDepartmentTipCommand
            {
                EnergyAnalysisQuestionId = 1,
                DepartmentTip = "Test Department Tip",
                Description = "Test Department Tip Description"
            }
            , CancellationToken.None);
            _scenarioContext.Add("result", result);
        }
        [Then(@"department tip is added sucessfully")]
        public void ThenDepartmentTipIsAddedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Department added with { test.Id}")); 

        }
    }
}
