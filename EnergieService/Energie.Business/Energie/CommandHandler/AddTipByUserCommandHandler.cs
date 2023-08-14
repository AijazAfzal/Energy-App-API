using Energie.Business.Energie.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddTipByUserCommandHandler : IRequestHandler<AddTipByUserCommand, ResponseMessage>
    {
        private readonly ILogger<AddTipByUserCommandHandler> _logger;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddTipByUserCommandHandler(ILogger<AddTipByUserCommandHandler> logger
            , IUserEnergyTipRepository userEnergyTipRepository
            , ICompanyUserRepository companyUserRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _userEnergyTipRepository = userEnergyTipRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddTipByUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var addUserTip = new UserTip().SetUserTip(request.categoryId,
                                                          (int)user.Id,
                                                          request.description
                                                          );
                await _userEnergyTipRepository.AddUserFavouriteTipAsync(addUserTip);
                responseMessage.IsSuccess = true;
                responseMessage.Id = addUserTip.Id;               
                responseMessage.Message = _localizer["User_tip_added"].Value;
                return responseMessage; 

            }
            catch (Exception ex)
            {              
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}",nameof(AddTipByUserCommandHandler));  
                throw ex;  
            }
        }
    }
}
