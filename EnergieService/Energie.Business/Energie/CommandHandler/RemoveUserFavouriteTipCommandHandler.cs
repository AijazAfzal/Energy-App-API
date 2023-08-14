using Energie.Business.Energie.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class RemoveUserFavouriteTipCommandHandler : IRequestHandler<RemoveUserFavouriteTipCommand, ResponseMessage>
    {
        private readonly ILogger<RemoveUserFavouriteTipCommandHandler> _logger;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public RemoveUserFavouriteTipCommandHandler(ILogger<RemoveUserFavouriteTipCommandHandler> logger
            , IUserEnergyTipRepository userEnergyTipRepository
            , ICompanyUserRepository companyUserRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _userEnergyTipRepository = userEnergyTipRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(RemoveUserFavouriteTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                if (request.TipBy == "SuperAdmin")
                {
                    await _userEnergyTipRepository.RemoveSuperAdminFavouriteTipAsync(request.TipId, user.Id);
                    responseMessage.IsSuccess = true;

                }
                else if (request.TipBy == "CompanyAdmin")
                {
                    await _userEnergyTipRepository.RemoveEmployerFavouriteHelpAsync(request.TipId, user.Id);
                    responseMessage.IsSuccess = true;
                }
                else if (request.TipBy == "User")
                {
                    await _userEnergyTipRepository.RemoveUserFavouriteHelpAsync(request.TipId, user.Id);
                    responseMessage.IsSuccess = true;
                }
                responseMessage.Message = _localizer["Favorite_tip_removed"].Value;
              
                return responseMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}"); 
                throw ex;
            }
        }
    }
}
