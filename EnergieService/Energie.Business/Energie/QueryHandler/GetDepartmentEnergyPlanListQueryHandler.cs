using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{
    public class GetDepartmentEnergyPlanListQueryHandler : IRequestHandler<GetDepartmentEnergyPlanListQuery, DepartmentEnergyPlanResponseList>
    {
        private readonly IEnergyPlanRepository _energyPlanRepository;
        private readonly ILogger<GetEnergyPlanListQueryHandler> _logger;
        private readonly IDepartmentTipRepository _departmentTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ITranslationsRepository<Domain.Domain.DepartmentFavouriteTip> _translationService;
        private readonly ITranslationsRepository<Domain.Domain.DepartmentFavouriteHelp> _translationDepartmentFavouriteHelp;
        private readonly ITranslationsRepository<Domain.Domain.UserDepartmentTip> _translationUserDepartmentTip;
        private readonly ITranslationsRepository<Domain.Domain.PlanStatus> _translationPlanStatus;


        public GetDepartmentEnergyPlanListQueryHandler(IEnergyPlanRepository energyPlanRepository
            , ILogger<GetEnergyPlanListQueryHandler> logger
            , IDepartmentTipRepository departmentTipRepository
            , ICompanyUserRepository companyUserRepository,
ITranslationsRepository<Domain.Domain.DepartmentFavouriteTip> translationService, ITranslationsRepository<Domain.Domain.DepartmentFavouriteHelp> translationDepartmentFavouriteHelp, ITranslationsRepository<Domain.Domain.UserDepartmentTip> translationUserDepartmentTip, ITranslationsRepository<PlanStatus> translationPlanStatus)
        {
            _energyPlanRepository = energyPlanRepository;
            _logger = logger;
            _departmentTipRepository = departmentTipRepository;
            _companyUserRepository = companyUserRepository;
            _translationService = translationService;
            _translationDepartmentFavouriteHelp = translationDepartmentFavouriteHelp;
            _translationUserDepartmentTip = translationUserDepartmentTip;
            _translationPlanStatus = translationPlanStatus;
        }

        public async Task<DepartmentEnergyPlanResponseList> Handle(GetDepartmentEnergyPlanListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Companyuser = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var superadminfavtip = await _departmentTipRepository.UserFavouriteDepartmentTipListAsync(request.UserEmail);
                var employerfavhelp = await _departmentTipRepository.DepartmentFavouriteHelpListAsync(request.UserEmail);
                var userfavtip = await _departmentTipRepository.GetUserAddedDepartmentTip((int)Companyuser.DepartmentID);
                var superadmintip = superadminfavtip.Select(x => new Model.Request.DepartmentFavouriteTip
                {
                    Id = (int)x.DepartmentTipId,
                    Name = x.DepartmentTip.Name,
                    Description = x.DepartmentTip.Description,
                    AddedDate = x.DepartmentTip.CreatedOn.Date,
                    TipBy = "SuperAdmin",
                    IsSelected = true
                }).ToList();
                var employerhelp = employerfavhelp.Select(x => new Model.Request.DepartmentFavouriteTip
                {
                    Id = x.CompanyDepartmentHelpId,
                    Name = x.CompanyDepartmentHelps.Name,
                    Description = x.CompanyDepartmentHelps.Description,
                    AddedDate = x.CompanyDepartmentHelps.CreatedDate.Date,
                    TipBy = "CompanyAdmin",
                    IsSelected = true
                }).ToList();
                var usertip = userfavtip.Select(x => new Model.Request.DepartmentFavouriteTip
                {
                    Id = x.Id,
                    Name = x.EnergyAnalysisQuestions.Name,
                    Description = x.Description,
                    AddedDate = x.CreatedOn,
                    TipBy = "User",
                    IsSelected = true
                }).ToList();

                superadmintip.AddRange(employerhelp);
                superadmintip.AddRange(usertip);
                var departmentEnergyPlan = new List<DepartmentEnergyPlanResponse>();
                var departmentplan = await _energyPlanRepository.GetDepartmentEnergyPlanListAsync(request.UserEmail);

                if (superadmintip == null)
                {
                    return default;
                }


                if (request.Language == "en-US")
                {
                    var translatedDepartmentFavouriteTip = await _translationService.GetTranslatedDataAsync<DepartmentFavouriteTip>(request.Language);
                    var translatedDepartmentFavouriteHelp = await _translationDepartmentFavouriteHelp.GetTranslatedDataAsync<DepartmentFavouriteHelp>(request.Language);
                    var translatedUserDepartmentTip = await _translationUserDepartmentTip.GetTranslatedDataAsync<UserDepartmentTip>(request.Language);
                    var planStatus = await _translationPlanStatus.GetTranslatedDataAsync<PlanStatus>(request.Language); 
                    foreach (var item in superadmintip)
                    {
                        foreach (var item2 in departmentplan)
                        {
                            if (item2.FavouriteTipId == item.Id && item2.TipType.Name == item.TipBy)
                            {

                                if (item.TipBy == "SuperAdmin")
                                {
                                  
                                    var translatedItem = translatedDepartmentFavouriteTip?.FirstOrDefault(tf => tf.DepartmentTip?.ID == item.Id);
                                    if (translatedItem != null && translatedItem.DepartmentTip != null)
                                    {
                                        item.Name = translatedItem.DepartmentTip.Name;
                                        item.Description = translatedItem.DepartmentTip.Description;
                                    }

                                }
                                else if (item.TipBy == "CompanyAdmin")
                                {
                                    var translatedItem = translatedDepartmentFavouriteHelp.FirstOrDefault(tf => tf.Id == item.Id);
                                    if (translatedItem != null)
                                    {
                                        item.Name = translatedItem.CompanyDepartmentHelps.Name;
                                        item.Description = translatedItem.CompanyDepartmentHelps.Description;
                                       
                                    }
                                }
                                else if (item.TipBy == "User")
                                {
                                    var translatedItem = translatedUserDepartmentTip.FirstOrDefault(tf => tf.Id == item.Id);
                                    if (translatedItem != null)
                                    {
                                        item.Name = translatedItem.EnergyAnalysisQuestions.Name;
                                        item.Description = translatedItem.Description;
                                    }
                                }

                                departmentEnergyPlan.Add(new DepartmentEnergyPlanResponse
                                {
                                    PlanName = item.Name,
                                    PlanDescription = item.Description,
                                    EndDate = item2.PlanEndDate,
                                    ResponsiblePerson = item2.ResponsiblePerson.UserName,
                                    Id = item2.Id,
                                    FavouriteTipId = item2.FavouriteTipId,
                                    TipBy = item2.TipType.Name,
                                    PlanStatus = item2.PlanStatus.Name,
                                    IsReminder = item2.IsReminder,
                                });
                            }
                        }
                    }
                }

                else
                {

                    foreach (var item in superadmintip)
                    {
                        foreach (var item2 in departmentplan)
                        {
                            if (item2.FavouriteTipId == item.Id && item2.TipType.Name == item.TipBy)
                            {
                                departmentEnergyPlan.Add(new DepartmentEnergyPlanResponse
                                {
                                    PlanName = item.Name,
                                    PlanDescription = item.Description,
                                    EndDate = item2.PlanEndDate,
                                    ResponsiblePerson = item2.ResponsiblePerson.UserName,
                                    Id = item2.Id,
                                    FavouriteTipId = item2.FavouriteTipId,
                                    TipBy = item2.TipType.Name,
                                    PlanStatus = item2.PlanStatus.Name,
                                    IsReminder = item2.IsReminder,
                                });

                            }
                        }
                    }
                }
                return new DepartmentEnergyPlanResponseList { departmentEnergyPlanResponses = departmentEnergyPlan };
            }

            catch (Exception ex)
            {
                //_logger.LogError(ex, $"error occured in {{0}}", nameof(GetDepartmentEnergyPlanListQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetDepartmentEnergyPlanListQueryHandler));
                throw;

            }


        }
    }
}
