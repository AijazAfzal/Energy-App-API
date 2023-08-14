using AutoMapper;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class GetUserEnergyTipQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<GetUserEnergyTipQueryHandler>> _logger;
        private readonly Mock<IUserEnergyTipRepository> _userEnergyTipRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly IMapper _mapper;
        private readonly ScenarioContext _scenariocontext;
        private readonly GetUserEnergyTipQueryHandler _getUserEnergyTipQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.Tip>> _translationService;
        public GetUserEnergyTipQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<GetUserEnergyTipQueryHandler>>();
            _userEnergyTipRepository = new Mock<IUserEnergyTipRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            if (_mapper == null) 
            { 
                var mappingConfig = new MapperConfiguration(mc => {
                    mc.AddProfile(new MappingProfile()); 
                }); 
                IMapper mapper = mappingConfig.CreateMapper(); 
                _mapper = mapper; 
            }
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.Tip>>();
            _scenariocontext = scenariocontext;
            _getUserEnergyTipQueryHandler = new GetUserEnergyTipQueryHandler(_logger.Object,_userEnergyTipRepository.Object,_categoryRepository.Object,_energyAnalysisRepository.Object,_mapper, _translationService.Object); 
                                                                                           

        }
        //Energy Tip List retrieved sucessfully
        [Given(@"the command to get Energy Tip Listt")]
        public  void GivenTheCommandToGetEnergyTipListt()
        {
            var newuser = new CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            var energyanalysis = new EnergyAnalysis().SetEnergyAnalysis("newanalysis",DateTime.Now); 
            var energyanalysisisquestions = new EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("newquestions","descp",1);
            energyanalysisisquestions.EnergyAnalysis = energyanalysis;
            _energyAnalysisRepository.Setup(x=>x.EnergyAnalysisQuestionsAsyncById(1)).ReturnsAsync(energyanalysisisquestions); 
            var newtip = new Tip().SetEnergyTip(1, "newtip", "decp");
            newtip.EnergyAnalysisQuestions = energyanalysisisquestions;
            _userEnergyTipRepository.Setup(x=>x.GetUserEnergyTip(0)).ReturnsAsync(new List<Tip>() { newtip });
            var userfavtip = new UserFavouriteTip().SetUserFavouriteTip(1,1);
            userfavtip.Tips = newtip;
            userfavtip.CompanyUser = newuser;

            _userEnergyTipRepository.Setup(x => x.GetUserFavoritesAsync("abc@gmail.com")).ReturnsAsync
               ((
                  new List<UserFavouriteTip>(),
                  new List<UserFavouriteHelp>() { new UserFavouriteHelp() },
                  new List<UserTip>()
              ));


        }

        [When(@"the command us handled to get the tip List")]
        public async void WhenTheCommandUsHandledToGetTheTipList()
        {
            var result = await  _getUserEnergyTipQueryHandler.Handle(new Business.Energie.Query.GetUserEnergyTipQuery { id=1,UserEmail= "abc@gmail.com" },CancellationToken.None);
            _scenariocontext.Add("result",result); 

        }

        [Then(@"Energy Tip Listt is retrieved")]
        public void ThenEnergyTipListtIsRetrieved()
        {
            var test = _scenariocontext.Get<UserEnergyTipList>("result");
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.UserEnergyTip.Count);

        }


        // Translated Energy Tip List retrieved sucessfully
        [Given(@"The command to get translated Energy Tip Listt")]
        public void GivenTheCommandToGetTranslatedEnergyTipListt()
        {
            var newuser = new CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            var energyanalysis = new EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energyanalysisisquestions = new EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("newquestions", "descp", 1);
            energyanalysisisquestions.EnergyAnalysis = energyanalysis;
            _energyAnalysisRepository.Setup(x => x.EnergyAnalysisQuestionsAsyncById(1)).ReturnsAsync(energyanalysisisquestions);
            var newtip = new Tip().SetEnergyTip(1, "newtip", "decp");
            newtip.EnergyAnalysisQuestions = energyanalysisisquestions;
            _userEnergyTipRepository.Setup(x => x.GetUserEnergyTip(0)).ReturnsAsync(new List<Tip>() { newtip });
            var userfavtip = new UserFavouriteTip().SetUserFavouriteTip(1, 1);
            userfavtip.Tips = newtip;
            userfavtip.CompanyUser = newuser;

            _userEnergyTipRepository.Setup(x => x.GetUserFavoritesAsync("abc@gmail.com")).ReturnsAsync
               ((
                  new List<UserFavouriteTip>(),
                  new List<UserFavouriteHelp>() { new UserFavouriteHelp() },
                  new List<UserTip>()
              ));



            var translatedData = new List<Domain.Domain.Tip>()
            {
                new Domain.Domain.Tip().SetEnergyTip(1, "newtip", "decp")
            };
            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.Tip>("en-US")).ReturnsAsync(translatedData);

        }

        [When(@"The command is handled to get the translated tip List")]
        public async void WhenTheCommandIsHandledToGetTheTranslatedTipList()
        {
            var result = await _getUserEnergyTipQueryHandler.Handle(new Business.Energie.Query.GetUserEnergyTipQuery { id = 1, UserEmail = "abc@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);

        }

        [Then(@"Translated Energy Tip Listt is retrieved")]
        public void ThenTranslatedEnergyTipListtIsRetrieved()
        {

            var test = _scenariocontext.Get<UserEnergyTipList>("result");
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.UserEnergyTip.Count);
        }

    }
}
