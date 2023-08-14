using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Model;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.User
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    [Route("api/Energie/[controller]")]
    [ApiController]

    public class EnergyPlanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;
        public EnergyPlanController(IMapper mapper
            , IMediator mediator
            , IClaimService claimService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _claimService = claimService;
        }
        [HttpPost("AddEnergyPlanAsync")]
        public async Task<ActionResult<ResponseMessage>> AddEnergyPlanAsync([FromBody] EnergyPlanRequest energyPlan)
        {
            var addEnergyPlanCommand = _mapper.Map<AddEnergyPlanCommand>(energyPlan);
            addEnergyPlanCommand.UserEmail = _claimService.GetUserEmail();
            var response = await _mediator.Send(addEnergyPlanCommand);
            return Ok(response);
        }
        [HttpGet("GetEnergyPlanListAsync")]
        public async Task<ActionResult<EnergyPlanResponseList>> GetEnergyPlanListAsync()
        {
            var response = await _mediator.Send(new GetEnergyPlanListQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage(), 
            });
            return Ok(response);
        }
        [HttpPut("UpdateEnergyPlanAsync")]
        public async Task<ActionResult<ResponseMessage>> UpdateEnergyPlanAsync([FromBody] UpdateEnergyPlanRequest request)
        {
            var updatenergyplancommand = _mapper.Map<UpdateEnergyPlanCommand>(request);
            updatenergyplancommand.UserEmail = _claimService.GetUserEmail();
            var updateenergyplan = await _mediator.Send(updatenergyplancommand);
            return Ok(updateenergyplan);
        }

        [HttpDelete("DeleteEnergyPlanAsync/{energyplanId:int}")]
        public async Task<ActionResult<ResponseMessage>> DeleteEnergyPlanAsync([FromRoute] int energyplanId)
        {
            var response = await _mediator.Send(new RemoveEnergyPlanCommand { EnergyPlanId = energyplanId });
            return Ok(response);

        }

        [HttpPost("AddDepartmentEnergyPlan")]
        public async Task<ActionResult<ResponseMessage>> AddDepartmentEnergyPlan([FromBody] DepartmentEnergyPlanRequest request)
        {
            var departmentenergyplan = _mapper.Map<AddDepartmentEnergyPlanCommand>(request);
            departmentenergyplan.UserEmail = _claimService.GetUserEmail();
            var response = await _mediator.Send(departmentenergyplan);
            return Ok(response);
        }

        [HttpGet("GetDepartmentEnergyPlanListAsync")]
        public async Task<ActionResult<DepartmentEnergyPlanResponseList>> GetDepartmentEnergyPlanListAsync()
        {
            var response = await _mediator.Send(new GetDepartmentEnergyPlanListQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage()
            });
            return Ok(response);
        }
        [HttpPost("UpdateDepartmentEnergyPlanAsync")]
        public async Task<ActionResult<ResponseMessage>> UpdateDepartmentEnergyPlanAsync([FromBody] UpdateDepartmentEnergyPlanRequest planRequest)
        {
            var departmentenergyplan = _mapper.Map<UpdateDepartmentEnergyPlanCommand>(planRequest);
            departmentenergyplan.UserEmail = _claimService.GetUserEmail();
            var response = await _mediator.Send(departmentenergyplan);
            return Ok(response);
        }
        [HttpDelete("DeleteDepartmentEnergyPlanAsync/{id}")]
        public async Task<ActionResult<ResponseMessage>> DeleteDepartmentEnergyPlanAsync([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteDepartmentEnergyCommand
            {
                Id = id,
                UserEmail = _claimService.GetUserEmail()
            });
            return Ok(response);
        }
        //[HttpGet("GetEnergyPlanList")]
        //public async Task<IActionResult> GetEnergyPlanList()
        //{
        //    var response = await _mediator.Send(new GetEnergyPlanListTestQuery());
        //    return (IActionResult)response;
        //}
    }
}
