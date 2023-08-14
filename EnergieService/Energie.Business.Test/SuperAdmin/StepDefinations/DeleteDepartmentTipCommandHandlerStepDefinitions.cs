using AutoMapper;
using Azure.Core;
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
    public class DeleteDepartmentTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<DeleteDepartmentTipCommandHandler>> _logger;
        private readonly Mock<IEnergyTipRepository> _energyTipRepository;
        private readonly ScenarioContext _scenarioContext;
        private Mock<IMapper> _mapper;
        private readonly DeleteDepartmentTipCommandHandler deleteDepartmentTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public DeleteDepartmentTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _logger = new Mock<ILogger<DeleteDepartmentTipCommandHandler>>();
            _energyTipRepository = new Mock<IEnergyTipRepository>();
            _mapper = new Mock<IMapper>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            deleteDepartmentTipCommandHandler = new DeleteDepartmentTipCommandHandler(
                _logger.Object,
                _energyTipRepository.Object, _localizer.Object); 


        }
        [Given(@"the command to delete department tip")]
        public void GivenTheCommandToDeleteDepartmentTip()
        {
            _energyTipRepository.Setup(x => x.DeleteDepartmentTipAsync(1));
            _localizer.Setup(x => x["Department_tip_removed"]).Returns(new LocalizedString("Department_tip_removed", "Department tip removed"));

        }

        [When(@"the command is handled to delete department tip")]
        public async void WhenTheCommandIsHandledToDeleteDepartmentTip()
        {
            var result = await deleteDepartmentTipCommandHandler.Handle(new Business.SuperAdmin.Command.DeleteDepartmentTipCommand {Id=1},CancellationToken.None);  
            _scenarioContext.Add("result",result);  
            
        }

        [Then(@"the department tip is deleted sucessfully")]
        public void ThenTheDepartmentTipIsDeletedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            //Assert.That(test.Message, Is.EqualTo($"Department with " + test.Id + " id deleted"));  

        }
    }
}
