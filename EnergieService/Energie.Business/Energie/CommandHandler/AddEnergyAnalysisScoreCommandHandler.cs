using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddEnergyAnalysisScoreCommandHandler : IRequestHandler<AddEnergyAnalysisScoreCommand, UserEnergyAnalysisResponseList>
    {
        private readonly ILogger<AddEnergyAnalysisScoreCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddEnergyAnalysisScoreCommandHandler(ILogger<AddEnergyAnalysisScoreCommandHandler> logger
            , ICompanyUserRepository companyUserRepository
            , IEnergyAnalysisRepository energyAnalysisRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _energyAnalysisRepository = energyAnalysisRepository;
            _localizer = localizer;
        }
        public async Task<UserEnergyAnalysisResponseList> Handle(AddEnergyAnalysisScoreCommand request, CancellationToken cancellationToken)
        {

            try
            {
               var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
               if (user.Id == 0 || user == null)
               {
                   
                   string localizationMessage = _localizer["Something_wrong"].Value;
                   _logger.LogInformation(localizationMessage); 
                    return default;
               }
               else
               {
                   var checkRecord = await _energyAnalysisRepository.GetUserEnergyAnalysisAsync(user.Id);
                   if (checkRecord != null)
                    {
                       await _energyAnalysisRepository.DeleteEnergyAnalysis((int)checkRecord.CompanyUserID);

                    }
                   foreach (int i in request.EnergyAnalysisRecord)
                   {
                       var userEnergieAnalysis = new UserEnergyAnalysis().SetUserEnergyAnalysis(i, user.Id);
                       await _energyAnalysisRepository.SetEnergyAnalysis(userEnergieAnalysis);
                   }
                   await _energyAnalysisRepository.SaveChangesForEnergyAsync();
              
                   var userEnergyAnalysisQuestion = await _energyAnalysisRepository.UserEnergyAnalysisAsync(request.UserEmail);
                   var userEnergyAnalysis = new List<UserEnergyAnalysisResponse>();
              
                   var uniqueuserEnergyAnalysis = userEnergyAnalysisQuestion.Select(x => x.EnergyAnalysisQuestions.EnergyAnalysis.Id).Distinct().ToList();
                   foreach (var item in uniqueuserEnergyAnalysis)
                   {
                       userEnergyAnalysis.Add(new UserEnergyAnalysisResponse
                       {
                           energyAnalysis = userEnergyAnalysisQuestion.Where(x => x.EnergyAnalysisQuestions.EnergyAnalysis.Id == item).Select(x => x.EnergyAnalysisQuestions.EnergyAnalysis.Name).FirstOrDefault(),
                           EnergyAnalysisQuestionsResponse = userEnergyAnalysisQuestion.
                           Where(x => x.EnergyAnalysisQuestions.EnergyAnalysisID == item)
                           .Select(x => new EnergyAnalysisQuestion
                           {
                               EnergyAnalysis = x.EnergyAnalysisQuestions.Name,
                               Id = x.EnergyAnalysisQuestions.Id,
                               IsSelected = true
                           }).ToList(),
                       });
                   }
                   return new UserEnergyAnalysisResponseList { EnergyAnalysisResponse = userEnergyAnalysis };
               }
              
              
            
            }
            catch (Exception ex)
            {           
                _logger.LogError($"Er is een fout opgetreden in {{0}}", ex.Message); 
                throw;
            }

        }
    }
}
