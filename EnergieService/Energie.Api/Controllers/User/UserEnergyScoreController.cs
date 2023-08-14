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
    [Route("api/energie/[controller]")]
    [ApiController]
    public class UserEnergyScoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public UserEnergyScoreController(IMediator mediator
            , IMapper mapper

            , IClaimService claimService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _claimService = claimService;
        }
        #region for Personal
        [HttpPost("AddEnergyScore")]
        public async Task<ActionResult<ResponseMessage>> AddEnergyScore([FromBody] AddMonthlyEnergyRequest addMonthlyEnergyRequest)
        {
            var addMonthlyEnergyScoreCommand = _mapper.Map<AddMonthlyEnergyScoreCommand>(addMonthlyEnergyRequest);
            var energyScore = await _mediator.Send(addMonthlyEnergyScoreCommand);
            if (energyScore.IsSuccess == true)
                return Ok(energyScore);

            return StatusCode(409, energyScore);
        }
        //unit test complete 
        [HttpGet("GetEnergyScoreForUser")]
        public async Task<ActionResult<ListEnergyScore>> GetEnergyScoreForUser()
        {
            var energyscore = await _mediator.Send(new UserEnergyScoreQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage(),
            }); ;
            return Ok(energyscore);

        }
        //unit test complete 
        [HttpGet("GetUserEnergyAverage")]
        public async Task<ActionResult<EnergyScoreAverage>> GetUserEnergyAverage()
        {
            var energieaverage = await _mediator.Send(new UserEnergyScoreAverageQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage()
                
            });
            return Ok(energieaverage);


        }
        //unit test complete 
        [HttpGet("GetTrendScoreForUser")]
        public async Task<ActionResult<TrendScoreResponse>> GetTrendScoreForUser()
        {
            var trendScore = await _mediator.Send(new UserTrendScoreQuery
            {
                UserEmail = _claimService.GetUserEmail()
            });
            return Ok(trendScore);
        }
        #endregion

        #region for department
        [HttpGet("GetDepartmentMonthlyScore")]
        public async Task<ActionResult<DepartmentMonthlyScore>> GetDepartmentMonthlyScoreAsync()
        {  
            var score = await _mediator.Send(new DepartmentMonthlyScoreQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage()  
                
            });
            return Ok(score);
        }

        [HttpGet("GetDepartmentAverageForUser")]
        public async Task<ActionResult<DepartmentScoreAverage>> GetDepartmentAverageForUserAsync()
        {
            var averageScore = await _mediator.Send(new DepartmentAverageForUserQuery
            {
                Language = _claimService.GetLanguage()  
            });
            return Ok(averageScore);
        }

        [HttpGet("GetDepartmentTrendScore")]
        public async Task<ActionResult<TrendScoreResponse>> GetDepartmentTrendScore()
        {
            var departmentTrendScore = await _mediator.Send(new DepartmentTrendScoreQuery
            {
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage()  
            });
            return Ok(departmentTrendScore);
        }
        #endregion


    }
}
