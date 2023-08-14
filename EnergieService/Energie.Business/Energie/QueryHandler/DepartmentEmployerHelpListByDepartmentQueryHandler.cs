using AutoMapper;
using Energie.Business.Energie.Query;
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

namespace Energie.Business.Energie.QueryHandler
{
    public class DepartmentEmployerHelpListByDepartmentQueryHandler : IRequestHandler<DepartmentEmployerHelpListByDepartmentQuery, DepartmentEmployerHelpList>
    {
        private readonly ILogger<DepartmentEmployerHelpListByDepartmentQueryHandler> _logger;
        private readonly ICompanyHelpCategoryRepository _companyHelpCategoryRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IDepartmentTipRepository _departmentTipRepository;      
        private readonly ITranslationsRepository<CompanyDepartmentHelp> _translationService;
        private readonly IMapper _mapper;
        public DepartmentEmployerHelpListByDepartmentQueryHandler(ILogger<DepartmentEmployerHelpListByDepartmentQueryHandler> logger
            , ICompanyHelpCategoryRepository companyHelpCategoryRepository
            , ICompanyUserRepository companyUserRepository
            , IMapper mapper
            , IDepartmentTipRepository departmentTipRepository
            , ITranslationsRepository<CompanyDepartmentHelp> translationService)
        {
            _companyHelpCategoryRepository = companyHelpCategoryRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _mapper = mapper;
            _departmentTipRepository = departmentTipRepository;
            _translationService = translationService;
        }
        public async Task<DepartmentEmployerHelpList> Handle(DepartmentEmployerHelpListByDepartmentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var employerHelpList = await _companyHelpCategoryRepository.EmployerHelpByDepartmentIdAsync((int)user.DepartmentID, request.Id);
                var companyCategory = employerHelpList.Select(x => x.HelpCategory).FirstOrDefault();
                var empfavHelp = await _departmentTipRepository
                                                    .DepartmentFavouriteHelpListAsync(request.UserEmail);


                if (request.Language == "en-US")
                {
                    var translatedDepartmentHelp = await _translationService.GetTranslatedDataAsync<CompanyDepartmentHelp>(request.Language);

                    var list = employerHelpList.Select(
                   x => new DepartmentEmployerHelpList
                   {
                       Id = companyCategory.Id,
                       CompanyHelpCategoryName = companyCategory.Name,
                       Description = companyCategory.Description,
                       ImageUrl = companyCategory.ImageUrl,
                       DepartmentEmployerHelps = _mapper.Map<IList<DepartmentEmployerHelp>>(employerHelpList)
                   }).FirstOrDefault();

                    if (list != null)
                    {
                        foreach (var Ut in list.DepartmentEmployerHelps)
                        {
                            foreach (var ft in empfavHelp)
                            {
                                if (ft.CompanyDepartmentHelpId == Ut.Id)
                                {
                                    Ut.IsSelected = true;
                                }
                            }
                        }
                    }
               
                    return list;
                }
                else
                {
                    var list = employerHelpList.Select(
                        x => new DepartmentEmployerHelpList
                        {
                            Id = companyCategory.Id,
                            CompanyHelpCategoryName = companyCategory.Name,
                            Description = companyCategory.Description,
                            ImageUrl = companyCategory.ImageUrl,
                            DepartmentEmployerHelps = _mapper.Map<IList<DepartmentEmployerHelp>>(employerHelpList)
                        }).FirstOrDefault();

                    if (list != null)
                    {
                        foreach (var Ut in list.DepartmentEmployerHelps)
                        {
                            foreach (var ft in empfavHelp)
                            {
                                if (ft.CompanyDepartmentHelpId == Ut.Id)
                                {
                                    Ut.IsSelected = true;
                                }
                            }
                        }
                    }
                   
                    return list;
                }


            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"error occured in {{0}}", nameof(DepartmentEmployerHelpListByDepartmentQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(DepartmentEmployerHelpListByDepartmentQueryHandler)); 
                throw;
            }
        }
    }
}
