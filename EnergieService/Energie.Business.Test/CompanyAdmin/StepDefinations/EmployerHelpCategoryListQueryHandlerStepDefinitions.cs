using AutoMapper;
using Energie.Api;
using Energie.Business.CompanyAdmin.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.CompanyAdmin.StepDefinations
{
    [Binding]
    public class EmployerHelpCategoryListQueryHandlerStepDefinitions
    {
        private readonly Mock<ICompanyHelpCategoryRepository> _companyHelpCategoryRepository;
        private readonly Mock<ILogger<EmployerHelpCategoryListQueryHandler>> _logger;
        private readonly IMapper _mapper;
        private readonly ScenarioContext _scenariocontext; 
        private readonly EmployerHelpCategoryListQueryHandler _employerHelpCategoryListQueryHandler;
        private readonly Mock<ITranslationsRepository<Domain.Domain.HelpCategory>> _translationService;
        public EmployerHelpCategoryListQueryHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _companyHelpCategoryRepository = new Mock<ICompanyHelpCategoryRepository>();
            _logger = new Mock<ILogger<EmployerHelpCategoryListQueryHandler>>();
            _scenariocontext = scenariocontext; 
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _translationService = new Mock<ITranslationsRepository<Domain.Domain.HelpCategory>>();
                _mapper = mapper;
            }

            _employerHelpCategoryListQueryHandler = new EmployerHelpCategoryListQueryHandler(_companyHelpCategoryRepository.Object,_logger.Object,_mapper, _translationService.Object); 



        }
        [Given(@"the command to get employer help category list")]
        public void GivenTheCommandToGetEmployerHelpCategoryList()
        {
            var helpcategory = new HelpCategory().SetHelpCategory("newcategory","descp");  
            _companyHelpCategoryRepository.Setup(x => x.GetEmployerHelpCategoriesAsync()).ReturnsAsync(new List<HelpCategory>() { helpcategory });

        }

        [When(@"the command is handled to get the listtt")]
        public async void WhenTheCommandIsHandledToGetTheListtt()
        {
            var result = await _employerHelpCategoryListQueryHandler.Handle(new Business.CompanyAdmin.Query.EmployerHelpCategoryListQuery { },CancellationToken.None);
            _scenariocontext.Add("result",result); 
            
        }

        [Then(@"the  Employer help category list is retrieved sucessfullyy")]
        public void ThenTheEmployerHelpCategoryListIsRetrievedSucessfullyy()
        {
            var test = _scenariocontext.Get<EmployerHelpCategoryList>("result"); 
            Assert.IsNotNull(test); 

        }
    }
}
