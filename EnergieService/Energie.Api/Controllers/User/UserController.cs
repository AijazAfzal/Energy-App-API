using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Model;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 


namespace Energie.Api.Controllers.User
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;
        public UserController(IMediator mediator
                            , IMapper mapper
                            , IClaimService claimService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _claimService = claimService;
        }
        [HttpGet("GetDepartmentUserAsync")]
        public async Task<ActionResult<DepartmentUserList>> GetDepartmentUsersAsync()
        {
            var response = await _mediator.Send(new GetDepartmentUsersQuery
            {
                UserEmail = _claimService.GetUserEmail(),
            });
            return response;
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
            //{
            //    return Ok(response); 
            //}
            //else
            //{
            //    return StatusCode(500);
            //}

            return Ok();
        }

        [HttpPost("UpdateCompanyUserLanguage")]
        public async Task<ActionResult<ResponseMessage>> UpdateCompanyUserLanguage([FromBody] UpdateCompanyUserLanguageRequest request)
        {
            var updatecompanyuserlanguagecommand = _mapper.Map<UpdateCompanyUserLanguageCommand>(request);
            updatecompanyuserlanguagecommand.UserEmail = _claimService.GetUserEmail();
            var updatecompanyuserlanguage = await _mediator.Send(updatecompanyuserlanguagecommand); 
            return Ok(updatecompanyuserlanguage);
        }

        [HttpGet("GetLanguageListAsync")]
        public async Task<ActionResult<LanguageList>> GetLanguageListAsync()
        {
            var list = await _mediator.Send(new GetLanguagesListQuery());
            return Ok(list); 


        }

        [HttpGet("GetCompanyUser")]
        public async Task<ActionResult<Model.Response.CompanyUser>> GetCompanyUser()
        {
            var user = await _mediator.Send(new GetCompanyUserQuery
            {
                UserEmail = _claimService.GetUserEmail()
            });

            return Ok(user);

        }

    }
}
