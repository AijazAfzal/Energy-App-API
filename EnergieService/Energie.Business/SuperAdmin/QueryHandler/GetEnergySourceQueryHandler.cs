using AutoMapper;
using Energie.Business.SuperAdmin.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.QueryHandler
{
    public class GetEnergySourceQueryHandler : IRequestHandler<GetEnergySourceQuery, EnergyAnalysisList>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetEnergySourceQueryHandler> _logger;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly ITranslationsRepository<Domain.Domain.EnergyAnalysis> _translationService;
        public GetEnergySourceQueryHandler(ILogger<GetEnergySourceQueryHandler> logger
            , IEnergyAnalysisRepository energyAnalysisRepository
            , IMapper mapper,
ITranslationsRepository<Domain.Domain.EnergyAnalysis> translationService)
        {
            _mapper = mapper;
            _logger = logger;
            _energyAnalysisRepository = energyAnalysisRepository;
            _translationService = translationService;
        }
        public async Task<EnergyAnalysisList> Handle(GetEnergySourceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var energySource = await _energyAnalysisRepository.GetAllEnergySourceAsync();

                
                var obj = new List<Domain.Domain.EnergyAnalysis>();

                obj.Add(energySource.Where(x => x.Name.Contains("Stress")).FirstOrDefault());
                obj.Add(energySource.Where(x => x.Name.Contains("Energie")).FirstOrDefault());
                obj.Add(energySource.Where(x => x.Name.Contains("kwetsbaarheden")).FirstOrDefault());
                obj.Add(energySource.Where(x => x.Name.Contains("hulpbronnen")).FirstOrDefault());
                
                energySource.Clear();
                if (request.Language == "en-US")
                {
                    obj.Clear();
                    var translatedEnergyAnalysis = await _translationService.GetTranslatedDataAsync<Domain.Domain.EnergyAnalysis>(request.Language);

                    obj.Add(translatedEnergyAnalysis.Where(x => x.Name == "Stress sources (unloaders)").FirstOrDefault());
                    obj.Add(translatedEnergyAnalysis.Where(x => x.Name == "Energy sources (chargers)").FirstOrDefault());
                    obj.Add(translatedEnergyAnalysis.Where(x => x.Name == "Personal vulnerabilities").FirstOrDefault());
                    obj.Add(translatedEnergyAnalysis.Where(x => x.Name == "Personal Resources").FirstOrDefault());
                }
                var respone = _mapper.Map<IList<Model.Response.EnergyAnalysis>>(obj);
                return new EnergyAnalysisList { EnergyAnalyses = respone };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(GetEnergySourceQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(GetEnergySourceQueryHandler));
                throw;
            }
        }
    }
}
