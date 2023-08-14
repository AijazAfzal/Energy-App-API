using AutoMapper;
using Energie.Business.SuperAdmin.QueryHandler;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.SuperAdmin.StepDefinations
{
    [Binding]
    public class DepartmentTipListQueryHandlerStepDefinitions
    {
        private readonly Mock <IEnergyTipRepository> _energyTipRepository;
        private readonly Mock <ILogger<DepartmentTipListQueryHandler>> _logger;
        private readonly ScenarioContext _scenarioContext;
        private readonly DepartmentTipListQueryHandler _departmentTipListQueryHandler;
        public DepartmentTipListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _energyTipRepository = new Mock<IEnergyTipRepository>();
            _logger = new Mock<ILogger<DepartmentTipListQueryHandler>>();
            _departmentTipListQueryHandler = new DepartmentTipListQueryHandler(_energyTipRepository.Object,_logger.Object); 

        }
        [Given(@"the command to retrieve Department Tip List")]
        public void GivenTheCommandToRetrieveDepartmentTipList()
        {
            var energyquestions = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("ques", "desp", 1);
            energyquestions.EnergyAnalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var departmentTip = new Domain.Domain.DepartmentTip().SetDepartMentTip("tip", "tip descp", energyquestions.Id); 
            departmentTip.EnergyAnalysisQuestions = energyquestions;

            _energyTipRepository.Setup(x => x.GetDepatrmentTipListAsync()).ReturnsAsync( new List<Domain.Domain.DepartmentTip>() { departmentTip } ); 

          



        }

        [When(@"the command is handled to get the Department Tip List")]
        public async void WhenTheCommandIsHandledToGetTheDepartmentTipList()
        {
            var result =await  _departmentTipListQueryHandler.Handle(new Business.SuperAdmin.Query.DepartmentTipListQuery(),CancellationToken.None); 
            _scenarioContext.Add("result",result);  

        }

        [Then(@"the department tip list is retrieved")]
        public void ThenTheDepartmentTipListIsRetrieved()
        {
            var test = _scenarioContext.Get<DepartmentTipList>("result"); 
            Assert.IsNotNull(test); 

        }
    }
}
