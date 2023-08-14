using Energie.Api.Helper;
using Energie.Business.SuperAdmin.Helper;
using Energie.Business.SuperAdmin.Query;
using Energie.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Energie.Api.Controllers.User
{
    [Authorize(AuthenticationSchemes = AuthorizationScheme.AzureADB2CUser)]
    [Route("api/energie/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;
        public CategoryController(ILogger<CategoryController> logger,
            IMediator mediator,
            IClaimService claimService)
        {
            _logger = logger;
            _mediator = mediator;
            _claimService = claimService;
        }

        [HttpGet("GetCategoryList")]
        public async Task<ActionResult<CategoryList>> GetCategoryList()
        {
            try
            {
                var categoryList = await _mediator.Send(new CategoryListQuery
                {
                    UserEmail = _claimService.GetUserEmail(),
                    Language = _claimService.GetLanguage(),
                });
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", GetCategoryList);
                throw ex;
            }
        }
    }
}
