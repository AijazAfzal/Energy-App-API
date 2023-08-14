using Energie.Business.Energie.Query.NotificationFlag;
using Energie.Domain;
using Energie.Domain.IRepository;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging; 


namespace Energie.Business.Energie.QueryHandler.NotificationFlagHandler
{
    public class GetAllUserNotificationQueryHandler : IRequestHandler<GetAllUserNotificationQuery, NotificationMessageList>
    {
        readonly ILogger<GetAllUserNotificationQueryHandler> _logger;
        readonly IUserDepartmentTipRepository _userDepartmentTipRepository;
        readonly IEnergyPlanRepository _energyPlanRepository;
        readonly IFlagRepository _repository;
        readonly IUserEnergyTipRepository _userEnergyTipRepository;
        readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;

        public GetAllUserNotificationQueryHandler(ILogger<GetAllUserNotificationQueryHandler> logger
            , IUserDepartmentTipRepository userDepartmentTipRepository
            , IEnergyPlanRepository energyPlanRepository
            , IUserEnergyTipRepository userEnergyTipRepository
            , IFlagRepository repository
            , IEnergyAnalysisRepository energyAnalysisRepository
            , ICompanyUserRepository companyUserRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _userDepartmentTipRepository = userDepartmentTipRepository;
            _energyPlanRepository = energyPlanRepository;
            _userEnergyTipRepository = userEnergyTipRepository;
            _energyAnalysisRepository = energyAnalysisRepository;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;
            _repository = repository;

        }
        public async Task<NotificationMessageList> Handle(GetAllUserNotificationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var notifications = new List<NotificationMessages>(); 
                var userenergyanalysis = await _energyAnalysisRepository.UserEnergyAnalysisAsync(request.UserEmail); 
                var getallfavtips = await _userEnergyTipRepository.GetUserFavoritesAsync(request.UserEmail);
                var superadminfavtips = getallfavtips.Item1.Select(x => new Model.Request.UserFavouriteTipRequest
                {
                    Id = x.Id,
                    Name = x.Tips.Name,
                    Description = x.Tips.Description
                }).ToList();
                var employerhelpfav = getallfavtips.Item2.Select(x => new Model.Request.UserFavouriteTipRequest
                {
                    Id = x.Id,
                    Name = x.CompanyHelp.Name,
                    Description = x.CompanyHelp.Description
                }).ToList();
                var userTip = getallfavtips.Item3.Select
                    (x => new Model.Request.UserFavouriteTipRequest
                    {
                        Id = x.Id,
                        Name = x.EnergyAnalysisQuestions.Name,
                        Description = x.Description
                    }).ToList();

                superadminfavtips.AddRange(employerhelpfav);
                superadminfavtips.AddRange(userTip);

                var response = await _repository.GetNotificationsForUserAsync(request.UserEmail);
                var enablePlan = response.Where(x=> x.NotificationType?.Notification_Name == "Kennisgeving van de uiterste datum van het energieplan").FirstOrDefault();

                if(enablePlan != null)
                {
                    var plans = await _energyPlanRepository.GetEnergyPlansForDeadline(request.UserEmail);

                    foreach (var item in superadminfavtips)
                    {
                        foreach (var item2 in plans)
                        {
                            if (item2.FavouriteTipId == item.Id)
                            {

                                DateTime currentdatee = DateTime.UtcNow;
                                TimeSpan difference = item2.PlanEndDate.Subtract(currentdatee);
                                int difffernceinDays = (int)Math.Round(difference.TotalDays);
                                {
                                    if (difffernceinDays > 0 && difffernceinDays <= 3)
                                    {
                                        var message = new NotificationMessages
                                        {
                                            Id = item2.Id,
                                            Message = _localizer["Deadline_for_the_Energy_Plan", item.Description, difffernceinDays].Value,
                                            IsSuccess = true
                                        };
                                        notifications.Add(message);
                                    }
                                }
                            }
                        }
                    }

                }


                //Notification For DepartmentTip added by Collegue

                var colleague = response.Where(x => x.NotificationType?.Notification_Name == "Energietip toegevoegd door collega-melding").FirstOrDefault();

                var tipaddedtodepartment = await _userDepartmentTipRepository.GetUserDepartmentTipsAddedByCollegue(request.UserEmail);
                var existinguser = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                if (colleague != null && tipaddedtodepartment != null) 
                {
                        foreach (var tips in tipaddedtodepartment)
                        {
                            if (tips.CompanyUser != existinguser)
                            {
                                var notificationmessage = new NotificationMessages
                                {
                                    Id = tips.Id,                            
                                    Message = _localizer["Tip_added_department_by_User", tips.CompanyUser!.UserName, tips.Description].Value,
                                    IsSuccess = true

                                };
                                notifications.Add(notificationmessage);
                            }
                        }
                    
                }


                //EnergyAnalysis Update Notification 
                var notificationOfEnergyAnalysis = response.Where(x => x.NotificationType?.Notification_Name == "Kennisgeving van actualisering energieanalyse").FirstOrDefault();
                if(notificationOfEnergyAnalysis != null)
                {
                    var analysis = userenergyanalysis.FirstOrDefault();
                    var currentdate = DateTime.Now;
                    var firstdatee = new DateTime(currentdate.Year, 12, 1);
                    var endDatee = firstdatee.AddDays(6);

                    if (currentdate >= firstdatee && currentdate <= endDatee && analysis?.CreatedOn.Month != 12)
                    {
                        var message = new NotificationMessages
                        {
                            Id = existinguser.Id,                            
                            Message = _localizer["Update_energy_analysis_notification"].Value,
                            IsSuccess = true
                        };
                        notifications.Add(message);
                    }
                }

                return new NotificationMessageList { NotificationMessages = notifications };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetAllUserNotificationQueryHandler));
                throw;
            }

        }
    }







}