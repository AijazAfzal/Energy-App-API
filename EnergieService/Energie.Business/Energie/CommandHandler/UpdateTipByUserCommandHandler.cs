using Energie.Business.Energie.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class UpdateTipByUserCommandHandler : IRequestHandler<UpdateTipByUserCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateTipByUserCommandHandler> _logger;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateTipByUserCommandHandler(ILogger<UpdateTipByUserCommandHandler> logger
            , IUserEnergyTipRepository userEnergyTipRepository
            , ICompanyUserRepository companyUserRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _userEnergyTipRepository = userEnergyTipRepository;
            _localizer = localizer;
        }

        public async Task<ResponseMessage> Handle(UpdateTipByUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var usertip = await _userEnergyTipRepository.GetUserFavouriteTipByIdAsync(request.Id);
                var addupdatedtip = usertip.UpdateUserTip(request.categoryId, request.description);
                var tipupdatebyuser = await _userEnergyTipRepository.UpdateUserFavouriteTipAsync(addupdatedtip);
                if (tipupdatebyuser != null)
                {
                    responseMessage.IsSuccess = true;               
                    responseMessage.Message = _localizer["User_tip_updated"].Value;
                    responseMessage.Id = tipupdatebyuser.Id;
                    return responseMessage;
                }
                responseMessage.Message = _localizer["Something_wrong"].Value; 
                return responseMessage;
            }
            catch (Exception ex)
            {               
                _logger.LogError(ex, $"fout opgetreden in {{0}}");
                throw ex;
            }
        }
    }
}
