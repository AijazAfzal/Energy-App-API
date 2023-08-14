using AutoMapper;
using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Helper;
using Energie.Model.Request;
using Energie.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Energie.Model.Response;
using Energie.Api.Helper;
using Microsoft.AspNetCore.Authorization;
using Energie.Business.Energie.Query;
using Azure.Core;

namespace Energie.Api.Controllers.User
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(IMediator mediator, IMapper mapper, IClaimService claimService, ILogger<FeedbackController> logger)
        {
             _mediator = mediator;
             _mapper = mapper;
            _claimService = claimService;
             _logger = logger;   
        }

        [HttpPost("AddFeedback")]
        public async Task<ActionResult<FeedbackResponse>> AddFeedback([FromBody] AddFeedbackRequest addFeedbackRequest)
        {
           
                var feedbackByUserCommand = _mapper.Map<AddFeedbackCommand>(addFeedbackRequest);
                feedbackByUserCommand.UserEmail = _claimService.GetUserEmail();
                var AddFeedback = await _mediator.Send(feedbackByUserCommand);

                return Ok(AddFeedback); 
            
        }

        [HttpGet("GetFeedbackForUser")]
        public async Task<ActionResult<FeedbackResponse>> GetFeedbackForUser()
        {

            var userTip = await _mediator.Send(new GetFeedbackByUserQuery
            {
                UserEmail = _claimService.GetUserEmail()
            }); ;
            return userTip; 

        }

        [HttpPut("UpdateUserFeedback")]
        public async Task<ActionResult<ResponseMessage>> UpdateUserFeedback([FromBody] UpdateUserFeedbackRequest request)
        {
            var updateuserfeedbackcommand = _mapper.Map<UpdateUserFeedbackCommand>(request); 
            updateuserfeedbackcommand.UserEmail = _claimService.GetUserEmail();
            var updateduserfeedback = await _mediator.Send(updateuserfeedbackcommand);
            return Ok(updateduserfeedback); 

        }
    }
}
