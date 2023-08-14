using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.SuperAdmin.Command;
using Energie.Model;
using Energie.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;

namespace Energie.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = AuthorizationScheme.AzureAD)]
    [Route("api/superadmin/[controller]")]
    [ApiController]
    public class CompanyAdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CompanyAdminController> _logger;
        private readonly IMapper _mapper;
        public CompanyAdminController(IMapper mapper
            , IMediator mediator
            , ILogger<CompanyAdminController> logger)
        {
             _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("AddCompanyAdminAsync")]
        public async Task<ActionResult<ResponseMessage>> AddCompanyAdminAsync([FromBody] AddCompanyAdminRequest addCompanyAdmin)
        {
            try
            {
                var companyAdmin = _mapper.Map<AddCompanyAdminCommand>(addCompanyAdmin);
                var response = await _mediator.Send(companyAdmin);
                return Ok(response);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(AddCompanyAdminAsync));
                throw ex;
            }
        }

    }
}
