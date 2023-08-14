using AutoMapper;
using Energie.Api;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class DepartmentEmployerHelpListByDepartmentQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<DepartmentEmployerHelpListByDepartmentQueryHandler>> _logger;
        private readonly Mock<ICompanyHelpCategoryRepository> _companyHelpCategoryRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<IDepartmentTipRepository> _departmentTipRepository;
        private readonly IMapper _mapper;
        private readonly ScenarioContext _Scenariocontext;
        private readonly DepartmentEmployerHelpListByDepartmentQueryHandler _departmentEmployerHelpListByDepartmentQueryHandler;
        private readonly Mock<ITranslationsRepository<CompanyDepartmentHelp>> _translationService;
        public DepartmentEmployerHelpListByDepartmentQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _logger = new Mock<ILogger<DepartmentEmployerHelpListByDepartmentQueryHandler>>();
            _companyHelpCategoryRepository = new Mock<ICompanyHelpCategoryRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _translationService = new Mock<ITranslationsRepository<CompanyDepartmentHelp>>();
            _departmentTipRepository = new Mock<IDepartmentTipRepository>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            
            _Scenariocontext = scenariocontext;
            _departmentEmployerHelpListByDepartmentQueryHandler = new DepartmentEmployerHelpListByDepartmentQueryHandler(_logger.Object, 
                _companyHelpCategoryRepository.Object, 
                _companyUserRepository.Object, 
                _mapper, 
                _departmentTipRepository.Object, _translationService.Object);

        }

        // Department Employer Help List retrieved sucessfully
        [Given(@"the command to get department employer help list")]
        public void GivenTheCommandToGetDepartmentEmployerHelpList()
        {
            var department = new Domain.Domain.Department().SetCompanyDepartment("HR", 1);
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user);
            var helpcategory = new HelpCategory().SetHelpCategory("newcategory", "descp");
            var companydepartmenthelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp", "descp", "cont", "reqvia", "moreinfo", 1, 1);
            companydepartmenthelp.Department = department;
            companydepartmenthelp.HelpCategory = helpcategory;


            var departmentfavhelp = new DepartmentFavouriteHelp().SetDepartmentFavouriteHelp(1, 1);
            departmentfavhelp.CompanyUser = user;
            departmentfavhelp.CompanyDepartmentHelps = companydepartmenthelp;

            _companyHelpCategoryRepository.Setup(x => x.EmployerHelpByDepartmentIdAsync(1, 1)).ReturnsAsync(new List<CompanyDepartmentHelp>() { companydepartmenthelp });
            _departmentTipRepository.Setup(x => x.DepartmentFavouriteHelpListAsync("abc@gmail.com")).ReturnsAsync(new List<DepartmentFavouriteHelp>() { departmentfavhelp });

            _departmentTipRepository.Setup(x => x.DepartmentFavouriteHelpListAsync("abc@gmail.com")).ReturnsAsync(new List<DepartmentFavouriteHelp>() { departmentfavhelp });


        }

        [When(@"the command is handled to get help list")]
        public async void WhenTheCommandIsHandledToGetHelpList()
        {
            var result = await _departmentEmployerHelpListByDepartmentQueryHandler.Handle(new Business.Energie.Query.DepartmentEmployerHelpListByDepartmentQuery { Id = 1, UserEmail = "abc@gmail.com" }, CancellationToken.None);
            _Scenariocontext.Add("result", result);
        }

        [Then(@"the help list is retrieved sucessfully")]
        public void ThenTheHelpListIsRetrievedSucessfully()
        {
            var test = _Scenariocontext.Get<DepartmentEmployerHelpList>("result");
            Assert.IsNotNull(test);
        }

        //Translated Department Employer Help List retrieved sucessfully
        [Given(@"The command to get translated department employer help list")]
        public void GivenTheCommandToGetTranslatedDepartmentEmployerHelpList()
        {
            var department = new Domain.Domain.Department().SetCompanyDepartment("HR", 1);
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("newuser", "abc@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(user);
            var helpcategory = new HelpCategory().SetHelpCategory("newcategory", "descp");
            var companydepartmenthelp = new CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp", "descp", "cont", "reqvia", "moreinfo", 1, 1);
            companydepartmenthelp.Department = department;
            companydepartmenthelp.HelpCategory = helpcategory;


            var departmentfavhelp = new DepartmentFavouriteHelp().SetDepartmentFavouriteHelp(1, 1);
            departmentfavhelp.CompanyUser = user;
            departmentfavhelp.CompanyDepartmentHelps = companydepartmenthelp;

            _companyHelpCategoryRepository.Setup(x => x.EmployerHelpByDepartmentIdAsync(1, 1)).ReturnsAsync(new List<CompanyDepartmentHelp>() { companydepartmenthelp });
            _departmentTipRepository.Setup(x => x.DepartmentFavouriteHelpListAsync("abc@gmail.com")).ReturnsAsync(new List<DepartmentFavouriteHelp>() { departmentfavhelp });

            _departmentTipRepository.Setup(x => x.DepartmentFavouriteHelpListAsync("abc@gmail.com")).ReturnsAsync(new List<DepartmentFavouriteHelp>() { departmentfavhelp });


            var translatedData = new List<Domain.Domain.CompanyDepartmentHelp>()
            {
                new Domain.Domain.CompanyDepartmentHelp().SetCompanyDepartmentHelp("newhelp", "descp", "cont", "reqvia", "moreinfo", 1, 1)
            };
            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>("en-US")).ReturnsAsync(translatedData);

        }

        [When(@"The command is handled to get translated help list")]
        public async void WhenTheCommandIsHandledToGetTranslatedHelpList()
        {
            var result = await _departmentEmployerHelpListByDepartmentQueryHandler.Handle(new Business.Energie.Query.DepartmentEmployerHelpListByDepartmentQuery { Id = 1, UserEmail = "abc@gmail.com",Language = "en-US" }, CancellationToken.None);
            _Scenariocontext.Add("result", result);
        }

        [Then(@"The translated help list is retrieved sucessfully")]
        public void ThenTheTranslatedHelpListIsRetrievedSucessfully()
        {
            var test = _Scenariocontext.Get<DepartmentEmployerHelpList>("result");
            Assert.IsNotNull(test);
        }

    }
}
