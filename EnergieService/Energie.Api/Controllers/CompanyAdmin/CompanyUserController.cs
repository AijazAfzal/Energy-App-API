using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Helper;
using Energie.Model;
using Energie.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Energie.Api.Controllers.CompanyAdmin
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2C)]
    [Route("api/companyadmin/[controller]")]
    [ApiController]
    public class CompanyUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService; 
        public CompanyUserController(IMapper mapper,
            IMediator mediator,
            IClaimService claimService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _claimService = claimService; 
        }
        [HttpPost("AddCompanyUser")]
        public async Task<ActionResult<ResponseMessage>> AddCompanyUserAsync([FromBody] AddCompanyUserRequest addCompanyUser)
        {
            var addCompanyUserCommand = _mapper.Map<AddCompanyUserCommand>(addCompanyUser);
            var response = await _mediator.Send(addCompanyUserCommand);
            return Ok(response);
        }
        [HttpPost("AddMultipleUsersAsync")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddMultipleUsersAsync([FromBody] AddMultipleUserRequestList request)
        {
            return Created("", await _mediator.Send(_mapper.Map<AddMultipleUserCommandList>(request))); 
        }
        [HttpDelete("DeleteCompanyUserById/{userId:int}")]
        public async Task<ActionResult<ResponseMessage>> DeleteCompanyUserById([FromRoute] int userId)
        {
            var response = await _mediator.Send(new DeleteUserCommand 
            { 
                UserId = userId 
            });
            if (response.IsSuccess == true)
                return Ok(response);

            return StatusCode(500);
        }

        //For deleting User Account from Ad and Db

        [HttpDelete("RemoveUserAccount")] 
        public async Task<ActionResult<ResponseMessage>> RemoveUserAccount()
        {
            //var response = await _mediator.Send(new RemoveUserAccountCommand
            //{
            //    UserEmail = _tokenClaimService.GetUserEmail() 
            //});
            //if (response.IsSuccess == true)
            //    return Ok(response);

            //return StatusCode(500); 
            return Ok(); 
        }
    }
}
