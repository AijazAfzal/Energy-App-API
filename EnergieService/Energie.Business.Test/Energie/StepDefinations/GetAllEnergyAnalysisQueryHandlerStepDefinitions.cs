using AutoMapper;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class GetAllEnergyAnalysisQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<GetAllEnergyAnalysisQueryHandler>> _logger;
        private readonly Mock<IClaimService> _tokenClaimService;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly IMapper _mapper;
        private readonly GetAllEnergyAnalysisQueryHandler _getAllEnergyAnalysisQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions>> _translationService;

        public GetAllEnergyAnalysisQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<GetAllEnergyAnalysisQueryHandler>>();
            _tokenClaimService = new Mock<IClaimService>();
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions>>();
            _scenariocontext = scenariocontext;
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _getAllEnergyAnalysisQueryHandler = new GetAllEnergyAnalysisQueryHandler(_logger.Object,
                _tokenClaimService.Object,
                _energyAnalysisRepository.Object,
                _mapper, _translationService.Object);


        }

        //Energy Analysis Questions retrieved sucessfully
        [Given(@"the command to get all energy analysis questions")]
        public void GivenTheCommandToGetAllEnergyAnalysisQuestions()
        {
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "newuser@gmail.com", 1);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energy = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("abc", "kjh", 1);
            energy.EnergyAnalysis = energyanalysis;
            var userfavouriteHelp = new
                                        Domain.Domain.UserFavouriteHelp()
                                        .SetUserFavouriteHelp(0, 0);
            userfavouriteHelp.CompanyUser = user;
            _energyAnalysisRepository
                .Setup(x => x.GetAllEnergyAnalysisQuestions())
                .ReturnsAsync(new List<Domain.Domain.EnergyAnalysisQuestions>() { energy });

            _energyAnalysisRepository
               .Setup(x => x.UserEnergyAnalysisAsync("newuser@gmail.com"));
              
            var userenergyanalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(0, 0);
            userenergyanalysis.CompanyUser = user;
            userenergyanalysis.EnergyAnalysisQuestions = energy;

            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("newuser@gmail.com");

            var userasnalysis = _energyAnalysisRepository
                                                    .Setup(x => x.UserEnergyAnalysisAsync("newuser@gmail.com"))
                                                    .ReturnsAsync(new List<UserEnergyAnalysis>() { userenergyanalysis });

        }

        [When(@"the command is handled to get the list of questions")]
        public async void WhenTheCommandIsHandledToGetTheListOfQuestions()
        {
            var result = await _getAllEnergyAnalysisQueryHandler.Handle(new Business.Energie.Query.GetAllEnergyAnalysisQuery {language = "nl-NL",UserEmail = "newuser@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);

        }

        [Then(@"the list of energy analysis questions is retrieved")]
        public void ThenTheListOfEnergyAnalysisQuestionsIsRetrieved()
        {
            var test = _scenariocontext.Get<ListEnergyAnalysisQuestions>("result");
            Assert.IsNotNull(test);

        }


        // Translated Energy Analysis Questions retrieved sucessfully
        [Given(@"The command to get all translated energy analysis questions")]
        public void GivenTheCommandToGetAllTranslatedEnergyAnalysisQuestions()
        {
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "newuser@gmail.com", 1);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energy = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("abc", "kjh", 1);
            energy.EnergyAnalysis = energyanalysis;
            var userfavouriteHelp = new
                                        Domain.Domain.UserFavouriteHelp()
                                        .SetUserFavouriteHelp(0, 0);
            userfavouriteHelp.CompanyUser = user;
            _energyAnalysisRepository
                .Setup(x => x.GetAllEnergyAnalysisQuestions())
                .ReturnsAsync(new List<Domain.Domain.EnergyAnalysisQuestions>() { energy });

            _energyAnalysisRepository
               .Setup(x => x.UserEnergyAnalysisAsync("newuser@gmail.com"));

            var userenergyanalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(0, 0);
            userenergyanalysis.CompanyUser = user;
            userenergyanalysis.EnergyAnalysisQuestions = energy;

            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("newuser@gmail.com");

            var userasnalysis = _energyAnalysisRepository
                                                    .Setup(x => x.UserEnergyAnalysisAsync("newuser@gmail.com"))
                                                    .ReturnsAsync(new List<UserEnergyAnalysis>() { userenergyanalysis });

            var translatedData = new List<Domain.Domain.EnergyAnalysisQuestions>()
            {
                new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("abc", "kjh", 1)
            };
            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>("en-US")).ReturnsAsync(translatedData);



        }

        [When(@"The command is handled to get the translated list of questions")]
        public async void WhenTheCommandIsHandledToGetTheTranslatedListOfQuestions()
        {
            var result = await _getAllEnergyAnalysisQueryHandler.Handle(new Business.Energie.Query.GetAllEnergyAnalysisQuery { language = "en-US", UserEmail = "newuser@gmail.com" }, CancellationToken.None);
            _scenariocontext.Add("result", result);

        }

        [Then(@"The translated list of energy analysis questions is retrieved")]
        public void ThenTheTranslatedListOfEnergyAnalysisQuestionsIsRetrieved()
        {
            var test = _scenariocontext.Get<ListEnergyAnalysisQuestions>("result");
            Assert.IsNotNull(test);
        }

    }
}
