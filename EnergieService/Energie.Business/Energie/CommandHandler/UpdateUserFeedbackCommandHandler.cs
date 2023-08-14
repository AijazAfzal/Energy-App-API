using Energie.Business.Energie.Command;
using Energie.Domain.ApplicationEnum;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model;
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
    public class UpdateUserFeedbackCommandHandler : IRequestHandler<UpdateUserFeedbackCommand, ResponseMessage>
    {
        readonly ILogger<UpdateEnergyPlanCommandHandler> _logger;
        readonly IFeedbackRepository _feedbackRepository;
        readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateUserFeedbackCommandHandler(ILogger<UpdateEnergyPlanCommandHandler> logger
            ,IFeedbackRepository feedbackRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _feedbackRepository = feedbackRepository; 
            _localizer = localizer; 
            
        }
        public async Task<ResponseMessage> Handle(UpdateUserFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responsemessage = new ResponseMessage();
                var userfeedback = await _feedbackRepository.GetFeedbackByUserEmailAsync(request.UserEmail);  
                if (userfeedback != null)
                {
                    var updatefeedback = userfeedback.UpdateFeedback(request.Rating, request.description);
                    await _feedbackRepository.UpdateUserFeedbackAsync(updatefeedback);
                    responsemessage.Message = _localizer["Feedback_Update"].Value; 
                    responsemessage.IsSuccess = true;
                    responsemessage.Id = request.Id;
                    return responsemessage;

                }
                else
                {
                    return default; 
                }
            } 

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(UpdateUserFeedbackCommandHandler));
                throw;
            }
        }
    }
}
