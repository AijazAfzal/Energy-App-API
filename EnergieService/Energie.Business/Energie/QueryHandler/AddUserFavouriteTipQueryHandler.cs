using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class AddUserFavouriteTipQueryHandler : IRequestHandler<AddUserFavouriteTipQuery, ResponseMessage>
    {
        private readonly ILogger<AddUserFavouriteTipQueryHandler> _logger;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly IClaimService _tokenClaimService;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddUserFavouriteTipQueryHandler(ILogger<AddUserFavouriteTipQueryHandler> logger
            , IUserEnergyTipRepository userEnergyTipRepository
            , IClaimService tokenClaimService
            , ICompanyUserRepository companyUserRepository
            , IStringLocalizer<Resources.Resources> localizer
            )
        {
            _logger = logger;
            _userEnergyTipRepository = userEnergyTipRepository;
            _tokenClaimService = tokenClaimService;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddUserFavouriteTipQuery request, CancellationToken cancellationToken)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            var userEmail = _tokenClaimService.GetUserEmail();
            try
            {
                var user = await _companyUserRepository.GetCompanyUserAsync(userEmail);
                var userfavouriteTip = new UserFavouriteTip().SetUserFavouriteTip(request.tipID, user.Id);
                var alreadyExistTip = await _userEnergyTipRepository.GetFavouriteTipById(userfavouriteTip);
                if(alreadyExistTip != null )
                {
                    responseMessage.Message = _localizer["Favorite_tip_exists"].Value; 
                }
                else
                {
                    await _userEnergyTipRepository.SetUserFavouriteTip(userfavouriteTip);
                    responseMessage.Message = _localizer["Favorite_tip_added"].Value;  
                    responseMessage.IsSuccess = true;
                    
                }
                return responseMessage;
            }
            catch (Exception ex)
            {               
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(AddUserFavouriteTipQueryHandler));  
                throw;
            }
        }
    }
}
