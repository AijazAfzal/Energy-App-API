using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class GetFeedbackByUserQueryHandler : IRequestHandler<GetFeedbackByUserQuery, FeedbackResponse>
    {

        private readonly ILogger<GetFeedbackByUserQueryHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;

        public GetFeedbackByUserQueryHandler(ILogger<GetFeedbackByUserQueryHandler> logger, ICompanyUserRepository companyUserRepository, IFeedbackRepository feedbackRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _feedbackRepository = feedbackRepository;
            _localizer = localizer; 
        }
    
        public async Task<FeedbackResponse> Handle(GetFeedbackByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new FeedbackResponse();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var existfeedback = await _feedbackRepository.GetCompanyUserFeedback(user.Id);

                if (existfeedback != null)
                {
                    response.Rating = existfeedback.Rating;
                    response.description = existfeedback.Description; 
                    response.Message = _localizer["Feedback_exist"].Value; 
                    return response;
                }
                else
                {
                    response.Message = _localizer["Give_feedback"].Value;
                    return response;
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetFeedbackByUserQueryHandler));
                throw;
            }
        }
    }
}
