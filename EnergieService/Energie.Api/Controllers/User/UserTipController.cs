using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Model;
using Energie.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.User
{
    [Route("api/energie/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    public class UserTipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserTipController> _logger;
        private readonly IClaimService _claimService;
        public UserTipController(ILogger<UserTipController> logger
           , IMapper mapper
           , IMediator mediator
           , IClaimService claimService
           )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
            _claimService = claimService;

        }
        [HttpGet("GetUserEnergyTipList/{Id:int}")]
        public async Task<UserEnergyTipList> GetUserEnergyTipList([FromRoute] int Id)
        {
            try
            {
                var userTip = await _mediator.Send(new GetUserEnergyTipQuery
                {
                    id = Id,
                    UserEmail = _claimService.GetUserEmail(),
                    Language = _claimService.GetLanguage(),
                }); ;
                return userTip;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {{0}}", GetUserEnergyTipList, ex.Message);
                throw ex;
            }
        }
        [HttpGet("AddUserFavouriteTip/{tipId:int}")]
        public async Task<ActionResult<ResponseMessage>> AddUserFavouriteTip([FromRoute] int tipId)
        {
            try
            {
                var addedTip = await _mediator.Send(new AddUserFavouriteTipQuery { tipID = tipId });
                if (addedTip.IsSuccess == true)
                    return Ok(addedTip);

                return BadRequest(addedTip);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {{0}}", AddUserFavouriteTip, ex.Message);
                throw ex;
            }
        }
        [HttpGet("UserFavouriteTip")]
        public async Task<ActionResult<UserFavouriteTipRequestList>> UserFavouriteTip()
        {
            try
            {
                var userFavouriteTip = await _mediator.Send(new UserFavouriteTipQuery
                {
                    UserEmail = _claimService.GetUserEmail(),
                    Language = _claimService.GetLanguage(), 
                }); ;
                return Ok(userFavouriteTip);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {{0}}", UserFavouriteTip, ex.Message);
                throw ex;
            }
        }

        [HttpDelete("RemoveMyFavouriteTip")]
        public async Task<ActionResult<ResponseMessage>> RemoveMyFavouriteTip([FromBody] RemoveUserFavouriteTipRequest removeUserFavouriteTipRequest)
        {
            try
            {
                var removeUserFavouriteTipCommand = _mapper.Map<RemoveUserFavouriteTipCommand>(removeUserFavouriteTipRequest);
                removeUserFavouriteTipCommand.UserEmail = _claimService.GetUserEmail();
                var response = await _mediator.Send(removeUserFavouriteTipCommand);
                return Ok(response);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("AddTipByUser")]
        public async Task<ActionResult<ResponseMessage>> AddTipByUser([FromBody] AddTipByUserRequest addTipByUserRequest)
        {
            try
            {
                var addTipByUserCommand = _mapper.Map<AddTipByUserCommand>(addTipByUserRequest);
                addTipByUserCommand.UserEmail = _claimService.GetUserEmail();
                var userTip = await _mediator.Send(addTipByUserCommand);
                if (userTip.IsSuccess == true)
                    return Ok(userTip);

                return StatusCode(409, userTip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(AddTipByUser));
                throw ex;
            }
        }
        [HttpPost("UpdateTipByUser")]
        public async Task<ActionResult<ResponseMessage>> UpdateTipByUser([FromBody] UpdateTipByUserRequest updateTipByUserRequest)
        {
            try
            {
                var updateTipByUserCommand = _mapper.Map<UpdateTipByUserCommand>(updateTipByUserRequest);
                var updatedUserTip = await _mediator.Send(updateTipByUserCommand);
                if (updatedUserTip.IsSuccess == true)
                    return Ok(updatedUserTip);

                return StatusCode(409, updatedUserTip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(UpdateTipByUser));
                throw ex;
            }
        }
    }
}
