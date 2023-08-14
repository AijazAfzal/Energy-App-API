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
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    [Route("api/energie/[controller]")]
    [ApiController]
    public class UserDepartmentTipController : ControllerBase
    {
        private readonly ILogger<UserDepartmentTipController> _logger;
        private readonly IClaimService _claimService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserDepartmentTipController(ILogger<UserDepartmentTipController> logger
            , IClaimService claimService
            , IMediator mediator
            , IMapper mapper)
        {
            _logger = logger;
            _claimService = claimService;
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet("DepartmentTipCategoryListAsync")]
        public async Task<ActionResult<DepartmentCategoryList>> DepartmentTipCategoryListAsync()
        {
            var categoryList = await _mediator.Send(new DepartmentCategoryListQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage(),
            });
            return Ok(categoryList);
        }
        [HttpGet("DepartmentTipForUserListAsync/{id:int}")]
        public async Task<ActionResult<DepartmentEnergyTipList>> DepartmentTipForUserListAsync([FromRoute] int id)
        {
            var userDepartmentTip = await _mediator.Send(new DepartmentEnergyTipQuery
            {
                Id = id,
                UserEmail = _claimService.GetUserEmail(),
                Language= _claimService.GetLanguage(),  
            });
            return Ok(userDepartmentTip);

        }
        [HttpGet("AddDepartmentFavouriteTipAsync/{id:int}")]
        public async Task<ActionResult<ResponseMessage>> AddDepartmentFavouriteTipAsync([FromRoute] int id)
        {
            var addedDepartmentFavouriteTip = await _mediator.Send(new AddDepartmentFavouriteTipCommand
            {
                Id = id,
                UserEmail = _claimService.GetUserEmail()
            });
            return Ok(addedDepartmentFavouriteTip);
        }
        [HttpGet("ListDepartmentFavouriteTipAsync")]
        public async Task<ActionResult<DepartmentEnergyTipsList>> ListDepartmentFavouriteTipAsync() 
        {
            var departmentTipList = await _mediator.Send(new DepartmentFavouriteTipListQuery
            {
                UserEmail = _claimService.GetUserEmail(), 
                Language = _claimService.GetLanguage(), 
            });
            return Ok(departmentTipList);
        }
        [HttpDelete("RemoveDepartmentFavouriteTipAsync")]
        public async Task<ActionResult<ResponseMessage>> RemoveDepartmentFavouriteTipAsync([FromBody] RemoveDepartmentTipRequest request)
        {
            var removedepartmentfav = _mapper.Map<RemoveDepartmentFavouriteTipCommand>(request);
            removedepartmentfav.UserEmail = _claimService.GetUserEmail();
            var response = await _mediator.Send(removedepartmentfav);
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        #region
        [HttpPost("AddUserDepartmentTipAsync")]
        public async Task<ActionResult<ResponseMessage>> AddUserDepartmentTipAsync(AddDepartmentTipbyUserRequest request)
        {
            var addDepartmentTipbyUserCommand = _mapper.Map<AddDepartmentTipbyUserCommand>(request);
            addDepartmentTipbyUserCommand.UserEmail = _claimService.GetUserEmail();
            var departmentTip = await _mediator.Send(addDepartmentTipbyUserCommand);
            return Ok(departmentTip);


        }

        [HttpGet("GetUserDepartmentTipListAsync")]
        public async Task<ActionResult<UserDepartmentTipList>> GetUserDepartmentTipListAsync()
        {

            var list = await _mediator.Send(new GetUserDepartmentTipListQuery
            {
                UserEmail = _claimService.GetUserEmail()
            });
            return Ok(list);

        }

        [HttpDelete("RemoveUserDepartmentTipAsync/{id:int}")]
        public async Task<ActionResult<ResponseMessage>> RemoveUserDepartmentTipAsync([FromRoute] int id)
        {
            var Tiptobedeleted = await _mediator.Send(new DeleteDepartmentTipbyUserCommand
            {
                Id = id,
                UserEmail= _claimService.GetUserEmail()
            });
            return Ok(Tiptobedeleted);
        }

        [HttpPut("UpdateUserDepartmentTipAsync")]
        public async Task<ActionResult<ResponseMessage>> UpdateUserDepartmentTipAsync([FromBody] UpdateDepartementTipbyUser request)
        {
            var updateuserdepartmenttip = _mapper.Map<UpdateDepartmentTipbyUserCommand>(request);
            var value = await _mediator.Send(updateuserdepartmenttip);
            return Ok(value);

        }


        #endregion
    }
}
