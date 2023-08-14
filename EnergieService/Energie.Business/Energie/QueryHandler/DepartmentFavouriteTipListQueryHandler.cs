using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Domain.IRepository;
using Energie.Infrastructure.Repository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentFavouriteTipListQueryHandler : IRequestHandler<DepartmentFavouriteTipListQuery, DepartmentEnergyTipsList> 
    {
        private readonly ILogger<DepartmentFavouriteTipListQueryHandler> _logger;
        private readonly IDepartmentTipRepository _departmentTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ITranslationsRepository<Domain.Domain.DepartmentTip> _translationService;
        private readonly ITranslationsRepository<Domain.Domain.DepartmentFavouriteHelp> _translationDepartmentFavouriteHelp;
        private readonly ITranslationsRepository<Domain.Domain.UserDepartmentTip> _translationUserDepartmentTip;

        private readonly ILikeTipRepository _likeTipRepository;
        private readonly IMapper _mapper;
        public DepartmentFavouriteTipListQueryHandler(ILogger<DepartmentFavouriteTipListQueryHandler> logger
            , IDepartmentTipRepository departmentTipRepository
            , ICompanyUserRepository companyUserRepository
            , ITranslationsRepository<Domain.Domain.DepartmentTip> translationService
            , ITranslationsRepository<Domain.Domain.DepartmentFavouriteHelp> translationDepartmentFavouriteHelp
            , ITranslationsRepository<Domain.Domain.UserDepartmentTip> translationUserDepartmentTip
            , ILikeTipRepository likeTipRepository
            , IMapper mapper)
        {
            _logger = logger;
            _departmentTipRepository = departmentTipRepository;
            _companyUserRepository = companyUserRepository;
            _translationService = translationService;
            _translationDepartmentFavouriteHelp = translationDepartmentFavouriteHelp;
            _translationUserDepartmentTip = translationUserDepartmentTip;
            _likeTipRepository = likeTipRepository;
            _mapper = mapper;

        }
        public async Task<DepartmentEnergyTipsList> Handle(DepartmentFavouriteTipListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var departmentTips = await _departmentTipRepository.GetDepatrmentTipListAsync(); 
                var userFavourtieTipList = await _departmentTipRepository.UserFavouriteDepartmentTipListAsync(request.UserEmail);
                var empfavHelp = await _departmentTipRepository.DepartmentFavouriteHelpListAsync(request.UserEmail);
                var userFavouritetip = await _departmentTipRepository.GetUserAddedDepartmentTip((int)user.DepartmentID); 
                var empfavHelps = empfavHelp.Select
                                 (x => new Model.Request.DepartmentEnergyTip
                                 {
                                     Id = x.Id,
                                     Name = x.CompanyDepartmentHelps.Name,
                                     Description = x.CompanyDepartmentHelps.Description,  
                                     AddedDate = x.CreatedDate.Date,
                                     TipBy = "CompanyAdmin",
                                     IsSelected = true
                                 }).ToList();
                var userAddedTip = userFavouritetip.Select
                    (x => new Model.Request.DepartmentEnergyTip
                    {
                        Id = x.Id,
                        Name = x.EnergyAnalysisQuestions.Name,
                        Description = x.Description,
                        AddedDate = x.CreatedOn.Date,
                        TipBy = "User",
                        IsSelected = true
                    }).ToList();

                var likedTip = await _likeTipRepository.GetLikeTipListAsync(request.UserEmail);
                var id = departmentTips.Select(x => x.ID).Distinct().ToList();
                var userEnergyAnalysis = new List<Domain.Domain.DepartmentTip>();

                foreach (var item in userFavourtieTipList)
                {
                    if (id.Contains(item.DepartmentTipId.Value))
                    {
                        var value = departmentTips.Where(x => x.ID == item.DepartmentTipId.Value).FirstOrDefault();
                        userEnergyAnalysis.Add(value);
                    }
                }

                var userDepartmentTips = departmentTips.Select
                                 (x => new DepartmentEnergyTipsList
                                 {
                                     DepartmentEnergyTips = _mapper.Map<List<DepartmentEnergyTip>>(userEnergyAnalysis)

                                 }).FirstOrDefault(); 


                foreach (var Ut in userDepartmentTips.DepartmentEnergyTips)
                {
                    foreach (var ft in userFavourtieTipList)
                    {
                        if (ft.DepartmentTipId == Ut.Id)
                        {
                            Ut.IsSelected = true;
                            Ut.TipBy = "SuperAdmin";
                            Ut.AddedDate = ft.Created.Date;  
                        }
                    }
                    foreach (var lt in likedTip)
                    {
                        if (lt.DepartmentTipId == Ut.Id)
                        {
                            Ut.IsLiked = true; 
                        }
                    }
                  
                }

                empfavHelps.AddRange(userAddedTip);
                empfavHelps.AddRange(userDepartmentTips.DepartmentEnergyTips);

              
                if (request.Language == "en-US")
                {
                    var translatedDepartmentFavouriteTip = await _translationService.GetTranslatedDataAsync<Domain.Domain.DepartmentTip>(request.Language);
                    var translatedDepartmentFavouriteHelp = await _translationDepartmentFavouriteHelp.GetTranslatedDataAsync<Domain.Domain.DepartmentFavouriteHelp>(request.Language);
                    var translatedHelpUserDepartmentTip = await _translationUserDepartmentTip.GetTranslatedDataAsync<Domain.Domain.UserDepartmentTip>(request.Language);
                    var translatedDepartmentFavouriteTips = empfavHelps.Select(t =>
                    {
                        var translatedName = string.Empty;
                        var translatedDescription = string.Empty;

                        if (t.TipBy == "User")
                        {
                            translatedName = translatedHelpUserDepartmentTip.FirstOrDefault(tf => tf.Id == t.Id)?.EnergyAnalysisQuestions?.Name;
                            translatedDescription = t.Description;
                        }
                        else if (t.TipBy == "CompanyAdmin")
                        {
                            translatedName = translatedDepartmentFavouriteHelp.FirstOrDefault(tf => tf.Id == t.Id)?.CompanyDepartmentHelps.Name;
                            translatedDescription = translatedDepartmentFavouriteHelp.FirstOrDefault(tf => tf.Id == t.Id)?.CompanyDepartmentHelps.Description;
                        }
                        else
                        {

                            translatedName = translatedDepartmentFavouriteTip.FirstOrDefault(tf => tf.ID == t.Id)?.Name;
                            translatedDescription = translatedDepartmentFavouriteTip.FirstOrDefault(tf => tf.ID == t.Id)?.Description;
                        }

                        return new Model.Request.DepartmentEnergyTip
                        {
                            Id = t.Id,
                            Name = translatedName,
                            Description = translatedDescription,
                            AddedDate = t.AddedDate,
                            TipBy = t.TipBy,
                            IsSelected = t.IsSelected,
                            Count = t.Count,    
                            IsLiked = t.IsLiked,
                        };
                    }).ToList();


                    return new DepartmentEnergyTipsList { DepartmentEnergyTips = translatedDepartmentFavouriteTips };

                }

                return new DepartmentEnergyTipsList { DepartmentEnergyTips = empfavHelps };   

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(DepartmentFavouriteTipListQueryHandler));
                throw;
            }
        }
    }
}
