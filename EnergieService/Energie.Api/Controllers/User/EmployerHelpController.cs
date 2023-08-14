
using AutoMapper;
using Energie.Api.Helper;
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
    public class EmployerHelpController : ControllerBase
    {
        private readonly ILogger<EmployerHelpController> _logger;
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        public EmployerHelpController(ILogger<EmployerHelpController> logger
            , IMediator mediator
            , IMapper mapper
            , IClaimService claimService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _claimService = claimService;
        }

        [HttpGet("CompanyCategoryList")]
        public async Task<ActionResult<CompanyCategoryUserList>> CompanyCategoryList()
        {
            try
            {
                var companyCategory = await _mediator.Send(new CompanyCategoryListQuery
                {
                    UserEmail = _claimService.GetUserEmail()
                });
                var list = companyCategory
                    .Select(x => new CompanyCategory
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    }).ToList();
                return Ok(new CompanyCategoryUserList { CompanyCategories = list });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Occured in {{0}}");
                return BadRequest();
            }
        }

        [HttpGet("GetCompanyUserEnergyTipList/{companyHelpId:int}")]
        public async Task<ActionResult<CompanyUserEnergyTipList>> GetCompanyUserEnergyTipList([FromRoute] int companyHelpId)
        {
            try
            {
                var userCompanyUserEnergyTip = await _mediator.Send(new CompanyUserEnergyTipQuery
                {
                    companyCategoryId = companyHelpId,
                    UserEmail = _claimService.GetUserEmail(),
                    Language = _claimService.GetLanguage()
                });
                return Ok(userCompanyUserEnergyTip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(GetCompanyUserEnergyTipList));
                throw ex;
            }
        }

        [HttpGet("SetCompanyUserFavouriteTip/{companyHelpTipId:int}")]
        public async Task<ActionResult<ResponseMessage>> SetCompanyUserFavouriteTip([FromRoute] int companyHelpTipId)
        {
            try
            {
               // var responseMessage = new ResponseMessage();
                var setuserFavouriteTip = await _mediator
                                        .Send(new AddCompanyHelpUserFavouriteTipQuery
                                        {
                                            Id = companyHelpTipId,
                                            UserEmail = _claimService.GetUserEmail(),
                                        });

                return Ok(setuserFavouriteTip);
                //if (setuserFavouriteTip == 2)
                //{
                //    responseMessage.Message = "Tip Removed";
                //    return BadRequest(responseMessage);
                //}
                //else if (setuserFavouriteTip == 1)
                //{
                //    responseMessage.Message = "Tip Added";
                //    responseMessage.IsSuccess = true;
                //    return Ok(responseMessage);
                //}
                //return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(SetCompanyUserFavouriteTip));
                throw;
            }
        }

        [HttpGet("DepartmentEmployerCategoryListAsync")]
        public async Task<ActionResult<CompanyCategoryUserList>> DepartmentEmployerCategoryListAsync()
        {
            var employerHelpCategoryList = await _mediator.Send(new DepartmentEmployerCategoryListQuery());
            return Ok(employerHelpCategoryList);
        }
        [HttpGet("GetDepartmentEmployerHelpAsync/{Id:int}")]
        public async Task<ActionResult<DepartmentEmployerHelpList>> GetDepartmentEmployerHelpListAsync([FromRoute] int Id)
        {
            var helpList = await _mediator.Send(new DepartmentEmployerHelpListByDepartmentQuery
            {
                Id = Id,
                UserEmail = _claimService.GetUserEmail(),
                Language = _claimService.GetLanguage(), 
            });
            return Ok(helpList);
        }
        [HttpGet("SetDepartmentUserFavouriteHelp/{Id:int}")]
        public async Task<ActionResult<ResponseMessage>> SetDepartmentUserFavouriteHelp([FromRoute] int Id)
        {
            var departmentHelp = await _mediator.Send(new SetDepartmentfavouriteHelpQuery
            {
                Id = Id,
                UserEmail = _claimService.GetUserEmail(),
            });
            return Ok(departmentHelp);
        }
    }
}
