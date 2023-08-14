using Energie.Business.Energie.Command;
using Energie.Business.Energie.CommandHandler;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddMonthlyEnergyScoreCommandHandlerStepDefinitions
    {
        private readonly Mock<IClaimService> _tokenClaimService;
        public readonly Mock<ILogger<AddMonthlyEnergyScoreCommandHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<IEnergyScoreRepository> _energyScoreRepository;
        private AddMonthlyEnergyScoreCommandHandler _handler;
        private AddMonthlyEnergyScoreCommand _command;
        private ResponseMessage _response;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public AddMonthlyEnergyScoreCommandHandlerStepDefinitions()
        {
            _tokenClaimService = new Mock<IClaimService>();
            _logger = new Mock<ILogger<AddMonthlyEnergyScoreCommandHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _energyScoreRepository = new Mock<IEnergyScoreRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();



            _handler = new AddMonthlyEnergyScoreCommandHandler(
               _tokenClaimService.Object,
               _logger.Object,
               _companyUserRepository.Object,
               _energyScoreRepository.Object,
               _localizer.Object
           );
        }


        //The AddMonthlyEnergyScore when existEnergyScore is not null
        [Given(@"the command to add energy score")]
        public void GivenTheCommandToAddEnergyScore()
        {
            _command = new AddMonthlyEnergyScoreCommand { EnergyScore = 1 };
        }

        [When(@"the command is handled to add energy score")]
        public async void WhenTheCommandIsHandledToAddEnergyScore()
        {
            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("user@example.com");

            var user = new CompanyUser().SetCompanyUser("Satya", "user@example.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);
            _energyScoreRepository.Setup(x => x.GetCompanyUserEnergyScore(user.Id)).ReturnsAsync((EnergyScore)null);
            _energyScoreRepository.Setup(x => x.AddEnergyScoreAsync(It.IsAny<EnergyScore>())).ReturnsAsync(new EnergyScore());
            _localizer.Setup(x => x["Score_added"]).Returns(new LocalizedString("Score_added", "Score added"));
            _response = await _handler.Handle(_command, CancellationToken.None);


        }
        [Then(@"the energy score is added sucessfully")]
        public void ThenTheEnergyScoreIsAddedSucessfully()
        {
            Assert.NotNull(_response);
            Assert.True(_response.IsSuccess);
            Assert.AreEqual("Score added", _response.Message);

        }

        //The AddMonthlyEnergyScore when existEnergyScore is not null

        [Given(@"The AddMonthlyEnergyScoreCommand to add energy when existEnergyScore is not null")]
        public void GivenTheAddMonthlyEnergyScoreCommandToAddEnergyWhenExistEnergyScoreIsNotNull()
        {
            _command = new AddMonthlyEnergyScoreCommand { EnergyScore = 1 };

        }

        [When(@"The AddMonthlyEnergyScoreCommand is handled to add energy score when existEnergyScore is not null")]
        public async void WhenTheAddMonthlyEnergyScoreCommandIsHandledToAddEnergyScoreWhenExistEnergyScoreIsNotNull()
        {           

            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("user@example.com");

            var user = new CompanyUser().SetCompanyUser("Satya", "user@example.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);


            var existingEnergyScore = new EnergyScore().SetEnergyScoreForList(_command.EnergyScore, 1, 2002, user.Id);
            _energyScoreRepository.Setup(x => x.GetCompanyUserEnergyScore(user.Id)).ReturnsAsync(existingEnergyScore);
            _localizer.Setup(x => x["Error_occurred"]).Returns(new LocalizedString("Error_occurred", "Error occurred"));

            _response = await _handler.Handle(_command, CancellationToken.None);

        }

        [Then(@"The MonthlyEnergyScore is added sucessfully when existEnergyScore is not null")]
        public void ThenTheMonthlyEnergyScoreIsAddedSucessfullyWhenExistEnergyScoreIsNotNull()
        {
            Assert.NotNull(_response);
            Assert.AreEqual("Error occurred", _response.Message);
        }


        //Check Energy score already exist or Not for the month
        [Given(@"The AddMonthlyEnergyScoreCommand to Check Energy score already exist or Not for the month")]
        public void GivenTheAddMonthlyEnergyScoreCommandToCheckEnergyScoreAlreadyExistOrNotForTheMonth()
        {
            _command = new AddMonthlyEnergyScoreCommand { EnergyScore = 1 };
        }

        [When(@"The AddMonthlyEnergyScoreCommand is handled to Check Energy score already exist or Not")]
        public async void WhenTheAddMonthlyEnergyScoreCommandIsHandledToCheckEnergyScoreAlreadyExistOrNot()
        {
            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("user@example.com");

            var user = new CompanyUser().SetCompanyUser("Satya", "user@example.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);


            var existingEnergyScore = new EnergyScore().SetEnergyScore(5, user.Id);
            _energyScoreRepository.Setup(x => x.GetCompanyUserEnergyScore(user.Id)).ReturnsAsync(existingEnergyScore);

            _localizer.Setup(x => x["Score_exists_for_this_month"]).Returns(new LocalizedString("Score_exists_for_this_month", "Score exists for this month"));

            _response = await _handler.Handle(_command, CancellationToken.None);
        }

        [Then(@"If exist return Energy score already exist or Not for the month")]
        public void ThenIfExistReturnEnergyScoreAlreadyExistOrNotForTheMonth()
        {
            Assert.NotNull(_response);
            Assert.False(_response.IsSuccess);
            Assert.AreEqual("Score exists for this month", _response.Message);
        }


        //When something wrong with the input

        [Given(@"The AddMonthlyEnergyScoreCommand input to Check everything is correct or not")]
        public void GivenTheAddMonthlyEnergyScoreCommandInputToCheckEverythingIsCorrectOrNot()
        {
            _command = new AddMonthlyEnergyScoreCommand { EnergyScore = 8 };


        }

        [When(@"The AddMonthlyEnergyScoreCommand is handled to Check given input is correct or not")]
        public async void WhenTheAddMonthlyEnergyScoreCommandIsHandledToCheckGivenInputIsCorrectOrNot()
        {
            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("user@example.com");

            var user = new CompanyUser().SetCompanyUser("Satya", "user@example.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("user@example.com")).ReturnsAsync(user);

            var energyscor = new EnergyScore().SetEnergyScore(8, user.Id);
            _energyScoreRepository.Setup(x => x.AddEnergyScoreAsync(energyscor));
            ;
           

            _response = await _handler.Handle(_command, CancellationToken.None);

        }

        [Then(@"If something is wrong with the input then return message Something went wrong")]
        public void ThenIfSomethingIsWrongWithTheInputThenReturnMessageSomethingWentWrong()
        {
           // Assert.AreEqual("Error occurred", _response.Message);
        }

    }

}


