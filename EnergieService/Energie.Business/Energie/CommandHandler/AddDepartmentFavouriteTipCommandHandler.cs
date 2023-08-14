using Energie.Business.Energie.Command;
using Energie.Business.Energie.QueryHandler;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddDepartmentFavouriteTipCommandHandler : IRequestHandler<AddDepartmentFavouriteTipCommand, ResponseMessage>
    {
        private readonly ILogger<AddDepartmentFavouriteTipCommandHandler> _logger;
        private readonly IDepartmentTipRepository _departmentTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddDepartmentFavouriteTipCommandHandler(ILogger<AddDepartmentFavouriteTipCommandHandler> logger,
            IDepartmentTipRepository departmentTipRepository,
            ICompanyUserRepository companyUserRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _departmentTipRepository = departmentTipRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddDepartmentFavouriteTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responsemassage = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var setdepartmentTip = new Domain.Domain.DepartmentFavouriteTip().SetDepartmentFavouriteTip(request.Id, user.Id);
                var existuserDepartmentTip = await _departmentTipRepository.UserFavouriteDepartmentTipAsync(user.Id, request.Id);
                if (existuserDepartmentTip != null)
                {
                    await _departmentTipRepository.RemovedDepartmentFavouriteTipsAsync(user.Id, request.Id);
                    responsemassage.Message = _localizer["Department_favorite_tip_Removed"].Value; 
                    return responsemassage; 
                }
                await _departmentTipRepository.AddUserFavouriteDepartmentTipAsync(setdepartmentTip);
                responsemassage.IsSuccess = true;
                responsemassage.Message = _localizer["Department_favorite_tip_with_ID_added_for_the_user"].Value; 
                responsemassage.Id = setdepartmentTip.Id;
                return responsemassage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(DepartmentEnergyTipQueryHandler)); 
                throw;
            }
        }
    }
}
