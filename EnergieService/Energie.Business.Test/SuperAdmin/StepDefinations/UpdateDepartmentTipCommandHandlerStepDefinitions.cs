using AutoMapper;
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
    public class UpdateDepartmentTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<UpdateDepartmentTipCommandHandler>> _logger;
        private readonly Mock<IEnergyTipRepository> _departmentTipRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IMapper> _mapper;
        private readonly UpdateDepartmentTipCommandHandler _updateDepartmentTipCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;


        public UpdateDepartmentTipCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<UpdateDepartmentTipCommandHandler>>();
            _departmentTipRepository = new Mock<IEnergyTipRepository>();
            _scenarioContext=scenariocontext;
            _mapper = new Mock<IMapper>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _updateDepartmentTipCommandHandler = new UpdateDepartmentTipCommandHandler(_logger.Object,_departmentTipRepository.Object, _localizer.Object);
          

        }
        [Given(@"the command to update department tip")]
        public void GivenTheCommandToUpdateDepartmentTip()
        {
            var newdepartmenttip = new DepartmentTip().SetDepartMentTip("abc","xyz",1);
            var departmenttiptobeupdated = _departmentTipRepository.Setup(x => x.GetDepatrmentTipByIdAsync(1)).ReturnsAsync(newdepartmenttip); 
            var updatingtip = newdepartmenttip.UpdateDepartMentTip("klm","fgh",1);
            _departmentTipRepository.Setup(x => x.UpdateDepartmentTipAsync(updatingtip));
            _localizer.Setup(x => x["Update_department_tip", newdepartmenttip.ID]).Returns(new LocalizedString("Update_department_tip", $"Department tip with Id {newdepartmenttip.ID} update"));
        }

        [When(@"the command is handled to update department tip")]
        public async void WhenTheCommandIsHandledToUpdateDepartmentTip()
        {
            var result = await  _updateDepartmentTipCommandHandler.Handle(new Business.SuperAdmin.Command.UpdateDepartmentTipCommand  
                                 { Id=1,
                                 EnergyAnalysisQuestionId=1,
                                 DepartmentTip= "klm",
                                 Description= "fgh" 

            }
            ,CancellationToken.None);
            _scenarioContext.Add("result",result);  
           
        }

        [Then(@"the department tip is updated")]
        public void ThenTheDepartmentTipIsUpdated()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Department tip with Id {test.Id} update"));  

        }
    }
}
