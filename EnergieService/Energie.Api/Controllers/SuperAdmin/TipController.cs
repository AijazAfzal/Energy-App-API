using AutoMapper;
using Energie.Business.SuperAdmin.Command;
using Energie.Business.SuperAdmin.Helper;
using Energie.Business.SuperAdmin.Query;
using Energie.Model;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.SuperAdmin
{
    [Route("api/superadmin/[controller]")]
    [ApiController]
    public class TipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public TipController(IMediator mediator,
            IMapper mapper, IClaimService claimService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _claimService = claimService;

        }
        
        [HttpGet("GetEnergySource")]
        public async Task<ActionResult<EnergyAnalysisList>> GetEnergySourceAsync()
        {
            var energySource = await _mediator.Send(new GetEnergySourceQuery
            {
                Language = _claimService.GetLanguage()
            });
            return Ok(energySource);
        }
        [HttpGet("GetEnergyAnalysis/{sourceId:int}")]
        public async Task<ActionResult<ListEnergyAnalysisQuestions>> GetEnergyAnalysisByIdAsync([FromRoute] int sourceId)
        {

            var category = await _mediator.Send(new GetEnergyAnalysisByIdQuery
            {
                Id = sourceId,
                Language = _claimService.GetLanguage()  
            });
            return Ok(category);
        }
        /// <summary>
        /// This method is added for Add EnergyTip by SuperAdmin
        /// And I perform here all operation of create edit delete and update 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        #region personal
        [HttpPost("AddEnergyTipByAsync")]
        public async Task<ActionResult<ResponseMessage>> AddEnergyTipByAsync([FromBody] AddEnergyTipRequest addEnergyTip)
        {
            var energyTipCommand = _mapper.Map<AddTipCommand>(addEnergyTip);
            var response = await _mediator.Send(energyTipCommand);
            return Ok(response);
        }
        [HttpGet("GetEnergyTipListAsync")]
        public async Task<ActionResult<EnergyTipList>> GetEnergyTipListAsync()
        {
            var response = await _mediator.Send(new TipListQuery());
            return Ok(response);
        }
        [HttpGet("GetEnergyTipById/{energyTipID:int}")]
        public async Task<ActionResult<EnergyTip>> GetEnergyTipByIdAsync([FromRoute] int energyTipID)
        {
            var response = await _mediator.Send(new GetTipByIdQuery{id = energyTipID });
            return response;
        }
        [HttpPost("UpdateEnergyTip")]
        public async Task<ActionResult<ResponseMessage>> UpdateEnergyTipAsync([FromBody] UpdateTipRequest updateTip)
        {
            var updateEnergyTipCommand = _mapper.Map<UpdateEnergyTipCommand>(updateTip);
            var response = await _mediator.Send(updateEnergyTipCommand);
            return Ok(response);
        }

        [HttpDelete("DeleteTip/{tipId:int}")]
        public async Task<ActionResult<ResponseMessage>> DeleteTipAsync([FromRoute] int tipId)
        {
            var response = await _mediator.Send(new DeleteEnergyTipCommand { TipId = tipId });
            return Ok(response);

        }

        #endregion
        /// <summary>
        /// this method for department tip added by super Admin 
        /// </summary>
        /// <param name="addDepartmentTip"></param>
        /// <returns></returns>
        /// 

        #region department
        [HttpPost("AddDepartmentTipAsync")]
        public async Task<ActionResult> AddDepartmentTipAsync([FromBody] AddDepartmentTipRequest addDepartmentTip)
        {
            var addDepartmentCommand = _mapper.Map<AddDepartmentTipCommand>(addDepartmentTip);
            var response = await _mediator.Send(addDepartmentCommand);
            return Ok(response);
        }
        [HttpGet("DepartmentTipListAsync")]
        public async Task<ActionResult<DepartmentTipList>> DepartmentTipListAsync()
        {
            var response = await _mediator.Send(new DepartmentTipListQuery());
            return Ok(response);
        }
        [HttpPost("UpdateDepartmentTipAsync")]
        public async Task<ActionResult<ResponseMessage>> UpdateDepartmentTipAsync([FromBody] UpdateDepartmentTipRequest updateDepartmentTip)
        {
            var updateDepartmentCommand = _mapper.Map<UpdateDepartmentTipCommand>(updateDepartmentTip);
            var response = await _mediator.Send(updateDepartmentCommand);
            return Ok(response);

        }
        [HttpDelete("DeleteDepartmentTipAsync/{id:int}")]
        public async Task<ActionResult<ResponseMessage>> DeleteDepartmentTipAsync([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteDepartmentTipCommand
            {
                Id = id
            });
            return Ok(response);
        }
        #endregion
    }
}
