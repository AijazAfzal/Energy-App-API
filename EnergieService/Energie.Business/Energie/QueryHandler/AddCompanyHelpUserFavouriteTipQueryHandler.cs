using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class AddCompanyHelpUserFavouriteTipQueryHandler : IRequestHandler<AddCompanyHelpUserFavouriteTipQuery, ResponseMessage>
    {
        private readonly ILogger<AddCompanyHelpUserFavouriteTipQueryHandler> _logger;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddCompanyHelpUserFavouriteTipQueryHandler(ILogger<AddCompanyHelpUserFavouriteTipQueryHandler> logge
            , IUserEnergyTipRepository userEnergyTipRepository
            , ICompanyUserRepository companyUserRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logge;
            _userEnergyTipRepository = userEnergyTipRepository;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer; 
        }
        public async Task<ResponseMessage> Handle(AddCompanyHelpUserFavouriteTipQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ResponseMessage responseMessage = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var userfavouriteTip = new UserFavouriteHelp().SetUserFavouriteHelp(request.Id, user.Id);
                var alreadyExistTip = await _userEnergyTipRepository.GetUserFavouriteHelpByIdAsync(userfavouriteTip);
                if (alreadyExistTip != null)
                {
                    await _userEnergyTipRepository.RemoveEmployerFavouriteHelpAsync(alreadyExistTip.Id, user.Id);
                    responseMessage.Message = _localizer["Help_Tip_Removed"].Value;
                    
                }
                else
                {
                    await _userEnergyTipRepository.SetCompanyHelpUserFavouriteTip(userfavouriteTip);
                    responseMessage.Message = _localizer["Help_Tip_Added"].Value;
                    responseMessage.IsSuccess = true;
                 

                }
                return responseMessage;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Occured In {{0}}");              
                throw;
            }
        }
    }
}
