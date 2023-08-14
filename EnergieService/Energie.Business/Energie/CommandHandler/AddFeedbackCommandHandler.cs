using AutoMapper.Execution;
using Energie.Business.Energie.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddFeedbackCommandHandler : IRequestHandler<AddFeedbackCommand, FeedbackResponse>
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ILogger<AddFeedbackCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;


        public AddFeedbackCommandHandler(IFeedbackRepository feedbackRepository, ILogger<AddFeedbackCommandHandler> logger, ICompanyUserRepository companyUserRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _feedbackRepository = feedbackRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;
        }

        public async Task<FeedbackResponse> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
             {
                var response = new FeedbackResponse();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var existfeedback = await _feedbackRepository.GetCompanyUserFeedback(user.Id);

                if (existfeedback != null)
                {
                    response.Message = _localizer["Feedback_already_exists"].Value;
                    return response;
                }
                else
                {
                    var feedback = new Feedback().SetFeedback(request.Rating,request.description, user.Id); 
                    await _feedbackRepository.AddFeedbackAsync(feedback);
                    response.Rating = feedback.Rating;
                    response.description = feedback.Description;    
                    response.Message = _localizer["Thanks_for_feedback"].Value;
                    return response;

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error occured {{0}}", nameof(AddFeedbackCommandHandler)); 
                throw;
            }
        }
    }
}
