using AutoMapper;
using Energie.Business.SuperAdmin.Command;
using Energie.Business.SuperAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class AddTipCommandHandlerStepDefinitions
    {
        private readonly Mock <ILogger<AddTipCommandHandler>> _logger;
        private readonly Mock <IEnergyTipRepository> _energyTipsRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly AddTipCommandHandler addEnergyTipsCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public AddTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _energyTipsRepository = new Mock<IEnergyTipRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _logger = new Mock<ILogger<AddTipCommandHandler>>();
            addEnergyTipsCommandHandler = new AddTipCommandHandler(
                                                  _logger.Object
                                                , _energyTipsRepository.Object, _localizer.Object); 
        }
        [Given(@"the command to add EnergyTips")]
        public void GivenTheCommandToAddEnergyTips()
        {
            var newtiptobeadded = new Tip().SetEnergyTip(1,"Test Tip","Test Tip Description");
            _energyTipsRepository.Setup(x => x.AddEnergyTipAsync(newtiptobeadded));
            _localizer.Setup(x => x["Tip_added", newtiptobeadded.Id]).Returns(new LocalizedString("Tip_added", $"Tip added with Id {newtiptobeadded.Id}"));

        }

        [When(@"the command is handled to add EnergyTips")]
        public async void WhenTheCommandIsHandledToAddEnergyTips()
        {
            var result = await addEnergyTipsCommandHandler.Handle(new AddTipCommand
                               { 
                                    EnergyAnalysisQuestionId=1,
                                    Description= "Test Tip Description",
                                    Tip= "Test Tip"

            }
                               ,CancellationToken.None); 
            _scenarioContext.Add("result", result);

        }

        [Then(@"category added EnergyTips")]
        public void ThenCategoryAddedEnergyTips()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Tip added with Id {test.Id}")); 

        }
    }
}
