using AutoMapper;
using Energie.Api.Helper;
using Energie.Business.CompanyAdmin.Command;
using Energie.Business.CompanyAdmin.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Model;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.CompanyAdmin
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2C)]
    [Route("api/companyadmin/[controller]")]
    [ApiController]
    public class CompanyHelpController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public CompanyHelpController
            (IMediator mediator
              , IMapper mapper
              , IClaimService claimService
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _claimService = claimService;
        }
        #region for personal 

        [HttpPost("AddCompanyHelpTip")]
        public async Task<ActionResult<ResponseMessage>> AddCompanyHelpAsync([FromBody] AddCompanyHelpRequest addCompanyHelp)
        {
            var companyHelpTipCommand = _mapper.Map<AddCompanyHelpCommand>(addCompanyHelp);
            companyHelpTipCommand.UserEmail = _claimService.GetUserEmail();
            var response = await _mediator.Send(companyHelpTipCommand);
            if (response.IsSuccess == true)
                return Ok(response);

            return StatusCode(500);
        }
        [HttpGet("CompanyHelpTipListAsync")]
        public async Task<ActionResult<CompanyHelpList>> CompanyHelpTipListAsync()
        {
            var companyHelp = await _mediator.Send(new CompanyHelpListQuery
            {
                userEmail = _claimService.GetUserEmail()
            });
            return Ok(companyHelp);

        }
        [HttpGet("CompanyHelpById/{id:int}")]
        public async Task<ActionResult<CompanyHelp>> CompanyHelpByIdAsync([FromRoute] int id)
        {
            var companyHelp = await _mediator.Send(new CompanyHelpByIdQuery
            {
                Id = id,
                UserEmail = _claimService.GetUserEmail()
            });
            return Ok(companyHelp);
        }
        [HttpPost("UpdateCompanyHelp")]
        public async Task<ActionResult<ResponseMessage>> UpdateCompanyHelpAsync([FromBody] UpdateCompanyHelpRequest updateCompanyHelp)
        {
            var companyHelpCommand = _mapper.Map<UpdateCompanyHelpCommand>(updateCompanyHelp);
            companyHelpCommand.UserEmail = _claimService.GetUserEmail();
            var response = await _mediator.Send(companyHelpCommand);
            if (response.IsSuccess == true)
                return Ok(response);

            return StatusCode(500, response);
        }
        [HttpDelete("DeleteCompanyHelpById/{id:int}")]
        public async Task<ActionResult<ResponseMessage>> DeleteCompanyHelpByIdAsync([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteCompanyHelpCommand 
            { 
                Id = id
                , UserEmail = _claimService.GetUserEmail()
            });
            if (response.IsSuccess == true)
                return Ok(response);

            return StatusCode(500, response);

        }

        #endregion

        #region for department 
        [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2C)]
        [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
        [HttpGet("GetHelpCategoryAsync")]
        public async Task<ActionResult<EmployerHelpCategoryList>> GetHelpCategoryAsync()
        {
            var helpCategoryList = await _mediator.Send(new EmployerHelpCategoryListQuery
            {
                Language = _claimService.GetLanguage()
            }); 
            return Ok(helpCategoryList);
        }

        [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2C)]
        [HttpPost("AddDepartmentEmployerHelpAsync")]
        public async Task<ActionResult<ResponseMessage>> AddDepartmentEmployerHelpAsync([FromBody] AddEmployerHelpRequest addEmployerHelp)
        {
            var employerHelpCommand = _mapper.Map<AddEmployerHelpForDeparmentCommand>(addEmployerHelp);
            var response = await _mediator.Send(employerHelpCommand);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2C)]
        [HttpPost("UpdateDepartmentEmployerHelpAsync")]
        public async Task<ActionResult<ResponseMessage>> UpdateDepartmentEmployerHelpAsync([FromBody] UpdateEmployerHelpRequest updateEmployerHelp)
        {
            var mapupdatecommand = _mapper.Map<UpdateEmployerHelpForDeparmentCommand>(updateEmployerHelp);
            var response = await _mediator.Send(mapupdatecommand);
            if (response.IsSuccess == true)
                return Ok(response);

            return StatusCode(500);
        }

        [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2C)]
        [HttpGet("ListDepartmentEmployerHelpAsync")]
        public async Task<ActionResult<DepartmentEmployerHelpList>> ListDepartmentEmployerHelpAsync()
        {
            var departmentEmployerHelpList = await _mediator.Send(new DepartmentEmployerHelpListQuery
            {
                UserEmail = _claimService.GetUserEmail()
            });
            return Ok(departmentEmployerHelpList);
        }
        [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2C)]
        [HttpDelete("DeleteDepartmentEmployerHelpAsync/{Id:int}")]
        public async Task<ActionResult<ResponseMessage>> DeleteDepartmentEmployerHelpAsync([FromRoute] int Id)
        {
            var departmentEmployerHelp = await _mediator.Send(new DeleteDepartmentEmployerHelpCommand
            {
                Id = Id,
                UserEmail= _claimService.GetUserEmail()
            });
            return departmentEmployerHelp;
        }
        #endregion
    }
}
