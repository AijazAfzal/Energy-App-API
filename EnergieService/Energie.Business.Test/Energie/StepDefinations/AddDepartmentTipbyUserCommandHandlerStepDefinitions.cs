using Energie.Business.Energie.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddDepartmentTipbyUserCommandHandlerStepDefinitions
    {
        private readonly Mock<IUserDepartmentTipRepository> _userDepartmentTipRepository;
        private readonly Mock<ILogger<AddDepartmentTipbyUserCommandHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly ScenarioContext _scenariocontext;
        private readonly AddDepartmentTipbyUserCommandHandler _addDepartmentTipbyUserCommandHandler;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;


        public AddDepartmentTipbyUserCommandHandlerStepDefinitions(ScenarioContext scenariocontext)
        {
            _scenariocontext = scenariocontext;
            _userDepartmentTipRepository = new Mock<IUserDepartmentTipRepository>();
            _logger = new Mock<ILogger<AddDepartmentTipbyUserCommandHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _addDepartmentTipbyUserCommandHandler = new AddDepartmentTipbyUserCommandHandler(_userDepartmentTipRepository.Object,_logger.Object,_companyUserRepository.Object, _localizer.Object); 
        }
        [Given(@"the command to add department tip by user")]
        public void GivenTheCommandToAddDepartmentTipByUser()
        {
            var newcompanyuser = new CompanyUser().SetCompanyUser("newuser","abc@gmail.com",1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("abc@gmail.com")).ReturnsAsync(newcompanyuser);
            var newtip = new UserDepartmentTip().SetUserDepartmentTip(1,"new tip added",1); 
            _userDepartmentTipRepository.Setup(x=>x.AddUserDepartmentTipAsync(newtip));  
            _localizer.Setup(x => x["Department_tip_with_DepartmentTipbyUserID_is_added_by_the_user", newtip.Id]).Returns(new LocalizedString("Department_tip_with_DepartmentTipbyUserID_is_added_by_the_user", $"Department tip with {newtip.Id} is added by the user"));
        }

        [When(@"the command is handled to add department tip")]
        public async void WhenTheCommandIsHandledToAddDepartmentTip()
        {
            var result = await _addDepartmentTipbyUserCommandHandler.Handle(new Business.Energie.Command.AddDepartmentTipbyUserCommand 
                               { categoryId=1,
                                 Description= "new tip added",
                                 UserEmail= "abc@gmail.com"  
                               }
            ,CancellationToken.None);
            _scenariocontext.Add("result",result); 

        }

        [Then(@"the department tip is added sucessfully")]
        public void ThenTheDepartmentTipIsAddedSucessfully()
        {
            var test = _scenariocontext.Get<ResponseMessage>("result");
            Assert.True(test.IsSuccess);
            Assert.IsNotNull(test);
            Assert.That(test.Message, Is.EqualTo($"Department tip with {test.Id} is added by the user"));


        }
    }
}
