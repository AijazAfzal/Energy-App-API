using AutoMapper;
using Energie.Business.Energie.Query;
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

namespace Energie.Business.Energie.QueryHandler
{
    public class GetUserEnergyTipQueryHandler : IRequestHandler<GetUserEnergyTipQuery, UserEnergyTipList>
    {
        private readonly ILogger<GetUserEnergyTipQueryHandler> _logger;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly IMapper _mapper;
        private readonly ITranslationsRepository<Domain.Domain.Tip> _translationService; 
        public GetUserEnergyTipQueryHandler(ILogger<GetUserEnergyTipQueryHandler> logger
            , IUserEnergyTipRepository userEnergyTipRepository
            , ICategoryRepository categoryRepository,
            IEnergyAnalysisRepository energyAnalysisRepository,
            IMapper mapper,
            ITranslationsRepository<Domain.Domain.Tip> translationService)
        {
            _logger = logger;
            _userEnergyTipRepository = userEnergyTipRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _energyAnalysisRepository = energyAnalysisRepository;
            _translationService = translationService;
        }
        public async Task<UserEnergyTipList> Handle(GetUserEnergyTipQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var energyAnalysisQuestion = await _energyAnalysisRepository.EnergyAnalysisQuestionsAsyncById(request.id);
                var tips = await _userEnergyTipRepository.GetUserEnergyTip(energyAnalysisQuestion.Id);
                var favouriteTip = await _userEnergyTipRepository.GetUserFavoritesAsync(request.UserEmail);

                if (request.Language == "en-US")
                {
                    var translatedTip = await _translationService.GetTranslatedDataAsync<Tip>(request.Language);

                    var userEnergyTipList = new UserEnergyTipList
                    {
                        Id = energyAnalysisQuestion.Id,
                        CategoryName = energyAnalysisQuestion.Name,
                        Description = energyAnalysisQuestion.Description,
                        UserEnergyTip = _mapper.Map<List<UserEnergyTip>>(tips)

                    };

                    //Is going to modified 
                    foreach (var Ut in userEnergyTipList.UserEnergyTip)
                    {
                        foreach (var ft in favouriteTip.Item1)
                        {
                            if (ft.TipId == Ut.Id)
                            {
                                Ut.IsSelected = true;
                            }
                        }
                    }

                    return userEnergyTipList;


                }


                else
                {

                    var userEnergyTipList = new UserEnergyTipList
                    {
                        Id = energyAnalysisQuestion.Id,
                        CategoryName = energyAnalysisQuestion.Name,
                        Description = energyAnalysisQuestion.Description,
                        UserEnergyTip = _mapper.Map<List<UserEnergyTip>>(tips)
                    };

                    //Is going to modified 
                    foreach (var Ut in userEnergyTipList.UserEnergyTip)
                    {
                        foreach (var ft in favouriteTip.Item1)
                        {
                            if (ft.TipId == Ut.Id)
                            {
                                Ut.IsSelected = true;
                            }
                        }
                    }

                    return userEnergyTipList;
                }



            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error Occcured in {{0}}", ex.Message);
                _logger.LogError($"fout opgetreden in {{0}}", ex.Message); 
                throw;
            }
            //throw new NotImplementedException();
        }
    }
}
