using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.Query.NotificationFlag;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain;
using Energie.Model;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.User
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    [Route("api/energie/[controller]")]
    [ApiController]
    public class FlagController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FlagController> _logger;
        public readonly IClaimService _claimService;
        public FlagController(IMediator mediator
            , ILogger<FlagController> logger
            , IClaimService claimService)
        {
            _mediator = mediator;
            _logger = logger;
            _claimService = claimService;
        }
        [HttpGet("ShowOnboarding")]
        public async Task<ActionResult<ShowOnboardingResponse>> ShowOnboarding()
        {
            try
            {
                var response = await _mediator.Send(new ShowOnboardingFlagQuery
                {
                    UserEmail = _claimService.GetUserEmail(),
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", ShowOnboarding);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetFlagForMonthlyPopUp")]
        public async Task<ActionResult<ResponseForMonthlyPopup>> GetFlagForMonthlyPopUp()
        {
            try
            {
                var response = await _mediator.Send(new GetFlagForMonthlyPopUpQuery
                {
                    UserEmail = _claimService.GetUserEmail(),
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {{0}}", ShowOnboarding, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ShowAnalysisFlagRequest")]
        public async Task<ActionResult<ShowAnalysisFlagResponse>> ShowAnalysisFlagRequest()
        {
            try
            {
                var response = await _mediator.Send(new ShowAnalysisFlagQuery
                {
                    UserEmail = _claimService.GetUserEmail(),
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {{0}}", ShowOnboarding, ex.Message); 
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("GetNotificationListAsync")]
        public async Task<ActionResult<NotificationResponseList>> GetNotificationListAsync()
        {
            var response = await _mediator.Send(new NotificationResponseQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage(),
            });
            return Ok(response);
        }

        [HttpPost("EnableNotificationAsync/{Id:int}")]
        public async Task<ActionResult<NotificationResponseList>> EnableNotificationAsync([FromRoute] int Id)
        {
            var response = await _mediator.Send(new UserNotificationCommand
            {
                Id = Id,
                UserEmail = _claimService.GetUserEmail(),
            });
            return Ok(response);
        }
  
        [HttpGet("GetAllUserNotifications")]
        public async Task<ActionResult<NotificationMessageList>> GetAllUserNotifications()
        {
            var usernotificationlist = await _mediator.Send(new GetAllUserNotificationQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage(), 

            });
            return Ok(usernotificationlist); 
                
            
        }


    }
}
