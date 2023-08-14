using AutoMapper;
using Azure.Core;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using CompanyHelp = Energie.Domain.Domain.CompanyHelp;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class CompanyUserEnergyTipQueryHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpRepository> _companyHelpTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly CompanyUserEnergyTipQueryHandler _companyUserEnergyTipQueryHandler;
        private readonly Mock<ILogger<CompanyUserEnergyTipQueryHandler>> _logger;
        private readonly IMapper _mapper;
        private readonly Mock<IUserEnergyTipRepository> _userEnergyTipRepository;
        //  private readonly Mock<TranslationService<CompanyHelp>> _translationService;
        private readonly Mock<ITranslationsRepository<CompanyHelp>> _translationService;


        public CompanyUserEnergyTipQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _companyHelpTipRepository = new Mock<ICompanyHelpRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _scenariocontext = scenariocontext;
            //_translationService = translationService;
            _translationService = new Mock<ITranslationsRepository<CompanyHelp>>();
            _logger = new Mock<ILogger<CompanyUserEnergyTipQueryHandler>>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _userEnergyTipRepository = new Mock<IUserEnergyTipRepository>();
            _companyUserEnergyTipQueryHandler = new CompanyUserEnergyTipQueryHandler(_companyHelpTipRepository.Object, _companyUserRepository.Object, _logger.Object, _mapper, _userEnergyTipRepository.Object, _translationService.Object);

        }

        //Compnay Helps retrieved sucessfully
        [Given(@"the command to get the company helps")]
        public void GivenTheCommandToGetTheCompanyHelps()
        {

            var company = Domain.Domain.Company.Create("Ariqt");
            var department = new Domain.Domain.Department().SetCompanyDepartment("HR", 1);
            var newcompanyuser = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            var newhelpcategory = new HelpCategory().SetHelpCategory("mental health", "descp");
            var newcompanyhelp = new Domain.Domain.CompanyHelp().SetCompanyHelp("newhelp", "descp", "cont", "req", "reqvia", 1, 1);
            newcompanyhelp.HelpCategory = newhelpcategory;
            newcompanyhelp.Company = company;
            var userfavhelp = new UserFavouriteHelp().SetUserFavouriteHelp(1, 1);
            userfavhelp.CompanyHelp = newcompanyhelp;
            userfavhelp.CompanyUser = newcompanyuser;

            _companyHelpTipRepository.Setup(x => x.GetCompanyHelpsByCategoryIdAsync(1)).ReturnsAsync(new List<Domain.Domain.CompanyHelp>() { newcompanyhelp });
           _userEnergyTipRepository.Setup(x => x.GetUserFavoritesAsync("abc@gmail.com")).ReturnsAsync
                ((
                   new List<UserFavouriteTip>(),
                   new List<UserFavouriteHelp>() { new UserFavouriteHelp() },
                   new List<UserTip>()
               ));

        }

        [When(@"the command is handled to get company helps")]
        public async void WhenTheCommandIsHandledToGetCompanyHelps()
        {
            var result = await _companyUserEnergyTipQueryHandler.Handle(new Business.Energie.Query.CompanyUserEnergyTipQuery { companyCategoryId = 1, UserEmail = "abc@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"the company helps are retrieved")]
        public void ThenTheCompanyHelpsAreRetrieved()
        {
            var test = _scenariocontext.Get<CompanyUserEnergyTipList>("result");
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.CompanyUserEnergyTips.Count);
          
        }


        //Translated Compnay Helps retrieved sucessfully
        [Given(@"The command to get the translated company helps")]
        public void GivenTheCommandToGetTheTranslatedCompanyHelps()
        {
            var company = Domain.Domain.Company.Create("Ariqt");
            var department = new Domain.Domain.Department().SetCompanyDepartment("HR", 1);
            var newcompanyuser = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            var newhelpcategory = new HelpCategory().SetHelpCategory("mental health", "descp");
            var newcompanyhelp = new Domain.Domain.CompanyHelp().SetCompanyHelp("newhelp", "descp", "cont", "req", "reqvia", 1, 1);
            newcompanyhelp.HelpCategory = newhelpcategory;
            newcompanyhelp.Company = company;
            var userfavhelp = new UserFavouriteHelp().SetUserFavouriteHelp(1, 1);
            userfavhelp.CompanyHelp = newcompanyhelp;
            userfavhelp.CompanyUser = newcompanyuser;

            _companyHelpTipRepository.Setup(x => x.GetCompanyHelpsByCategoryIdAsync(1)).ReturnsAsync(new List<Domain.Domain.CompanyHelp>() { newcompanyhelp });
            _userEnergyTipRepository.Setup(x => x.GetUserFavoritesAsync("abc@gmail.com")).ReturnsAsync
                 ((
                    new List<UserFavouriteTip>(),
                    new List<UserFavouriteHelp>() { new UserFavouriteHelp() },
                    new List<UserTip>()
                ));
        }

        [When(@"The command is handled to get translated company helps")]
        public async void WhenTheCommandIsHandledToGetTranslatedCompanyHelps()
        {
            var result = await _companyUserEnergyTipQueryHandler.Handle(new Business.Energie.Query.CompanyUserEnergyTipQuery { companyCategoryId = 1, UserEmail = "abc@gmail.com",Language = "en-US" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"The translated company helps are retrieved")]
        public void ThenTheTranslatedCompanyHelpsAreRetrieved()
        {
            var test = _scenariocontext.Get<CompanyUserEnergyTipList>("result");
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.CompanyUserEnergyTips.Count);

        }

    }
}
