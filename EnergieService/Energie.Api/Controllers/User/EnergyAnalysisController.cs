using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.Energie.Command;
using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
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
    public class EnergyAnalysisController : ControllerBase
    {
        private readonly ILogger<EnergyAnalysisController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public EnergyAnalysisController(IMediator mediator, ILogger<EnergyAnalysisController> logger
            , IMapper mapper
            , IClaimService claimService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _claimService = claimService;
        }
        [HttpGet("GetEnergyAnalysis")]
        public async Task<ActionResult<ListEnergyAnalysisQuestions>> GetEnergyAnalysis()
        {
            try
            {
                var energyAnalysis = await _mediator.Send(new GetAllEnergyAnalysisQuery
                {
                    language = _claimService.GetLanguage(),
                    UserEmail = _claimService.GetUserEmail(),
                });
                return Ok(energyAnalysis);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {{0}}", GetEnergyAnalysis, ex.Message);
                throw ex;
            }
        }
        [HttpPost("SetEnergyAnalysisScore")]
        public async Task<ActionResult<UserEnergyAnalysisResponseList>> SetEnergyAnalysisScore([FromBody] AddEnergyAnalysisScoreRequest addEnergyAnalysisScoreRequest)
        {
            try
            {
                var addEnergyAnalysisScoreCommand = _mapper.Map<AddEnergyAnalysisScoreCommand>(addEnergyAnalysisScoreRequest);
                addEnergyAnalysisScoreCommand.UserEmail = _claimService.GetUserEmail();
                var energyAnalysisScore = await _mediator.Send(addEnergyAnalysisScoreCommand);
                return Ok(energyAnalysisScore);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {{0}}", SetEnergyAnalysisScore, ex.Message);
                throw;
            }
        }

        [HttpGet("UserEnergyAnalysis")]
        public async Task<ActionResult<UserEnergyAnalysisRequestList>> UserEnergyAnalysis()
        {
            try
            {
                var userEnergyAnalysis = await _mediator.Send(new UserEnergyAnalysisQuery
                {
                    Language = _claimService.GetLanguage()
                });
                return Ok(userEnergyAnalysis);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {{0}}", UserEnergyAnalysis, ex.Message);
                throw;
            }
        }
        [HttpGet("DepartmentEnergyAnalysis")]
        public async Task<ActionResult<DepartmentEnergyAnalysisRequestList>> DepartmentEnergyAnalysis()
        {
            try
            {
                var departmentEnergyAnalysis = await _mediator.Send(new DepartmentEnergyAnalysisQuery
                {
                    Language = _claimService.GetLanguage(), 
                });
                return Ok(departmentEnergyAnalysis);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {{0}}", UserEnergyAnalysis, ex.Message);
                throw;
            }
        }
    }
}
