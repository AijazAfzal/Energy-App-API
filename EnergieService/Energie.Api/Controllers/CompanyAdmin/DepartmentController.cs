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
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public DepartmentController(ILogger<DepartmentController> logger, IMediator mediator
            , IMapper mapper
            , IClaimService claimService)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _claimService = claimService;
        }
        [HttpGet("GetDepartmentList")]
        public async Task<ActionResult<DepartmentList>> GetDepartmentList()
        {
            try
            {
                var department = await _mediator.Send(new GetDepartmentListQuery 
                { 
                    UserEmail = _claimService.GetUserEmail() 
                });
                var departmentList = _mapper.Map<List<Model.Request.Department>>(department);
                return new DepartmentList
                {
                    Departments = departmentList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {{0}}", GetDepartmentList, ex.Message);
                return NotFound();
            }
        }
        [HttpPost("AddDepartment")]
        public async Task<ActionResult<ResponseMessage>> AddDepartment([FromBody] AddDepartmentRequest addDepartmentRequest)
        {

            try
            {
                var addDepartmentCommand = _mapper.Map<AddDepartmentCommand>(addDepartmentRequest);
                addDepartmentCommand.UserEmail = _claimService.GetUserEmail();
                var department = await _mediator.Send(addDepartmentCommand);
                if (department.IsSuccess == true)
                    return Ok(department);

                return StatusCode(409, department);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {{0}}", AddDepartment, ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("GetDepartmentUserByDepartmentId/{departmentId:int}")]
        public async Task<ActionResult<DepartmentUserList>> GetDepartmentUserByDepartmentId([FromRoute] int departmentId)
        {
            try
            {
                var departmentUsers = await _mediator.Send(new GetDepartmentUserCommand { DepartmentId = departmentId });
                var departmentuserList = _mapper.Map<List<DepartmentUser>>(departmentUsers);
                return new DepartmentUserList
                {
                    DepartmentUsers = departmentuserList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {GetDepartmentUserByDepartmentId}", ex.Message);
                return BadRequest("Not found");
            }
        }
    }
}
