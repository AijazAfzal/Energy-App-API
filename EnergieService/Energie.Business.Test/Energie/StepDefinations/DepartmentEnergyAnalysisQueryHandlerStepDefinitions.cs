using Energie.Business.Energie.CommandHandler;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using TechTalk.SpecFlow;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class DepartmentEnergyAnalysisQueryHandlerStepDefinitions
    {
        private readonly Mock<IClaimService> _tokenClaimService;
        private readonly Mock<ILogger<DepartmentAverageForUserQueryHandler>> _logger;
        private readonly Mock<IEnergyAnalysisRepository> _energyAnalysisRepository;
        //   private readonly Mock<IDepartmentEnergyScoreRepository> _departmentEnergyScoreRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private DepartmentScoreAverage _departmentScoreAverage;
        private DepartmentEnergyAnalysisQueryHandler _handler;
        private DepartmentEnergyAnalysisQuery _command;

        private readonly ScenarioContext _scenarioContext;

        public DepartmentEnergyAnalysisQueryHandlerStepDefinitions(ScenarioContext scenarioContext)
        {
           
        }


        [Given(@"Department EnergyAnalysis Query to retrive DepartmentEnergyAnalysis")]
        public void GivenDepartmentEnergyAnalysisQueryToRetriveDepartmentEnergyAnalysis()
        {
            

        }

        [When(@"Department EnergyAnalysis Query is handled to retrive DepartmentEnergyAnalysis")]
        public async void WhenDepartmentEnergyAnalysisQueryIsHandledToRetriveDepartmentEnergyAnalysis()
        {

        }

        [Then(@"Department EnergyAnalysis retrive successfully")]
        public void ThenDepartmentEnergyAnalysisRetriveSuccessfully()
        {
           
        }
    }
}
