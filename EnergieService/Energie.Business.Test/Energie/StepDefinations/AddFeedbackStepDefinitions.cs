using Azure.Core;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.CommandHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model;
using Energie.Model.Response;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Energie.Business.Test.Energie.StepDefinations
{
    [Binding]
    public class AddFeedbackStepDefinitions
    {
        private readonly Mock<IFeedbackRepository> _feedbackRepository;
        private readonly Mock<ILogger<AddFeedbackCommandHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private AddFeedbackCommandHandler _handler;
        private AddFeedbackCommand _command;
        private FeedbackResponse _response;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public AddFeedbackStepDefinitions()
        {
           _feedbackRepository = new Mock<IFeedbackRepository>();  
           _logger = new Mock<ILogger<AddFeedbackCommandHandler>>();
           _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();
            _handler = new AddFeedbackCommandHandler(_feedbackRepository.Object, _logger.Object, _companyUserRepository.Object, _localizer.Object);

        }

        //Feedback added successfully 
        [Given(@"The command to add feedback")]
        public void GivenTheCommandToAddFeedback()
        {
            _command = new AddFeedbackCommand()
            {
                 Rating = 4,
                UserEmail="test@gmail.com"

            };
        }

        [When(@"The command is handled to add feedback successfully")]
        public async void WhenTheCommandIsHandledToAddFeedbackSuccessfully()
        {

            var user = new Domain.Domain.CompanyUser().SetCompanyUser("Satya", "test@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(user);

            var feedback = new Feedback().SetFeedback(_command.Rating,"", user.Id); 
            _feedbackRepository.Setup(x => x.AddFeedbackAsync(feedback));

            _localizer.Setup(x => x["Thanks_for_feedback"]).Returns(new LocalizedString("Thanks_for_feedback", "Thanks for feedback"));

            _response = await _handler.Handle(_command, CancellationToken.None);

        }

        [Then(@"Feedback added successfully")]
        public void ThenFeedbackAddedSuccessfully()
        {
            Assert.NotNull(_response);
            Assert.AreEqual(_command.Rating, _response.Rating);
            Assert.AreEqual("Thanks for feedback", _response.Message);
        }

        //Check Feedback rating is already exist or not
        [Given(@"The command to check the rating exist or not")]
        public void GivenTheCommandToCheckTheRatingExistOrNot()
        {
            _command = new AddFeedbackCommand()
            {
                Rating = 4,
                UserEmail = "test@gmail.com"

            };
        }

        [When(@"The command is handled to check the rating exist or not")]
        public async void WhenTheCommandIsHandledToCheckTheRatingExistOrNot()
        {
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("Satya", "test@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(user);

            var feedback = new Feedback().SetFeedback(_command.Rating,"", user.Id);
            _feedbackRepository.Setup(x => x.AddFeedbackAsync(feedback));
            _feedbackRepository.Setup(x => x.GetCompanyUserFeedback(user.Id)).ReturnsAsync(feedback);
            _localizer.Setup(x => x["Feedback_already_exists"]).Returns(new LocalizedString("Feedback_already_exists", "Feedback for this user already exist"));
            _response = await _handler.Handle(_command, CancellationToken.None);
        }

        [Then(@"If exist then give response message rating is already exist")]
        public void ThenIfExistThenGiveResponseMessageRatingIsAlreadyExist()
        {
           Assert.NotNull( _response);
           Assert.AreEqual("Feedback for this user already exist",_response.Message);
        }


    }
}
