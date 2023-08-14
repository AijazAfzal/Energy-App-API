using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddMonthlyEnergyScoreCommandHandler : IRequestHandler<AddMonthlyEnergyScoreCommand, ResponseMessage>
    {
        private readonly IClaimService _tokenClaimService;
        public readonly ILogger<AddMonthlyEnergyScoreCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IEnergyScoreRepository _energyScoreRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddMonthlyEnergyScoreCommandHandler(IClaimService tokenClaimService
            , ILogger<AddMonthlyEnergyScoreCommandHandler> logger
            , ICompanyUserRepository companyUserRepository
            , IEnergyScoreRepository energyScoreRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _tokenClaimService = tokenClaimService;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _energyScoreRepository = energyScoreRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddMonthlyEnergyScoreCommand request, CancellationToken cancellationToken)
        {
            ResponseMessage responseMessage = new();
            var userEmail = _tokenClaimService.GetUserEmail();
            try
            {
                var user = await _companyUserRepository.GetCompanyUserAsync(userEmail);
                var existEnergyScore = await _energyScoreRepository.GetCompanyUserEnergyScore(user.Id);
                DateTime datetime = DateTime.Now;
                int presentmonth = datetime.Month;
                int presentyear = datetime.Year;
                if (existEnergyScore == null)
                {
                    var setEnergyScore = new EnergyScore().SetEnergyScore(request.EnergyScore, user.Id);
                    var energyScore = await _energyScoreRepository.AddEnergyScoreAsync(setEnergyScore);
                    if (energyScore != null)
                    {
                        
                        responseMessage.Message = _localizer["Score_added"].Value; 
                        responseMessage.IsSuccess = true;
                        return responseMessage;
                    }
                    else
                    {                        
                        _logger.LogError($"Er is een fout opgetreden in {{0}}{userEmail}");
                        return responseMessage;
                    }
                }
                else if (presentmonth == existEnergyScore.MonthId && presentyear == existEnergyScore.Year)
                {
                    responseMessage.Message = _localizer["Score_exists_for_this_month"].Value;
                    _logger.LogError($"Er is een fout opgetreden in{userEmail}, {presentmonth},{existEnergyScore.MonthId},{presentyear},{existEnergyScore.Year}"); 
                    return responseMessage;

                }
                else
                {
                    var setEnergyScore = new EnergyScore().SetEnergyScore(request.EnergyScore, user.Id);
                    var energyScore = await _energyScoreRepository.AddEnergyScoreAsync(setEnergyScore);
                    if (energyScore != null)
                    {
                       
                        responseMessage.Message = _localizer["Score_added"].Value;

                        responseMessage.IsSuccess = true;
                        return responseMessage;
                    }
                    else
                    {
                       
                        responseMessage.Message = _localizer["Error_occurred"].Value;
                        return responseMessage;
                    }
                }
            }
            catch (Exception ex)
            {                
                _logger.LogError($"Er is een fout opgetreden in {{0}}", ex.Message); 
                return default;
            }

        }
    }
}
