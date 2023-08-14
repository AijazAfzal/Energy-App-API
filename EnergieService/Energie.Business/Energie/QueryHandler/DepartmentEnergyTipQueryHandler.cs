using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentEnergyTipQueryHandler : IRequestHandler<DepartmentEnergyTipQuery, DepartmentEnergyTipList>
    {
        private readonly ILogger<DepartmentEnergyTipQueryHandler> _logger;
        private readonly IDepartmentTipRepository _departmentTipRepository;
        private readonly ILikeTipRepository _likeTipRepository;
        private readonly IMapper _mapper;
        private readonly ITranslationsRepository<Domain.Domain.DepartmentTip> _translationService;
        public DepartmentEnergyTipQueryHandler(ILogger<DepartmentEnergyTipQueryHandler> logger
            , IDepartmentTipRepository departmentTipRepository
            , IMapper mapper,
ILikeTipRepository likeTipRepository, ITranslationsRepository<Domain.Domain.DepartmentTip> translationService)
        {
            _departmentTipRepository = departmentTipRepository;
            _logger = logger;
            _mapper = mapper;
            _likeTipRepository = likeTipRepository;
            _translationService = translationService;
        }
        public async Task<DepartmentEnergyTipList> Handle(DepartmentEnergyTipQuery request, CancellationToken cancellationToken)
        {
            try
            {
                 var  departmentTips = await _departmentTipRepository.GetDepatrmentTipByListAsync(request.Id);           

                 var userFavourtieTipList = await _departmentTipRepository
                                                    .UserFavouriteDepartmentTipListAsync(request.UserEmail);
                 var likedTip = await _likeTipRepository.GetLikeTipListAsync(request.UserEmail);

              

                if (request.Language == "en-US")
                {
                    var translatedDepartmentTip = await _translationService.GetTranslatedDataAsync<Domain.Domain.DepartmentTip>(request.Language);

               
                }
             
                    var userDepartmentTips = departmentTips.Select(x => new DepartmentEnergyTipList
                    {
                        Id = x.EnergyAnalysisQuestions.Id,
                        EnergyAnalysisName = x.EnergyAnalysisQuestions.Name,
                        EnergyAnalysisDescription = x.EnergyAnalysisQuestions.Description,
                        DepartmentEnergyTips = _mapper.Map<List<DepartmentEnergyTip>>(departmentTips)
                    }).FirstOrDefault();
               
                //verify it one more time
                foreach (var Ut in userDepartmentTips.DepartmentEnergyTips)
                    {
                        foreach (var ft in userFavourtieTipList)
                        {
                            if (ft.DepartmentTipId == Ut.Id)
                            {
                                Ut.IsSelected = true;
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

                    return userDepartmentTips;
                

                
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(DepartmentEnergyTipQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(DepartmentEnergyTipQueryHandler));
                throw;
            }

        }
    }
}
