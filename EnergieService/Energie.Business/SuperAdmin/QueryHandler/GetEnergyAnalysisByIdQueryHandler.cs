using AutoMapper;
using Energie.Business.SuperAdmin.Query;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.SuperAdmin.QueryHandler
{
    public class GetEnergyAnalysisByIdQueryHandler : IRequestHandler<GetEnergyAnalysisByIdQuery, ListEnergyAnalysisQuestions>
    {
        private readonly ILogger<GetEnergyAnalysisByIdQueryHandler> _logger;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly IMapper _mapper;
        private readonly ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> _translationsService;
        public GetEnergyAnalysisByIdQueryHandler(ILogger<GetEnergyAnalysisByIdQueryHandler> logger
            , IEnergyAnalysisRepository energyAnalysisRepository,
IMapper mapper,
ITranslationsRepository<Domain.Domain.EnergyAnalysisQuestions> translationsService)
        {
            _logger = logger;
            _energyAnalysisRepository = energyAnalysisRepository;
            _mapper = mapper;
            _translationsService = translationsService;
        }
        public async Task<ListEnergyAnalysisQuestions> Handle(GetEnergyAnalysisByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var energyAnalysisList = await _energyAnalysisRepository.GetAnalysisQuestionsByIdAsync(request.Id);

                if(request.Language == "en-US")
                {
                    var energyAnalysisQuestions = await _translationsService.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysisQuestions>(request.Language);
                  

                }
                var energyTipList = _mapper.Map<List<Model.Response.EnergyAnalysisQuestions>>(energyAnalysisList);
                return new ListEnergyAnalysisQuestions
                {
                    EnergyAnalysisQuestion = energyTipList
                };

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(CategoryListQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(CategoryListQueryHandler));
                throw;
            }
        }
    }
}
