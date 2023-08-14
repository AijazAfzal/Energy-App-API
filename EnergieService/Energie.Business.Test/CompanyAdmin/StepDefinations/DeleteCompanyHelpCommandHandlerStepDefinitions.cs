using AutoMapper;
using Azure.Core;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.CompanyAdmin.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Frameworks;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.StepDefinitions
{
    [Binding]
    public class DeleteCompanyHelpCommandHandlerStepDefinitions
    {
        private readonly Mock<ILogger<DeleteCompanyHelpCommandHandler>> _logger;
        private readonly Mock <ICompanyHelpRepository> _companyHelpTipRepository;
        private readonly Mock<ICompanyAdminRepository> _companyAdminRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly DeleteCompanyHelpCommandHandler _deleteCompanyHelpCommandHandler;
        private DeleteCompanyHelpCommand _deleteCompanyHelpCommand;
        private DeleteCompanyHelpCommand _command;
        private ResponseMessage _responseMessage;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public DeleteCompanyHelpCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
            _logger = new Mock<ILogger<DeleteCompanyHelpCommandHandler>>();
            _companyHelpTipRepository= new Mock<ICompanyHelpRepository>();
            _companyAdminRepository= new Mock<ICompanyAdminRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _deleteCompanyHelpCommandHandler = new DeleteCompanyHelpCommandHandler(_logger.Object
                , _companyHelpTipRepository.Object
                , _companyAdminRepository.Object
                , _localizer.Object); 
        }
        [Given(@"the command to delete company help")]
        public void GivenTheCommandToDeleteCompanyHelp()
        {
            _command = new DeleteCompanyHelpCommand { Id = 1, UserEmail = "test@gmail.com" };


            var companyadmin = new Domain.Domain.CompanyAdmin().SetCompanyAdmin(1,"testuser","test@gmail.com");
            _companyAdminRepository.Setup(x=>x.GetCompanyAdminByEmailAsync("test@gmail.com")).ReturnsAsync(companyadmin);
            _localizer.Setup(x => x["Business_tips_with_ID_remote", _command.Id]).Returns(new LocalizedString("Business_tips_with_ID_remote", $"Business tips with ID {_command.Id} remove"));
            _companyHelpTipRepository.Setup(x => x.DeleteCompanyHelpAsync(1,1));    
        }

        [When(@"the command is handled to delete company help")]
        public async void WhenTheCommandIsHandledToDeleteCompanyHelp()
        {
            var result = await  _deleteCompanyHelpCommandHandler.Handle(_command, CancellationToken.None);
            _scenarioContext.Add("result",result);   
            
        }

        [Then(@"company help deleted sucessfully")]
        public void ThenCompanyHelpDeletedSucessfully()
        {
            var test = _scenarioContext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.AreEqual(test.Message, $"Business tips with ID {test.Id} remove");
            Assert.IsNotNull(test);
            //Assert.That(test.Message, Is.EqualTo($"Company tips with id : " + test.Id + " deleted"));   

        }
    }
}
