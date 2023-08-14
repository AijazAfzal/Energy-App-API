using AutoMapper;
using Energie.Business.SuperAdmin.Query;
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

namespace Energie.Business.SuperAdmin.QueryHandler
{
    public class CategoryListQueryHandler : IRequestHandler<CategoryListQuery, CategoryList>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryListQueryHandler> _logger;
        public readonly ICategoryRepository _categoryRepository;
        private readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        private readonly ITranslationsRepository<UserEnergyAnalysis> _translationService;
        public CategoryListQueryHandler(IMapper mapper
            , ILogger<CategoryListQueryHandler> logger
            , ICategoryRepository categoryRepository,
             IEnergyAnalysisRepository energyAnalysisRepository,
             ITranslationsRepository<UserEnergyAnalysis> translationService)
        {
            _mapper = mapper;
            _logger = logger;
            _categoryRepository = categoryRepository;
            _energyAnalysisRepository = energyAnalysisRepository;
            _translationService = translationService;
        }

        public async Task<CategoryList> Handle(CategoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var userEnergyAnalysisQuestion = await _energyAnalysisRepository.UserEnergyAnalysisAsync(request.UserEmail);
                List<UserEnergyAnalysis> translatedData = null;

                if (request.Language == "en-US")
                {
                    translatedData = await _translationService.GetTranslatedDataAsync<UserEnergyAnalysis>(request.Language);
                }

                var list = userEnergyAnalysisQuestion.Select(x => new Model.Request.Category
                {
                    Id = (int)x.EnergyAnalysisQuestionsID,
                    Name = (translatedData != null) ? translatedData.Find(y => y.EnergyAnalysisQuestions != null && y.EnergyAnalysisQuestions.Id == x.EnergyAnalysisQuestionsID)?.EnergyAnalysisQuestions.Name ?? x.EnergyAnalysisQuestions.Name : x.EnergyAnalysisQuestions.Name,
                    CreatedDate = x.CreatedOn.Date,
                    Description = (translatedData != null) ? translatedData.Find(y => y.EnergyAnalysisQuestions != null && y.EnergyAnalysisQuestions.Id == x.EnergyAnalysisQuestionsID)?.EnergyAnalysisQuestions.Description ?? x.EnergyAnalysisQuestions.Description : x.EnergyAnalysisQuestions.Description,
                    ImageUrl = x.EnergyAnalysisQuestions?.ImageUrl
                })
            .ToList();

                return new CategoryList { Categories = list };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Er is een fout opgetreden in {{0}}, CategoryListQueryHandler ", ex.Message);
                throw;
            }
        }
    }
}
