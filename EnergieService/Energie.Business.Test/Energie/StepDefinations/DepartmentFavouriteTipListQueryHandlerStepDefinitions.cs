using AutoMapper;
using Azure.Core;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class DepartmentFavouriteTipListQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<DepartmentFavouriteTipListQueryHandler>> _logger;
        private readonly Mock<IDepartmentTipRepository> _departmentTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<ILikeTipRepository> _likeTipRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly DepartmentFavouriteTipListQueryHandler _departmentFavouriteTipListQueryHandler;
        private readonly IMapper _mapper;

        private readonly Mock<ITranslationsRepository<Domain.Domain.DepartmentTip>> _translationService;
        private readonly Mock<ITranslationsRepository<Domain.Domain.DepartmentFavouriteHelp>> _translationDepartmentFavouriteHelp;
        private readonly Mock<ITranslationsRepository<Domain.Domain.UserDepartmentTip>> _translationUserDepartmentTip;
        public DepartmentFavouriteTipListQueryHandlerStepDefinitions(ScenarioContext scensariocontext)
        {
            _scenariocontext = scensariocontext;
            _logger = new Mock<ILogger<DepartmentFavouriteTipListQueryHandler>>();
            _departmentTipRepository = new Mock<IDepartmentTipRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _likeTipRepository = new Mock<ILikeTipRepository>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.DepartmentTip>>();
            _translationDepartmentFavouriteHelp = new Mock<ITranslationsRepository<Domain.Domain.DepartmentFavouriteHelp>>();
            _translationUserDepartmentTip = new Mock<ITranslationsRepository<Domain.Domain.UserDepartmentTip>>();
            _departmentFavouriteTipListQueryHandler = new DepartmentFavouriteTipListQueryHandler(_logger.Object, _departmentTipRepository.Object,_companyUserRepository.Object, _translationService.Object
                , _translationDepartmentFavouriteHelp.Object, _translationUserDepartmentTip.Object, _likeTipRepository.Object, _mapper); 

        }
        //Department Favourite Tip List retreived
        [Given(@"the command to get Department Favourite Tip List")]
        public void GivenTheCommandToGetDepartmentFavouriteTipList()
        {
            var newcompany = Domain.Domain.Company.Create("Ariqt");
            var department = new Domain.Domain.Department().SetCompanyDepartment("HR",1);
            department.Company = newcompany; 
            var companyuser = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "newuser@gmail.com", 1); 
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energyanalysisiquestion = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("energyques", "descp", 1);
            energyanalysisiquestion.EnergyAnalysis = energyanalysis;
            var depttip = new Domain.Domain.DepartmentTip().SetDepartMentTip("newtip", "descp", 1);
            depttip.EnergyAnalysisQuestions = energyanalysisiquestion;
            var departmentfavtip = new Domain.Domain.DepartmentFavouriteTip().SetDepartmentFavouriteTip(1, 1);
            departmentfavtip.DepartmentTip = depttip;
            departmentfavtip.CompanyUser = companyuser;

            var companydepthelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp", "descp", "cont", "req vuia", "moreinfo", 1, 1);
            var deptfavhelp = new DepartmentFavouriteHelp().SetDepartmentFavouriteHelp(1, 1);
            deptfavhelp.CompanyDepartmentHelps = companydepthelp;
            deptfavhelp.CompanyUser = companyuser;

            var useraddeddepttip = new Domain.Domain.UserDepartmentTip().SetUserDepartmentTip(1,"descp",1);
            useraddeddepttip.EnergyAnalysisQuestions = energyanalysisiquestion;
            useraddeddepttip.CompanyUser = companyuser; 


            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("newuser@gmail.com")).ReturnsAsync(companyuser);
            _departmentTipRepository.Setup(x => x.UserFavouriteDepartmentTipListAsync("newuser@gmail.com")).ReturnsAsync(new List<Domain.Domain.DepartmentFavouriteTip>() { departmentfavtip });
            _departmentTipRepository.Setup(x => x.DepartmentFavouriteHelpListAsync("newuser@gmail.com")).ReturnsAsync(new List<DepartmentFavouriteHelp>() { deptfavhelp });
            _departmentTipRepository.Setup(x => x.GetUserAddedDepartmentTip(1)).ReturnsAsync(new List<Domain.Domain.UserDepartmentTip>() { useraddeddepttip });
            _departmentTipRepository.Setup(x => x.GetDepatrmentTipListAsync()).ReturnsAsync(new List<Domain.Domain.DepartmentTip>() { depttip });


        }

        [When(@"the command is handled to get list")]
        public async void WhenTheCommandIsHandledToGetList()
        {
            var result = await _departmentFavouriteTipListQueryHandler.Handle(new Business.Energie.Query.DepartmentFavouriteTipListQuery { UserEmail = "newuser@gmail.com",Language = "en-NL" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"the tip list is retrieved")]
        public void ThenTheTipListIsRetrieved()
        {
            var test = _scenariocontext.Get<DepartmentEnergyTipsList>("result");
            Assert.IsNotNull(test);

        }



        // Translated department Favourite Tip List retreived
        [Given(@"The command to get translated Department Favourite Tip List")]
        public void GivenTheCommandToGetTranslatedDepartmentFavouriteTipList()
        {
            var newcompany = Domain.Domain.Company.Create("Ariqt");
            var department = new Domain.Domain.Department().SetCompanyDepartment("HR", 1);
            department.Company = newcompany;
            var companyuser = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "newuser@gmail.com", 1);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energyanalysisiquestion = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("energyques", "descp", 1);
            energyanalysisiquestion.EnergyAnalysis = energyanalysis;
            var depttip = new Domain.Domain.DepartmentTip().SetDepartMentTip("newtip", "descp", 1);
            depttip.EnergyAnalysisQuestions = energyanalysisiquestion;
            var departmentfavtip = new Domain.Domain.DepartmentFavouriteTip().SetDepartmentFavouriteTip(1, 1);
            departmentfavtip.DepartmentTip = depttip;
            departmentfavtip.CompanyUser = companyuser;

            var companydepthelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp", "descp", "cont", "req vuia", "moreinfo", 1, 1);
            var deptfavhelp = new DepartmentFavouriteHelp().SetDepartmentFavouriteHelp(1, 1);
            deptfavhelp.CompanyDepartmentHelps = companydepthelp;
            deptfavhelp.CompanyUser = companyuser;

            var useraddeddepttip = new Domain.Domain.UserDepartmentTip().SetUserDepartmentTip(1, "descp", 1);
            useraddeddepttip.EnergyAnalysisQuestions = energyanalysisiquestion;
            useraddeddepttip.CompanyUser = companyuser;


            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("newuser@gmail.com")).ReturnsAsync(companyuser);
            _departmentTipRepository.Setup(x => x.UserFavouriteDepartmentTipListAsync("newuser@gmail.com")).ReturnsAsync(new List<Domain.Domain.DepartmentFavouriteTip>() { departmentfavtip });
            _departmentTipRepository.Setup(x => x.DepartmentFavouriteHelpListAsync("newuser@gmail.com")).ReturnsAsync(new List<DepartmentFavouriteHelp>() { deptfavhelp });
            _departmentTipRepository.Setup(x => x.GetUserAddedDepartmentTip(1)).ReturnsAsync(new List<Domain.Domain.UserDepartmentTip>() { useraddeddepttip });
            _departmentTipRepository.Setup(x => x.GetDepatrmentTipListAsync()).ReturnsAsync(new List<Domain.Domain.DepartmentTip>() { depttip });


            var translatedData = new List<Domain.Domain.DepartmentTip>()
            {
                new Domain.Domain.DepartmentTip().SetDepartMentTip("Translated tip"," Translated new tip",1)
            };
            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.DepartmentTip>("en-US")).ReturnsAsync(translatedData);

            var translatedDepartmentFavouriteHelp = new List<Domain.Domain.DepartmentFavouriteHelp>()
            {
              new Domain.Domain.DepartmentFavouriteHelp
                {
                 Id = 1,
                 CompanyDepartmentHelps = new Domain.Domain.CompanyDepartmentHelp().SetCompanyDepartmentHelp("name","description","contribution","requestvia","moreInformation",1,1)
              }
            };
            _translationDepartmentFavouriteHelp.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.DepartmentFavouriteHelp>("en-US"))
                .ReturnsAsync(translatedDepartmentFavouriteHelp);


            var translatedUserDepartmentTip = new List<Domain.Domain.UserDepartmentTip>()
            {
                new Domain.Domain.UserDepartmentTip().SetUserDepartmentTip(1, "descp", 1)
            };
            _translationUserDepartmentTip.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.UserDepartmentTip>("en-US")).ReturnsAsync(translatedUserDepartmentTip);
        }

        [When(@"The command is handled to get translated list")]
        public async void WhenTheCommandIsHandledToGetTranslatedList()
        {
            var result = await _departmentFavouriteTipListQueryHandler.Handle(new Business.Energie.Query.DepartmentFavouriteTipListQuery { UserEmail = "newuser@gmail.com", Language = "en-US" }, CancellationToken.None);
            _scenariocontext.Add("result", result);
        }

        [Then(@"The translated tip list is retrieved")]
        public void ThenTheTranslatedTipListIsRetrieved()
        {
            var test = _scenariocontext.Get<DepartmentEnergyTipsList>("result");
            Assert.IsNotNull(test);
        }

    }
}
