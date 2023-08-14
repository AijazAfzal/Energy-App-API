using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Helper;
using Energie.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.User
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    [Route("api/energie/[controller]")]
    [ApiController]
    public class LikeTipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public LikeTipController(IMediator mediator
            , IMapper mapper
            , IClaimService claimService)
        {
            _mapper = mapper;
            _claimService = claimService;
            _mediator = mediator;
        }
        [HttpGet("LikeTipAsync/{id}")]
        public async Task<ActionResult<ResponseMessage>> LikeTipAsync([FromRoute] int id)
        {
            var response = await _mediator.Send(new AddLikeTipCommand
            {
                Id = id,
                UserEmail = _claimService.GetUserEmail()
            });
            return Ok(response);
        }
    }
}
