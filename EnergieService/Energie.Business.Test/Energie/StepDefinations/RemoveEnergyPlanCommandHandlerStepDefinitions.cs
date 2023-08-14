using Azure.Core;
using Energie.Business.Energie.CommandHandler;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class RemoveEnergyPlanCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<RemoveEnergyPlanCommandHandler>> _logger;
        private readonly Mock<IEnergyPlanRepository> _energyPlanRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly RemoveEnergyPlanCommandHandler _removeEnergyPlanCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public RemoveEnergyPlanCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<RemoveEnergyPlanCommandHandler>>();
            _energyPlanRepository = new Mock<IEnergyPlanRepository>();
            _scenarioContext = scenariocontext;
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _removeEnergyPlanCommandHandler = new RemoveEnergyPlanCommandHandler(_logger.Object,_energyPlanRepository.Object,_localizer.Object);
            
        }
        [Given(@"the command to delete Energy Plan")]
        public void GivenTheCommandToDeleteEnergyPlan()
        {
            _energyPlanRepository.Setup(x=>x.DeleteEnergyPlanAsync(1));
            _localizer.Setup(x => x["Energy_plan_removed"]).Returns(new LocalizedString("Energy_plan_removed", "Energy plan removed"));

        }

        [When(@"the command is handeld to remove Plan")]
        public async void WhenTheCommandIsHandeldToRemovePlan()
        {
            var result = await _removeEnergyPlanCommandHandler.Handle(new Business.Energie.Command.RemoveEnergyPlanCommand { EnergyPlanId=1 },CancellationToken.None);
            _scenarioContext.Add("result",result); 
            
        }

        [Then(@"the Plan is removed sucessfully")]
        public void ThenThePlanIsRemovedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Energy plan removed"));  

        }
    }
}
