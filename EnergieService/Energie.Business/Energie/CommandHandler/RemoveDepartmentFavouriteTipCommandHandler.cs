using Energie.Business.Energie.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class RemoveDepartmentFavouriteTipCommandHandler : IRequestHandler<RemoveDepartmentFavouriteTipCommand, ResponseMessage>
    {
        private readonly ILogger<RemoveDepartmentFavouriteTipCommandHandler> _logger;
        private readonly IDepartmentTipRepository _departmentTipRepository;
        private readonly ICompanyHelpCategoryRepository _companyHelpCategoryRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public RemoveDepartmentFavouriteTipCommandHandler(ILogger<RemoveDepartmentFavouriteTipCommandHandler> logger
            , IDepartmentTipRepository departmentTipRepository
            , ICompanyUserRepository companyUserRepository
            , ICompanyHelpCategoryRepository companyHelpCategoryRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _departmentTipRepository = departmentTipRepository;
            _companyUserRepository = companyUserRepository;
            _companyHelpCategoryRepository = companyHelpCategoryRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(RemoveDepartmentFavouriteTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                if (request.TipBy == "SuperAdmin")
                {
                    await _departmentTipRepository.RemovedDepartmentFavouriteTipsAsync(user.Id, request.DepartmentTipId);
                    responseMessage.IsSuccess = true;

                }
                else if (request.TipBy == "CompanyAdmin")
                {
                    await _companyHelpCategoryRepository.DeleteDepartmentFavouriteForUserAsync(request.DepartmentTipId, user.Id);
                    responseMessage.IsSuccess = true;
                }
                else if (request.TipBy == "User")
                {
                    await _departmentTipRepository.RemoveUserDepartmentFavouriteTipsAsync(request.DepartmentTipId, (int)user.DepartmentID);
                    responseMessage.IsSuccess = true;
                }
                responseMessage.Message = _localizer["Department_tip_removed"].Value;
                return responseMessage;
            }
            catch (Exception ex)
            {             
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(RemoveDepartmentFavouriteTipCommandHandler));
                throw; 
            }
        }
    }
}
