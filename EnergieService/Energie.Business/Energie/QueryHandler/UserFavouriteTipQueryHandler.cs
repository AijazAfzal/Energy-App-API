using AutoMapper.Configuration.Conventions;
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
    public class UserFavouriteTipQueryHandler : IRequestHandler<UserFavouriteTipQuery, UserFavouriteTipRequestList>
    {
        private readonly ILogger<UserFavouriteTipQueryHandler> _logger;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly ITranslationsRepository<Domain.Domain.UserFavouriteTip> _translationService;
        private readonly ITranslationsRepository<Domain.Domain.UserTip> _translation;
        private readonly ITranslationsRepository<Domain.Domain.UserFavouriteHelp> _translationHelp;

        public UserFavouriteTipQueryHandler(IUserEnergyTipRepository userEnergyTipRepository
            , ILogger<UserFavouriteTipQueryHandler> logger,
IEnergyAnalysisRepository energyAnalysisRepository, ITranslationsRepository<Domain.Domain.UserFavouriteTip> translationService, ITranslationsRepository<Domain.Domain.UserTip> translation, ITranslationsRepository<Domain.Domain.UserFavouriteHelp> translationHelp)
        {
            _logger = logger;
            _userEnergyTipRepository = userEnergyTipRepository;
            _energyAnalysisRepository = energyAnalysisRepository;
            _translationService = translationService;   
            _translation = translation;
            _translationHelp = translationHelp;
        }
        public async Task<UserFavouriteTipRequestList> Handle(UserFavouriteTipQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userEnergyAnalysisQuestion = await _energyAnalysisRepository.UserEnergyAnalysisAsync(request.UserEmail);
                var superAdminTip = await _userEnergyTipRepository.GetUserFavoritesAsync(request.UserEmail);
                
                var tipsQuestion = superAdminTip.Item1.Select(x => x.Tips.EnergyAnalysisQuestionsId.ToString()).Distinct().ToList();
                var userAnalysisQuestion = userEnergyAnalysisQuestion.Select(x=> x.EnergyAnalysisQuestionsID.ToString()).Distinct().ToList();

                foreach(var t in tipsQuestion)
                {
                    if(userAnalysisQuestion.Contains(t))
                    {
                        int d = 0;
                    }
                    else
                    {
                        var test = superAdminTip.Item1.Where(x => x.Tips.EnergyAnalysisQuestionsId == Convert.ToInt32(t))
                                                .Select(x => x.Id)
                                                .ToList();
                        foreach (var r in test)
                        {
                            await _userEnergyTipRepository.RemoveUserFavouriteTipAsync(r);
                        }
                    }
                }
                var savechanges = await _energyAnalysisRepository.SaveChangesForEnergyAsync();

                var getallList = await _userEnergyTipRepository.GetUserFavoritesAsync(request.UserEmail);


                var userFavouriteTips = getallList.Item1.Select
                                    (x => new Model.Request.UserFavouriteTipRequest
                                    {
                                        Id = x.Id,
                                        Name = x.Tips.Name,
                                        Description = x.Tips.Description,
                                        AddedDate = x.CreatedOn.Date, 
                                        TipBy ="SuperAdmin"
                                    }).ToList();

                var tip = getallList.Item2.Select(x => new Model.Request.UserFavouriteTipRequest
                {
                    Id = x.Id,
                    Name = x.CompanyHelp.Name,
                    Description = x.CompanyHelp.Description,
                    AddedDate = x.CreatedOn.Date,  
                    TipBy = "CompanyAdmin"
                }).ToList();
                    

                var userTip = getallList.Item3.Select
                    (x => new Model.Request.UserFavouriteTipRequest
                    {
                        Id = x.Id,
                        Name = x.EnergyAnalysisQuestions.Name,
                        Description = x.Description,
                        AddedDate = x.CreatedOn.Date,
                        TipBy = "User"
                    }).ToList();
                userFavouriteTips.AddRange(tip);
                userFavouriteTips.AddRange(userTip);


                if (request.Language == "en-US")
                {
                    
                    var translatedFavouriteTip = await _translationService.GetTranslatedDataAsync<Domain.Domain.UserFavouriteTip>(request.Language);
                    var translated = await _translation.GetTranslatedDataAsync<Domain.Domain.UserTip>(request.Language);
                    var translatedHelp = await _translationHelp.GetTranslatedDataAsync<Domain.Domain.UserFavouriteHelp>(request.Language);

                    var translatedUserFavouriteTips = userFavouriteTips.Select(t =>
                    {
                        var translatedName = string.Empty;
                        var translatedDescription = string.Empty;

                        if (t.TipBy == "User")
                        {
                           
                            translatedName = translated.FirstOrDefault(tf => tf.Id == t.Id)?.EnergyAnalysisQuestions?.Name;
                            translatedDescription = t.Description;
                        }
                        else if(t.TipBy == "CompanyAdmin")
                        {
                            translatedName = translatedHelp.FirstOrDefault(tf => tf.Id == t.Id)?.CompanyHelp.Name;
                            translatedDescription = translatedHelp.FirstOrDefault(tf => tf.Id == t.Id)?.CompanyHelp.Description;
                        }
                        else
                        {
                           
                            translatedName = translatedFavouriteTip.FirstOrDefault(tf => tf.Id == t.Id)?.Tips?.Name;
                            translatedDescription = translatedFavouriteTip.FirstOrDefault(tf => tf.Id == t.Id)?.Tips?.Description;
                        }

                        return new Model.Request.UserFavouriteTipRequest
                        {
                            Id = t.Id,
                            Name = translatedName,
                            Description = translatedDescription,
                            AddedDate = t.AddedDate,
                            TipBy = t.TipBy
                        };
                    }).ToList();





                    return new UserFavouriteTipRequestList { UserFavouriteTipsRequests = translatedUserFavouriteTips };
                }

                return new UserFavouriteTipRequestList { UserFavouriteTipsRequests = userFavouriteTips };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}");
                _logger.LogError(ex, $"fout opgetreden in {{0}}"); 
                throw;
            }
            
        }
    }
}
