using Energie.Business.Energie.Command;
using Energie.Business.Energie.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddLikeTipCommandHandlerStepDefinitions
    {
        private readonly Mock<ILikeTipRepository> _likeTipRepository;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private readonly Mock<ILogger<AddLikeTipCommandHandler>> _logger;
        private AddLikeTipCommandHandler _addLikeTipCommandHandler;
        private AddLikeTipCommand _addLikeTipCommand;
        private ResponseMessage _responseMessage;

        public AddLikeTipCommandHandlerStepDefinitions()
        {
            _likeTipRepository = new Mock<ILikeTipRepository>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _logger = new Mock<ILogger<AddLikeTipCommandHandler>>();
            _addLikeTipCommandHandler = new AddLikeTipCommandHandler(_likeTipRepository.Object, _companyUserRepository.Object, _logger.Object);

        }

        // Add like tip sucessfully
        [Given(@"The command to add like tip")]
        public void GivenTheCommandToAddLikeTip()
        {
            _addLikeTipCommand = new AddLikeTipCommand
            {
                Id = 1,
                UserEmail = "test@test.com"
            };

        }

        [When(@"The command is handled to add like tip")]
        public async void WhenTheCommandIsHandledToAddLikeTip()
        {
           

            var user = new CompanyUser().SetCompanyUser("Satya", "test@test.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@test.com")).ReturnsAsync(user);
            _likeTipRepository.Setup(x => x.GetLikeTipAsync(user.Id, _addLikeTipCommand.Id)).ReturnsAsync((LikeTip)null);

            _likeTipRepository.Setup(x => x.AddLikeTipAsync(It.IsAny<LikeTip>())).Returns(Task.CompletedTask);
            _responseMessage = await _addLikeTipCommandHandler.Handle(_addLikeTipCommand, CancellationToken.None);

        }

        [Then(@"Like tip added sucessfully")]
        public void ThenLikeTipAddedSucessfully()
        {
            Assert.True(_responseMessage.IsSuccess);

        }



        //Remove Like Tip when departmentTip is not null
        [Given(@"The command to check departmentTip is null or not")]
        public void GivenTheCommandToCheckDepartmentTipIsNullOrNot()
        {
            _addLikeTipCommand = new AddLikeTipCommand
            {
                Id = 1,
                UserEmail = "test@test.com"
            };
        }

        [When(@"The command is handled to check departmentTip is null or not")]
        public async void WhenTheCommandIsHandledToCheckDepartmentTipIsNullOrNot()
        {
            var user = new CompanyUser().SetCompanyUser("Satya", "test@test.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@test.com")).ReturnsAsync(user);

            var departmentTip = new LikeTip().SetLikeTip(user.Id, 1);
            _likeTipRepository.Setup(x => x.GetLikeTipAsync(user.Id, _addLikeTipCommand.Id)).ReturnsAsync(departmentTip);

            _likeTipRepository.Setup(x => x.RemoveLikeTipAsync(user.Id, _addLikeTipCommand.Id)).Returns(Task.CompletedTask);
            _likeTipRepository.Setup(x => x.AddLikeTipAsync(It.IsAny<LikeTip>())).Returns(Task.CompletedTask);

            _responseMessage = await _addLikeTipCommandHandler.Handle(_addLikeTipCommand, CancellationToken.None);

        }

        [Then(@"Remove Like Tip sucessfully")]
        public void ThenRemoveLikeTipSucessfully()
        {
            Assert.True(_responseMessage.IsSuccess);
        }

    }
}
