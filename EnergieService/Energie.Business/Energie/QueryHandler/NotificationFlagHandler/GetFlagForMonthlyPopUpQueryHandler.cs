using Energie.Business.Energie.Query;
using Energie.Business.Energie.Query.NotificationFlag;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
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
    public class GetFlagForMonthlyPopUpQueryHandler : IRequestHandler<GetFlagForMonthlyPopUpQuery, ResponseForMonthlyPopup>
    {
        private readonly IFlagRepository _flagRepository;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly ICompanyUserRepository _userScoreRepository;
        private readonly ILogger<GetFlagForMonthlyPopUpQueryHandler> _logger;
        public GetFlagForMonthlyPopUpQueryHandler(IFlagRepository flagRepository
            , ILogger<GetFlagForMonthlyPopUpQueryHandler> logger
            , IEnergyScoreRepository energyScoreRepository,
              ICompanyUserRepository userScoreRepository)
        {
            _flagRepository = flagRepository;
            _energyScoreRepository = energyScoreRepository;
            _logger = logger;
            _userScoreRepository = userScoreRepository;
        }

        public async Task<ResponseForMonthlyPopup> Handle(GetFlagForMonthlyPopUpQuery request, CancellationToken cancellationToken)
        {
            var responseForMonthlyPopup = new ResponseForMonthlyPopup();
            try
            {
                DateTime datetime = DateTime.Now;
                var currentMonth = datetime.Month;
                var currentYear = datetime.Year;
                var userScores = await _energyScoreRepository.GetAllEnergyScores(request.UserEmail);
                var user = await _userScoreRepository.GetCompanyUserAsync(request.UserEmail);
                
                var companyUserId = userScores.Where(x => x.MonthId == currentMonth && x.Year == currentYear)
                                              .Select(x => x.CompanyUserID)
                                              .FirstOrDefault().ToString();
                if (string.IsNullOrEmpty(companyUserId))
                {
                    MonthlyNotification monthlyNotification = new MonthlyNotification();
                    monthlyNotification.SetFlagForPopUp(user.Id, currentMonth, currentYear, false);
                    var popupMessage = await _flagRepository.GetMonthlyNotificationAsync(monthlyNotification);
                    if (popupMessage != null)
                    {
                        responseForMonthlyPopup.IsPopUp = false;
                        return responseForMonthlyPopup;
                    }
                    else
                    {

                        await _flagRepository.SetMonthlyNotificationAsync(monthlyNotification);
                        responseForMonthlyPopup.IsPopUp = true;
                        return responseForMonthlyPopup;
                    }
                }
                else
                {
                    responseForMonthlyPopup.IsRecordAlredyExist = true;
                    return responseForMonthlyPopup;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}");
                throw;
            }
            
        }
    }
}
