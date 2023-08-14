using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Business.Energie.Query.NotificationFlag;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler.NotificationFlagHandler
{
    public class ShowOnboardingFlagQueryHandler : IRequestHandler<ShowOnboardingFlagQuery, ShowOnboardingResponse>
    {
        private readonly ILogger<ShowOnboardingFlagQueryHandler> _logger;
        private readonly IFlagRepository _flagRepository;
        public ShowOnboardingFlagQueryHandler(IFlagRepository flagRepository
            , ILogger<ShowOnboardingFlagQueryHandler> logger)
        {
            _flagRepository = flagRepository;
            _logger = logger;
        }

        public async Task<ShowOnboardingResponse> Handle(ShowOnboardingFlagQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var showOnboardingResponse = new ShowOnboardingResponse();
                var userOnboarding = await _flagRepository.GetUserOnboarding(request.UserEmail);
                if (userOnboarding.ShowOnboarding == true)
                {
                    userOnboarding.UpdateCompanyUser(false);
                    await _flagRepository.UpdateUserOnboarding(userOnboarding);
                    showOnboardingResponse.ShowOnboarding = true;
                    return showOnboardingResponse;
                }
                return showOnboardingResponse;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}");
                throw;
            }
        }
    }
}
