using AutoMapper;
using Energie.Business.SuperAdmin.MediatR.Commands.Company;
using Energie.Business.SuperAdmin.MediatR.Queries.Company;
using Energie.Model;
using Energie.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.SuperAdmin
{
    [Route("api/superadmin/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyController> _logger;
        public CompanyController(IMediator mediator
                                , IMapper mapper
                                , ILogger<CompanyController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("CreateCompanyAsync")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyRequest request)
        {
            try
            {
                return Created("", await _mediator.Send(_mapper.Map<CreateCompanyCommand>(request)));
            }
            catch (Exception)
            {
                _logger.LogError($"Error occured in {{0}}", nameof(CreateCompanyAsync));
                throw;
            }

        }

        [HttpGet("GetCompanyListAsync")]
        [ProducesResponseType(typeof(CompanyList), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyListAsync()
        {
            try
            {
                return Ok(await _mediator.Send(new CompaniesQuery()));
            }
            catch (Exception)
            {
                _logger.LogError($"Error occured in {{0}}", nameof(GetCompanyListAsync));
                throw;
            }

        }



    }
}
