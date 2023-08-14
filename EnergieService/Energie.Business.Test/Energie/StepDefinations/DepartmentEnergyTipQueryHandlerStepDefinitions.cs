using AutoMapper;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class DepartmentEnergyTipQueryHandlerStepDefinitions

    {
        private readonly Mock<ILogger<DepartmentEnergyTipQueryHandler>> _logger;
        private readonly Mock<IDepartmentTipRepository> _departmentTipRepository;
        private readonly Mock<ILikeTipRepository> _likeTipRepository; 
        private readonly IMapper _mapper; 
        private readonly ScenarioContext _scenariocontext;
        private readonly DepartmentEnergyTipQueryHandler _departmentEnergyTipQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.DepartmentTip>> _translationService;

        public DepartmentEnergyTipQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _logger = new Mock<ILogger<DepartmentEnergyTipQueryHandler>>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper=mapper; 
            }
            _departmentTipRepository = new Mock<IDepartmentTipRepository>();
            _likeTipRepository = new Mock<ILikeTipRepository>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.DepartmentTip>>();
            _departmentEnergyTipQueryHandler = new DepartmentEnergyTipQueryHandler(_logger.Object, 
                _departmentTipRepository.Object, 
                _mapper,
                _likeTipRepository.Object, _translationService.Object);  

        }
        // Energy Tip List retreived sucessfully
        [Given(@"the command to get Energy Tip List")]
        public void GivenTheCommandToGetEnergyTipList()
        {
            var department = new Domain.Domain.Department().SetCompanyDepartment("Hr",1);
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("abc","abc@gmail.com",1);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("abcd", DateTime.Now); 
            var questions = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("pqrs","xyz",1);
            var departmenttip = new Domain.Domain.DepartmentTip().SetDepartMentTip("tip","new tip",1);
            departmenttip.EnergyAnalysisQuestions = questions;
            var liketipss = new LikeTip().SetLikeTip(1, 1);
            liketipss.CompanyUsers = user;
            liketipss.DepartmentTip=departmenttip; 
            var deptfavtip = new Domain.Domain.DepartmentFavouriteTip().SetDepartmentFavouriteTip(1,1);
            deptfavtip.DepartmentTip = departmenttip;
            deptfavtip.CompanyUser = user;  
 

            _departmentTipRepository.Setup(x => x.GetDepatrmentTipByListAsync(1)).ReturnsAsync(new List<Domain.Domain.DepartmentTip>() { departmenttip });
            _departmentTipRepository.Setup(x => x.UserFavouriteDepartmentTipListAsync("abc@gmail.com")).ReturnsAsync(new List<Domain.Domain.DepartmentFavouriteTip>(){ deptfavtip });
            _likeTipRepository.Setup(x=>x.GetLikeTipListAsync("abc@gmail.com")).ReturnsAsync(new List<LikeTip>() { liketipss });
        }

        [When(@"the command is handled to get Energy Tipp List")]
        public async void WhenTheCommandIsHandledToGetEnergyTippList()
        {
            var result = await _departmentEnergyTipQueryHandler.Handle(new Business.Energie.Query.DepartmentEnergyTipQuery {Id=1,UserEmail= "abc@gmail.com" },CancellationToken.None); 
            _scenariocontext.Add("result",result); 

        }

        [Then(@"the list of Energy Tip is retrieved")]
        public void ThenTheListOfEnergyTipIsRetrieved()
        {
            var test = _scenariocontext.Get<DepartmentEnergyTipList>("result");
            Assert.IsNotNull(test);  

        }


        //Translated energy Tip List retreived sucessfully
        [Given(@"The command to get translated Energy Tip List")]
        public void GivenTheCommandToGetTranslatedEnergyTipList()
        {
            var translatedData = new List<Domain.Domain.DepartmentTip>()
            {
                new Domain.Domain.DepartmentTip().SetDepartMentTip("Translated tip"," Translated new tip",1)
            };
            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>("en-US")).ReturnsAsync(translatedData);

            var department = new Domain.Domain.Department().SetCompanyDepartment("Hr", 1);
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("abc", "abc@gmail.com", 1);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("abcd", DateTime.Now);
            var questions = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("pqrs", "xyz", 1);
            var departmenttip = new Domain.Domain.DepartmentTip().SetDepartMentTip("tip", "new tip", 1);
            departmenttip.EnergyAnalysisQuestions = questions;
            var liketipss = new LikeTip().SetLikeTip(1, 1);
            liketipss.CompanyUsers = user;
            liketipss.DepartmentTip = departmenttip;
            var deptfavtip = new Domain.Domain.DepartmentFavouriteTip().SetDepartmentFavouriteTip(1, 1);
            deptfavtip.DepartmentTip = departmenttip;
            deptfavtip.CompanyUser = user;


            _departmentTipRepository.Setup(x => x.GetDepatrmentTipByListAsync(1)).ReturnsAsync(new List<Domain.Domain.DepartmentTip>() { departmenttip });
            _departmentTipRepository.Setup(x => x.UserFavouriteDepartmentTipListAsync("abc@gmail.com")).ReturnsAsync(new List<Domain.Domain.DepartmentFavouriteTip>() { deptfavtip });
            _likeTipRepository.Setup(x => x.GetLikeTipListAsync("abc@gmail.com")).ReturnsAsync(new List<LikeTip>() { liketipss });


        }

        [When(@"The command is handled to get translated Energy Tipp List")]
        public async void WhenTheCommandIsHandledToGetTranslatedEnergyTippList()
        {
            var result = await _departmentEnergyTipQueryHandler.Handle(new Business.Energie.Query.DepartmentEnergyTipQuery { Id = 1, UserEmail = "abc@gmail.com",Language = "en-US" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"The list of translated Energy Tip is retrieved")]
        public void ThenTheListOfTranslatedEnergyTipIsRetrieved()
        {
            var test = _scenariocontext.Get<DepartmentEnergyTipList>("result");
            Assert.IsNotNull(test);
        }

    }
}
