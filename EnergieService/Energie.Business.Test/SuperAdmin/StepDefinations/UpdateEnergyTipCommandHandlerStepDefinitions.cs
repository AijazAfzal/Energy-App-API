using AutoMapper;
using Energie.Business.SuperAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Migrations;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class UpdateEnergyTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<UpdateEnergyTipCommandHandler>> _logger;
        private readonly Mock<IEnergyTipRepository> _energyTipRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IMapper> _mapper;
        private readonly UpdateEnergyTipCommandHandler _updateEnergyTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public UpdateEnergyTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<UpdateEnergyTipCommandHandler>>();
            _energyTipRepository=new Mock<IEnergyTipRepository>();
            _scenarioContext=scenariocontext;
            _mapper = new Mock<IMapper>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _updateEnergyTipCommandHandler = new UpdateEnergyTipCommandHandler(_energyTipRepository.Object,_logger.Object, _localizer.Object); 
            

        }
        [Given(@"the command to update Energy Tip")]
        public void GivenTheCommandToUpdateEnergyTip()
        {
            var newenergytip = new Tip().SetEnergyTip(1,"sdf","ghj");
            var energytiptobeupdated = _energyTipRepository.Setup(x => x.GetEnergyTipByIdAsync(1)).ReturnsAsync(newenergytip); 
            var updatedenergytip = newenergytip.UpdateEnergyTip(1,"abc","fjk");
            _energyTipRepository.Setup(x => x.UpdateEnergyTipAsync(updatedenergytip));
            _localizer.Setup(x => x["Tip_Update"]).Returns(new LocalizedString("Tip_Update", "Tip Update"));



        }

        [When(@"the command is handled to update Energy Tip")]
        public async void WhenTheCommandIsHandledToUpdateEnergyTip()
        {
            var result = await _updateEnergyTipCommandHandler.Handle(new Business.SuperAdmin.Command.UpdateEnergyTipCommand 
                               { Id=1,
                                Name= "abc",
                                Description= "fjk",
                                CategoryId=1


            }
            ,CancellationToken.None); 
            _scenarioContext.Add("result",result); 
            
        }

        [Then(@"Energy Tip is updated sucessfully")]
        public void ThenEnergyTipIsUpdatedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Tip Update")); 

        }
    }
}
