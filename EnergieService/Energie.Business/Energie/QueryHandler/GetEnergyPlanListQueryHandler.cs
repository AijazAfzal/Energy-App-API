using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{
    public class GetEnergyPlanListQueryHandler : IRequestHandler<GetEnergyPlanListQuery, EnergyPlanResponseList>
    {
        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly ILogger<GetEnergyPlanListQueryHandler> _logger;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        //private readonly TranslationService<UserFavouriteTip> _translationService;
        //private readonly TranslationService<UserFavouriteHelp> _translationUserFavouriteHelp;
        //private readonly TranslationService<UserTip> _translationUserTip;
        //private readonly TranslationService<PlanStatus> _translationPlanStatus;

        private readonly ITranslationsRepository<UserFavouriteTip> _translationService;
        private readonly ITranslationsRepository<UserFavouriteHelp> _translationUserFavouriteHelp;
        private readonly ITranslationsRepository<UserTip> _translationUserTip;
        private readonly ITranslationsRepository<PlanStatus> _translationPlanStatus;


        public GetEnergyPlanListQueryHandler(IEnergyPlanRepository energyPlanRepository,
            ILogger<GetEnergyPlanListQueryHandler> logger,
            IUserEnergyTipRepository userEnergyTipRepository,
            IEnergyAnalysisRepository energyAnalysisRepository,
            ITranslationsRepository<UserFavouriteTip> translationService, ITranslationsRepository<UserFavouriteHelp> translationUserFavouriteHelp, ITranslationsRepository<UserTip> translationUserTip, ITranslationsRepository<PlanStatus> translationPlanStatus)
        {
            _energyPlanRepository = energyPlanRepository;
            _logger = logger;
            _userEnergyTipRepository = userEnergyTipRepository;
            _energyAnalysisRepository = energyAnalysisRepository;
            _translationService = translationService;
            _translationUserTip = translationUserTip;
            _translationUserFavouriteHelp = translationUserFavouriteHelp;
            _translationPlanStatus = translationPlanStatus; 
        }

        public async Task<EnergyPlanResponseList> Handle(GetEnergyPlanListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                #region Get favourite tip for all  Optimize code 
                var getallList = await _userEnergyTipRepository.GetUserFavoritesAsync(request.UserEmail);

                var userFavouriteTips = getallList.Item1.Select(x => new Model.Request.UserFavouriteTipRequest
                {
                    Id = x.Id,
                    Name = x.Tips.Name,
                    Description = x.Tips.Description,
                    AddedDate = x.Tips.CreatedOn.Date,
                    TipBy = "SuperAdmin"
                }).ToList();
                var employerHelp = getallList.Item2.Select(x => new Model.Request.UserFavouriteTipRequest
                {
                    Id = x.Id,
                    Name = x.CompanyHelp.Name,
                    Description = x.CompanyHelp.Description,
                    AddedDate = x.CompanyHelp.CreatedOn.Date,
                    TipBy = "CompanyAdmin"
                }).ToList();
                var userTip = getallList.Item3.Select
                    (x => new Model.Request.UserFavouriteTipRequest
                    {
                        Id = x.Id,
                        Name = x.EnergyAnalysisQuestions.Name,
                        Description = x.Description,
                        AddedDate = x.CreatedOn.Date,
                        TipBy = "User"
                    }).ToList();

                userFavouriteTips.AddRange(employerHelp);
                userFavouriteTips.AddRange(userTip);
                var energyplanss = new List<EnergyPlanResponse>();


                var energyPlan = await _energyPlanRepository.GetEnergyPlanListAsync(request.UserEmail);

                if (request.Language == "en-US")
                {
                    var translatedPlanName = await _translationService.GetTranslatedDataAsync<UserFavouriteTip>(request.Language);
                    var translatedUserFavouriteHelp = await _translationUserFavouriteHelp.GetTranslatedDataAsync<UserFavouriteHelp>(request.Language);
                    var translatedPlanUserTip = await _translationUserTip.GetTranslatedDataAsync<UserTip>(request.Language);
                    var planStatus = await _translationPlanStatus.GetTranslatedDataAsync<PlanStatus>(request.Language);

                    foreach (var item in userFavouriteTips)
                    {
                        foreach (var item2 in energyPlan)
                        {
                            if (item2.FavouriteTipId == item.Id && item2.TipType.Name == item.TipBy)
                            {
                                //item2.PlanStatus.Name = planStatus;

                                if (item.TipBy == "SuperAdmin")
                                {
                                    var translatedItem = translatedPlanName.FirstOrDefault(tf => tf.Id == item.Id);
                                    if (translatedItem != null)
                                    {
                                        item.Name = translatedItem.Tips.Name;
                                        item.Description = translatedItem.Tips.Description;
                                    }
                                }
                                else if (item.TipBy == "CompanyAdmin")
                                {
                                    var translatedItem = translatedUserFavouriteHelp.FirstOrDefault(tf => tf.Id == item.Id);
                                    if (translatedItem != null)
                                    {
                                        item.Name = translatedItem.CompanyHelp.Name;
                                        item.Description = translatedItem.CompanyHelp.Description;
                                    }
                                }
                                else if (item.TipBy == "User")
                                {
                                    var translatedItem = translatedPlanUserTip.FirstOrDefault(tf => tf.Id == item.Id);
                                    if (translatedItem != null)
                                    {
                                        item.Name = translatedItem.EnergyAnalysisQuestions.Name;
                                        item.Description = translatedItem.Description;
                                    }
                                }

                                energyplanss.Add(new EnergyPlanResponse
                                {
                                    PlanName = item.Name,
                                    PlanDescription = item.Description,
                                    ResponsiblePerson = item2.ResponsiblePerson.UserName,
                                    EndDate = item2.PlanEndDate,
                                    Id = item2.Id,
                                    FavouriteTipId = item2.FavouriteTipId,
                                    TipBy = item2.TipType.Name,
                                    PlanStatus = item2.PlanStatus.Name,
                                    IsReminder = item2.IsReminder
                                });
                            }
                        }
                    }
                }
                #endregion

                else
                {
                    foreach (var item in userFavouriteTips)
                    {
                        foreach (var item2 in energyPlan)
                        {
                            if (item2.FavouriteTipId == item.Id && item2.TipType.Name == item.TipBy)
                            {

                                energyplanss.Add(new EnergyPlanResponse
                                {
                                    PlanName = item.Name,
                                    PlanDescription = item.Description,
                                    ResponsiblePerson = item2.ResponsiblePerson.UserName,
                                    EndDate = item2.PlanEndDate,
                                    Id = item2.Id,
                                    FavouriteTipId = item2.FavouriteTipId,
                                    TipBy = item2.TipType.Name,
                                    PlanStatus = item2.PlanStatus.Name,
                                    IsReminder = item2.IsReminder
                                });
                            }
                        }
                    }

                }
                return new EnergyPlanResponseList { EnergyPlanResponse = energyplanss };


            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"error occured in {{0}}", nameof(GetEnergyPlanListQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetEnergyPlanListQueryHandler));
                throw;
            }
        }
    }
}

