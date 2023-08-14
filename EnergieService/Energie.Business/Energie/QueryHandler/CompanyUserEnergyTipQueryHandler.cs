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
    public class CompanyUserEnergyTipQueryHandler : IRequestHandler<CompanyUserEnergyTipQuery, CompanyUserEnergyTipList>
    {
        private readonly ICompanyHelpRepository _companyHelpTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ILogger<CompanyUserEnergyTipQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserEnergyTipRepository _userEnergyTipRepository;
        //private readonly TranslationService<CompanyHelp> _translationService;
        private readonly ITranslationsRepository<CompanyHelp> _translationService;
        public CompanyUserEnergyTipQueryHandler(ICompanyHelpRepository companyHelpTipRepository
            , ICompanyUserRepository companyUserRepository
            , ILogger<CompanyUserEnergyTipQueryHandler> logger
            , IMapper mapper
            , IUserEnergyTipRepository userEnergyTipRepository,
              ITranslationsRepository<CompanyHelp> translationService)
        {
            _companyHelpTipRepository = companyHelpTipRepository;
            _companyUserRepository = companyUserRepository;
            _logger = logger;
            _mapper = mapper;
            _userEnergyTipRepository = userEnergyTipRepository;
            _translationService = translationService;
        }
        public async Task<CompanyUserEnergyTipList> Handle(CompanyUserEnergyTipQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var companyHelpTip = await _companyHelpTipRepository.GetCompanyHelpsByCategoryIdAsync(request.companyCategoryId);

                var companyCategory = companyHelpTip.Where(x => x.HelpCategoryId == request.companyCategoryId).FirstOrDefault();
                var companyHelp = companyHelpTip.Where(x => x.HelpCategoryId == request.companyCategoryId).ToList();

                var employerTip = await _userEnergyTipRepository.GetUserFavoritesAsync(request.UserEmail);



                if (request.Language == "en-US")
                {
                    var translatedHelpCategory = await _translationService.GetTranslatedDataAsync<CompanyHelp>(request.Language);
                    var userEnergyTipList = new CompanyUserEnergyTipList
                    {
                        Id = companyCategory.HelpCategoryId,
                        CompanyHelpCategoryName = companyCategory.HelpCategory.Name,
                        Description = companyCategory.HelpCategory.Description,
                        ImageUrl = companyCategory.HelpCategory.ImageUrl,
                        CompanyUserEnergyTips = _mapper.Map<List<CompanyUserEnergyTip>>(companyHelp)
                    };

                    //Is going to modified 
                    foreach (var Ut in userEnergyTipList.CompanyUserEnergyTips)
                    {
                        foreach (var ft in employerTip.Item2)
                        {
                            if (ft.CompanyHelpID == Ut.Id)
                            {
                                Ut.IsSelected = true;
                            }
                        }
                    }
                    return userEnergyTipList;


                }
                else
                {
                    var userEnergyTipList = new CompanyUserEnergyTipList
                    {
                        Id = companyCategory.HelpCategoryId,
                        CompanyHelpCategoryName = companyCategory.HelpCategory.Name,
                        Description = companyCategory.HelpCategory.Description,
                        ImageUrl = companyCategory.HelpCategory.ImageUrl,
                        CompanyUserEnergyTips = _mapper.Map<List<CompanyUserEnergyTip>>(companyHelp)
                    };

                    //Is going to modified 
                    foreach (var Ut in userEnergyTipList.CompanyUserEnergyTips)
                    {
                        foreach (var ft in employerTip.Item2)
                        {
                            if (ft.CompanyHelpID == Ut.Id)
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
                 _logger.LogError(ex, $"error occured in {{0}}", nameof(CompanyUserEnergyTipQueryHandler));                
                throw;
            }
        }
    }
}
