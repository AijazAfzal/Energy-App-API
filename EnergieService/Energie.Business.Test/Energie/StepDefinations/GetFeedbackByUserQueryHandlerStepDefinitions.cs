using Energie.Business.Energie.Command;
using Energie.Business.Energie.CommandHandler;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
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
    public class GetFeedbackByUserQueryHandlerStepDefinitions
    {
        private readonly Mock<IFeedbackRepository> _feedbackRepository;
        private readonly Mock<ILogger<GetFeedbackByUserQueryHandler>> _logger;
        private readonly Mock<ICompanyUserRepository> _companyUserRepository;
        private GetFeedbackByUserQueryHandler _handler;
        private GetFeedbackByUserQuery _command;
        private FeedbackResponse _response;
        private readonly Mock<IStringLocalizer<Resources.Resources>> _localizer;
        public GetFeedbackByUserQueryHandlerStepDefinitions()
        {
            _feedbackRepository = new Mock<IFeedbackRepository>();
            _logger = new Mock<ILogger<GetFeedbackByUserQueryHandler>>();
            _companyUserRepository = new Mock<ICompanyUserRepository>();
            _localizer = new Mock<IStringLocalizer<Resources.Resources>>();

            _handler = new GetFeedbackByUserQueryHandler(_logger.Object,_companyUserRepository.Object,_feedbackRepository.Object, _localizer.Object);

        }


        [Given(@"The query  to get user rating")]
        public void GivenTheQueryToGetUserRating()
        {
            _command = new GetFeedbackByUserQuery()
            {
                Rating = 4,
                UserEmail = "test@gmail.com"

            };
        }

        [When(@"The query handler to handel get rating by user")]
        public async void WhenTheQueryHandlerToHandelGetRatingByUser()
        {
            var user = new Domain.Domain.CompanyUser().SetCompanyUser("Satya", "test@gmail.com", 1);
            _companyUserRepository.Setup(x => x.GetCompanyUserAsync("test@gmail.com")).ReturnsAsync(user);

            var feedback = new Feedback().SetFeedback(_command.Rating,"", user.Id);
            _feedbackRepository.Setup(x => x.GetCompanyUserFeedback(user.Id)).ReturnsAsync(feedback);
            _localizer.Setup(x => x["Feedback_exist"]).Returns(new LocalizedString("Feedback_exist", "You already give your feedback"));
            _response = await _handler.Handle(_command, CancellationToken.None);
        }

        [Then(@"The rating given by user get successfully")]
        public void ThenTheRatingGivenByUserGetSuccessfully()
        {
            Assert.NotNull(_response);
            Assert.AreEqual(_command.Rating, _response.Rating); 
            Assert.AreEqual("You already give your feedback", _response.Message);
        }
    }
}
