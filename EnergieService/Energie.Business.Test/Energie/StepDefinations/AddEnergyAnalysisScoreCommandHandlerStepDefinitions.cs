using AutoMapper;
using Azure.Core;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.CommandHandler;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using TechTalk.SpecFlow;
using CompanyUser = Energie.Domain.Domain.CompanyUser;
using EnergyAnalysis = Energie.Domain.Domain.EnergyAnalysis;
using EnergyAnalysisQuestions = Energie.Domain.Domain.EnergyAnalysisQuestions;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddEnergyAnalysisScoreCommandHandlerStepDefinitions
    {

        private readonly Mock<ILogger<AddEnergyAnalysisScoreCommandHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly ScenarioContext _scenarioContext;
        // private readonly AddEnergyAnalysisScoreCommandHandler _addEnergyAnalysisScoreCommandHandler;
        private UserEnergyAnalysisResponseList _response;
        private AddEnergyAnalysisScoreCommandHandler _handler;
        private AddEnergyAnalysisScoreCommand _command;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;

        public AddEnergyAnalysisScoreCommandHandlerStepDefinitions(ScenarioContext sceanriocontext)
        {
            _logger = new Mock<ILogger<AddEnergyAnalysisScoreCommandHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            _scenarioContext = sceanriocontext;
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _handler = new AddEnergyAnalysisScoreCommandHandler(_logger.Object, _companyUserRepository.Object, _energyAnalysisRepository.Object, _localizer.Object);


        }
        [Given(@"the command to add  Energy Analysis Score")]
        public  void GivenTheCommandToAddEnergyAnalysisScore()
        {
            _command = new AddEnergyAnalysisScoreCommand()
            {
                EnergyAnalysisRecord = new int[] { 81 },
                UserEmail = "test@gmail.com"

            };

        }

        [When(@"the command is handled to add  Energy Analysis Score")]
        public async void WhenTheCommandIsHandledToAddEnergyAnalysisScore()
        {
            var user = new CompanyUser().SetCompanyUserForUnitTest(1, "Test", "test@gmail.com", 1, 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(user);


            var energyAnalysis = new EnergyAnalysis().SetEnergyAnalysisTest(1, "Test", DateTime.Now);
           

            var energyAnalysisQuestions = new EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("Test", "test dec", energyAnalysis.Id);
            energyAnalysisQuestions.EnergyAnalysis = energyAnalysis;
            var UserEnergyAnalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(energyAnalysisQuestions.Id, user.Id);
            _energyAnalysisRepository.Setup(x => x.GetUserEnergyAnalysisAsync(user.Id)).ReturnsAsync(UserEnergyAnalysis);
            _energyAnalysisRepository.Setup(x => x.DeleteEnergyAnalysis(UserEnergyAnalysis.Id));
            _energyAnalysisRepository.Setup(x => x.SaveChangesForEnergyAsync());
            _energyAnalysisRepository
            .Setup(x => x.UserEnergyAnalysisAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<UserEnergyAnalysis>
            {
                   new UserEnergyAnalysis
                   {
                       EnergyAnalysisQuestions = energyAnalysisQuestions,
                       CompanyUser = user
                   }


         });


            var userEnergyAnalysisResponse = new UserEnergyAnalysisResponse()
            {
                energyAnalysis = "test",
                EnergyAnalysisQuestionsResponse = new List<EnergyAnalysisQuestion>()
            };

            var EnergyAnalysisQuestion = new EnergyAnalysisQuestion()
            {
                Id = 1,
                IsSelected = true,
                EnergyAnalysis = "test"
            };

            _response = await _handler.Handle(_command, CancellationToken.None);
        }

        [Then(@"the Energy Analysis Score is added sucessfully")]
        public void ThenTheEnergyAnalysisScoreIsAddedSucessfully()
        {
            Assert.NotNull(_response);
            Assert.AreEqual(1, _response.EnergyAnalysisResponse.Count);
        }
    }
}
