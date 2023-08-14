using AutoMapper;
using Energie.Business.Energie.Query;
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

namespace Energie.Business.Energie.QueryHandler
{
    public class UserEnergyAnalysisQueryHandler : IRequestHandler<UserEnergyAnalysisQuery, UserEnergyAnalysisRequestList>
    {
        private readonly ILogger<UserEnergyAnalysisQueryHandler> _logger;
        private readonly IClaimService _tokenClaimService;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;     
        private readonly ITranslationsRepository<Domain.Domain.UserEnergyAnalysis> _translationService;
        public UserEnergyAnalysisQueryHandler(ILogger<UserEnergyAnalysisQueryHandler> logger
            , IClaimService tokenClaimService
            , IEnergyAnalysisRepository energyAnalysisRepository
            , ITranslationsRepository<Domain.Domain.UserEnergyAnalysis> translationService)
        {
            _energyAnalysisRepository = energyAnalysisRepository;
            _logger = logger;
            _tokenClaimService = tokenClaimService;
            _translationService = translationService;
        }
        public async Task<UserEnergyAnalysisRequestList> Handle(UserEnergyAnalysisQuery request, CancellationToken cancellationToken)
        {
            var userEmail = _tokenClaimService.GetUserEmail();
            try
            {
                var userEnergyAnalysisQuestion = await _energyAnalysisRepository.UserEnergyAnalysisAsync(userEmail);
                var userEnergyAnalysis = new List<UserEnergyAnalysisRequest>();
                var uniqueuserEnergyAnalysis = userEnergyAnalysisQuestion
                    .Select(x => x.EnergyAnalysisQuestions.EnergyAnalysis.Id)
                    .Distinct()
                    .ToList();

                if (request.Language == "en-US")
                {
                    var translatedUserEnergyAnalysis = await _translationService.GetTranslatedDataAsync<Domain.Domain.UserEnergyAnalysis>(request.Language);

                    foreach (var item in uniqueuserEnergyAnalysis)
                    {


                        userEnergyAnalysis.Add(new UserEnergyAnalysisRequest
                        {
                            energyAnalysis = translatedUserEnergyAnalysis
                           .Where(x => x.EnergyAnalysisQuestions != null && x.EnergyAnalysisQuestions.EnergyAnalysis.Id == item)
                           .Select(x => x.EnergyAnalysisQuestions?.EnergyAnalysis?.Name)
                            .FirstOrDefault(),
                            energyAnalysisQuestions = userEnergyAnalysisQuestion
                          .Where(x => x.EnergyAnalysisQuestions.EnergyAnalysisID == item)
                         .Select(x => x.EnergyAnalysisQuestions.Name)

                         .ToArray()
                        });


                    }
                }
                else
                {
                    foreach (var item in uniqueuserEnergyAnalysis)
                    {
                        userEnergyAnalysis.Add(new UserEnergyAnalysisRequest
                        {
                            energyAnalysis = userEnergyAnalysisQuestion
                                .Where(x => x.EnergyAnalysisQuestions.EnergyAnalysis.Id == item)
                                .Select(x => x.EnergyAnalysisQuestions.EnergyAnalysis.Name)
                                .FirstOrDefault(),
                            energyAnalysisQuestions = userEnergyAnalysisQuestion
                                .Where(x => x.EnergyAnalysisQuestions.EnergyAnalysisID == item)
                                .Select(x => x.EnergyAnalysisQuestions.Name)
                                .ToArray(),
                        });
                    }
                }

                return new UserEnergyAnalysisRequestList { UserEnergyAnalysisRequest = userEnergyAnalysis };


            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error Occcured in {{0}}", userEmail, ex.Message);
                _logger.LogError($"fout opgetreden in {{0}}", userEmail, ex.Message);
                throw;
            }
        }
    }
}
