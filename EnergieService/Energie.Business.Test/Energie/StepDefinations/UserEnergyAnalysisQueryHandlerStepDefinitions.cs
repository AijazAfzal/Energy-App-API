using AutoMapper;
using Energie.Api;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model.Request;
using Energie.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Reflection.Metadata;
using TechTalk.SpecFlow;
using Company = Energie.Domain.Domain.Company;
using CompanyUser = Energie.Domain.Domain.CompanyUser;
using Department = Energie.Domain.Domain.Department;
using EnergyAnalysis = Energie.Domain.Domain.EnergyAnalysis;

//using EnergyAnalysisQuestions = Energie.Domain.Domain.EnergyAnalysisQuestions;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class UserEnergyAnalysisQueryHandlerStepDefinitions
    {
        private readonly Mock<ILogger<UserEnergyAnalysisQueryHandler>> _logger;
        private readonly Mock<IClaimService> _tokenClaimService;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        private readonly UserEnergyAnalysisQueryHandler _userEnergyAnalysisQueryHandler;
        private UserEnergyAnalysisRequestList _result;
        private readonly Mock<ITranslationsRepository<Domain.Domain.UserEnergyAnalysis>> _translationService;

        public UserEnergyAnalysisQueryHandlerStepDefinitions()
        {
            _logger = new Mock<ILogger<UserEnergyAnalysisQueryHandler>>();
            _tokenClaimService = new Mock<IClaimService>();
            _energyAnalysisRepository = new Mock<IEnergyAnalysisRepository>();
            _translationService = new Mock<ITranslationsRepository<Domain.Domain.UserEnergyAnalysis>>();

            _userEnergyAnalysisQueryHandler = new UserEnergyAnalysisQueryHandler(_logger.Object, _tokenClaimService.Object, _energyAnalysisRepository.Object, _translationService.Object);
        }

        //User Energy Analysis list retrieved sucessfully
        [Given(@"the command to get user enrgyanalysis list")]
        public void GivenTheCommandToGetUserEnrgyanalysisList()
        {
            var company = Company.Create("test company");
            var department = new Department().SetCompanyDepartment("test department", company.Id);
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("testuser", "test@gmail.com", department.Id);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energy = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("test", "test description", energyanalysis.Id);
            energy.EnergyAnalysis = energyanalysis;
            var userfavouriteHelp = new Domain.Domain.UserFavouriteHelp().SetUserFavouriteHelp(0, user.Id);
             userfavouriteHelp.CompanyUser = user;
            _energyAnalysisRepository.Setup(x => x.GetAllEnergyAnalysisQuestions()).ReturnsAsync(new List<Domain.Domain.EnergyAnalysisQuestions>() { energy });
            var userenergyanalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(energyanalysis.Id, user.Id);
            userenergyanalysis.CompanyUser = user;
            userenergyanalysis.EnergyAnalysisQuestions = energy;

            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("test@gmail.com");
            var userasnalysis = _energyAnalysisRepository.Setup(x => x.UserEnergyAnalysisAsync("test@gmail.com")).ReturnsAsync(new List<UserEnergyAnalysis>() { userenergyanalysis });

        }

        [When(@"the command is handled to gett the listt")]
        public async void WhenTheCommandIsHandledToGettTheListt()
        {
           var query = new UserEnergyAnalysisQuery();
           _result = await _userEnergyAnalysisQueryHandler.Handle(query, CancellationToken.None);

        }

        [Then(@"the list is retrieved sucessfully")]
        public void ThenTheListIsRetrievedSucessfully()
        {
            Assert.NotNull(_result);
        }


        //Translated User Energy Analysis list retrieved sucessfully
        [Given(@"The command to get user translated enrgyanalysis list")]
        public void GivenTheCommandToGetUserTranslatedEnrgyanalysisList()
        {
            var company = Company.Create("test company");
            var department = new Department().SetCompanyDepartment("test department", company.Id);
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("testuser", "test@gmail.com", department.Id);
            var energyanalysis = new Domain.Domain.EnergyAnalysis().SetEnergyAnalysis("newanalysis", DateTime.Now);
            var energy = new Domain.Domain.EnergyAnalysisQuestions().SetEnergyAnalysisQuestions("test", "test description", energyanalysis.Id);
            energy.EnergyAnalysis = energyanalysis;
            var userfavouriteHelp = new Domain.Domain.UserFavouriteHelp().SetUserFavouriteHelp(0, user.Id);
            userfavouriteHelp.CompanyUser = user;
            _energyAnalysisRepository.Setup(x => x.GetAllEnergyAnalysisQuestions()).ReturnsAsync(new List<Domain.Domain.EnergyAnalysisQuestions>() { energy });
            var userenergyanalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(energyanalysis.Id, user.Id);
            userenergyanalysis.CompanyUser = user;
            userenergyanalysis.EnergyAnalysisQuestions = energy;

            _tokenClaimService.Setup(x => x.GetUserEmail()).Returns("test@gmail.com");
            var userasnalysis = _energyAnalysisRepository.Setup(x => x.UserEnergyAnalysisAsync("test@gmail.com")).ReturnsAsync(new List<UserEnergyAnalysis>() { userenergyanalysis });


            var translatedData = new List<Domain.Domain.UserEnergyAnalysis>()
            {
                new Domain.Domain.UserEnergyAnalysis().SetUserEnergyAnalysis(energyanalysis.Id, user.Id)
        };
            _translationService.Setup(x => x.GetTranslatedDataAsync<Domain.Domain.UserEnergyAnalysis>("en-US")).ReturnsAsync(translatedData);
        }

        [When(@"The command is handled to get the translated list")]
        public async void WhenTheCommandIsHandledToGetTheTranslatedList()
        {
            var query = new UserEnergyAnalysisQuery { Language = "en-US"};
            _result = await _userEnergyAnalysisQueryHandler.Handle(query, CancellationToken.None);
        }

        [Then(@"The translated list is retrieved sucessfully")]
        public void ThenTheTranslatedListIsRetrievedSucessfully()
        {
            Assert.NotNull(_result);
        }

    }
}
