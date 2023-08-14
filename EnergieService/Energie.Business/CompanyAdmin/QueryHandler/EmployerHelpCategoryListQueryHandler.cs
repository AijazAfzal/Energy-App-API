using AutoMapper;
using Energie.Business.CompanyAdmin.Query;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.QueryHandler
{
    public class EmployerHelpCategoryListQueryHandler : IRequestHandler<EmployerHelpCategoryListQuery, EmployerHelpCategoryList>
    {
        private readonly ICompanyHelpCategoryRepository _companyHelpCategoryRepository;
        private readonly ILogger<EmployerHelpCategoryListQueryHandler> _logger;
        private readonly IMapper _mapper;
        // private readonly TranslationService<Domain.Domain.HelpCategory> _translationService;
        private readonly ITranslationsRepository<Domain.Domain.HelpCategory> _translationService;

        public EmployerHelpCategoryListQueryHandler(ICompanyHelpCategoryRepository companyHelpCategoryRepository,
            ILogger<EmployerHelpCategoryListQueryHandler> logger
            , IMapper mapper
            ,//TranslationService<Domain.Domain.HelpCategory> translationService
             ITranslationsRepository<Domain.Domain.HelpCategory> translationService)
        {
            _companyHelpCategoryRepository = companyHelpCategoryRepository;
            _logger = logger;
            _mapper = mapper;
            _translationService = translationService;
        }
        public async Task<EmployerHelpCategoryList> Handle(EmployerHelpCategoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var categoryList = await _companyHelpCategoryRepository.GetEmployerHelpCategoriesAsync();

                if (request.Language == "en-US")
                {
                    var translatedHelpCategory = await _translationService.GetTranslatedDataAsync<Domain.Domain.HelpCategory>(request.Language);
                    return new EmployerHelpCategoryList
                    {
                        EmployerHelpCategories = _mapper.Map<List<EmployerHelpCategory>>(translatedHelpCategory)
                    };
                }
                else
                {
                    return new EmployerHelpCategoryList
                    {
                        EmployerHelpCategories = _mapper.Map<List<EmployerHelpCategory>>(categoryList)
                    };
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"error occured in {{0}}", nameof(EmployerHelpCategoryListQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(EmployerHelpCategoryListQueryHandler));
                throw;
            }
        }


    }
}
