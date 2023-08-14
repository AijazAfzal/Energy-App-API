using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class GetAllEnergyAnalysisQueryHandler : IRequestHandler<GetAllEnergyAnalysisQuery, ListEnergyAnalysisQuestions>
    {
        private readonly ILogger<GetAllEnergyAnalysisQueryHandler> _logger;
        private readonly IClaimService _tokenClaimService;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> _translationService;
        private readonly IMapper _mapper;
        public GetAllEnergyAnalysisQueryHandler(ILogger<GetAllEnergyAnalysisQueryHandler> logger
            , IClaimService tokenClaimService
            , IEnergyAnalysisRepository energyAnalysisRepository
            , IMapper mapper
            , ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> translationService)
        {
            _logger = logger;
            _tokenClaimService = tokenClaimService;
            _energyAnalysisRepository = energyAnalysisRepository;
            _mapper = mapper;
            _translationService = translationService;   
        }
        public async Task<ListEnergyAnalysisQuestions> Handle(GetAllEnergyAnalysisQuery request, CancellationToken cancellationToken)
        {
            
            try
            {
                var energyAnalysisQuestion = await _energyAnalysisRepository.GetAllEnergyAnalysisQuestions();
                var userEnergyAnalysisQuestion = await _energyAnalysisRepository.UserEnergyAnalysisAsync(request.UserEmail);

                
                var mapEnergyAnalysisQuestion = _mapper.Map<List<EnergyAnalysisQuestions>>(energyAnalysisQuestion);

                var listEnergyAnalysisQuestions =  new ListEnergyAnalysisQuestions();
                listEnergyAnalysisQuestions.EnergyAnalysisQuestion =  mapEnergyAnalysisQuestion;

                if (request.language == "en-US")
                {
                    var translatedEnergyAnalysisQuestions = await _translationService.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>(request.language);

                    listEnergyAnalysisQuestions.EnergyAnalysisQuestion = _mapper.Map<List<EnergyAnalysisQuestions>>(translatedEnergyAnalysisQuestions);

                }
                // two for loop not needed 
                foreach (var item in listEnergyAnalysisQuestions.EnergyAnalysisQuestion)
                {
                    foreach(var i in userEnergyAnalysisQuestion)
                    {
                        if(i.EnergyAnalysisQuestionsID == item.Id)
                        {
                            item.IsSelected= true;
                        }
                    }
                }
                return listEnergyAnalysisQuestions;
                
            }
            catch (Exception ex)
            {
               // _logger.LogError($"Error occured in {{0}}", userEmail, ex.Message);
                _logger.LogError($"fout opgetreden in {{0}}", request.UserEmail, ex.Message); 
                throw;
            }
        }
    }
}
