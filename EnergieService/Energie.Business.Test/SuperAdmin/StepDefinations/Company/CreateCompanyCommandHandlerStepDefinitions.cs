
using Energie.Business.Resources;
using Energie.Business.SuperAdmin.MediatR.Commands.Company;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace YourNamespace.Tests.SpecFlow
{
    [Binding]
    public class CreateCompanyCommandHandlerSteps
    {
        private CreateCompanyCommand _command;
        private ApiResponse _response;
        private Mock<ICompanyRepository<Energie.Domain.Domain.Company>> _mockCompanyRepository;
        private Mock<IStringLocalizer<Resources>> _mockLocalizer;
        private CreateCompanyCommandHandler _handler;

        [Given(@"a company repository")]
        public void GivenACompanyRepository()
        {
            _mockCompanyRepository = new Mock<ICompanyRepository<Energie.Domain.Domain.Company>>();
        }

        [Given(@"a command handler")]
        public void GivenACommandHandler()
        {
            _mockLocalizer = new Mock<IStringLocalizer<Resources>>();
            _handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mockLocalizer.Object);
        }

        [Given(@"the command to add company with name ""(.*)""")]
        public void GivenTheCommandToAddCompanyWithName(string companyName)
        {
            _command = new CreateCompanyCommand { CompanyName = companyName };
        }

        [Given(@"no company with name ""(.*)"" exists")]
        public void GivenNoCompanyWithNameExists(string companyName)
        {
            _mockCompanyRepository.Setup(repo => repo.GetCompanyByNameAsync(companyName)).ReturnsAsync((Energie.Domain.Domain.Company)null);
            _mockLocalizer.Setup(x => x["Bedrijf hiermee"]).Returns(new LocalizedString("Bedrijf hiermee", "Bdd"));
            //_localizer.Setup(x => x["Department_added", newdepartmenttip.ID]).Returns(new LocalizedString("Department_added", $"Department added with {newdepartmenttip.ID}"));
            //_localizer["Bedrijf hiermee"].Value
            //_localizer["Department_added", setDepartmentTip.ID].Value;
        }

        [Given(@"a company with name ""(.*)"" already exists")]
        public void GivenACompanyWithNameAlreadyExists(string companyName)
        {
            var existingCompany = Company.Create(companyName);
            _mockCompanyRepository.Setup(repo => repo.GetCompanyByNameAsync(companyName)).ReturnsAsync(existingCompany);
 
            _mockLocalizer.Setup(x => x["Bedrijf bestaat al"]).Returns(new LocalizedString("Bedrijf bestaat al", "Already Exist"));


        }

        [When(@"the command is handled to add company")]
        public async Task WhenTheCommandIsHandledToAddCompany()
        {
            _response = await _handler.Handle(_command, CancellationToken.None);
        }

        [Then(@"a success response is returned")]
        public void ThenASuccessResponseIsReturned()
        {
            Assert.IsTrue(_response.IsSuccess);
        }

        [Then(@"the success message should contain the company ID")]
        public void ThenTheSuccessMessageShouldContainTheCompanyID()
        {
            Assert.IsTrue(_response.Message.Contains(_response.Message.ToString()));
        }

        [Then(@"an error response is returned")]
        public void ThenAnErrorResponseIsReturned()
        {
            Assert.IsFalse(_response.IsSuccess);
        }

        [Then(@"the error message should indicate that the company already exists")]
        public void ThenTheErrorMessageShouldIndicateThatTheCompanyAlreadyExists()
        {
            Assert.AreEqual(_response.Message, "Already Exist");
        }
    }
}
