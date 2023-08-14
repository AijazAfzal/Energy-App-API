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
    public class DeleteEnergyTipCommandHandlerStepDefinitions
    {
        private readonly Mock< ILogger<DeleteEnergyTipCommandHandler>> _logger;
        private readonly Mock <IEnergyTipRepository> _energyTipRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IMapper> _mapper;
        private readonly DeleteEnergyTipCommandHandler _deleteEnergyTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public DeleteEnergyTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<DeleteEnergyTipCommandHandler>>();
            _energyTipRepository= new Mock<IEnergyTipRepository>();
            _scenarioContext= scenariocontext;
            _mapper = new Mock<IMapper>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _deleteEnergyTipCommandHandler = new DeleteEnergyTipCommandHandler(_energyTipRepository.Object,_logger.Object, _localizer.Object); 

        }
        [Given(@"the command to delete Energy Tip")]
        public void GivenTheCommandToDeleteEnergyTip()
        {
            _energyTipRepository.Setup(x => x.DeleteEnergyTipAsync(1)).ReturnsAsync(It.IsAny<int>());
            _localizer.Setup(x => x["Tip_removed"]).Returns(new LocalizedString("Tip_removed",  "Tip removed"));

        }

        [When(@"the command is handled to delete Energy Tip")]
        public async void WhenTheCommandIsHandledToDeleteEnergyTip()
        {
            var result = await _deleteEnergyTipCommandHandler.Handle(new 
                                DeleteEnergyTipCommand 
                                { 
                                    TipId=1 
                                },CancellationToken.None); 
            _scenarioContext.Add("result",result);   
            
        }

        [Then(@"the Energy Tip is deleted sucessfully")]
        public void ThenTheEnergyTipIsDeletedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo("Tip removed")); 

        }
    }
}
