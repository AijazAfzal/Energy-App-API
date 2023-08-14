using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class SetDepartmentfavouriteHelpQueryHandler : IRequestHandler<SetDepartmentfavouriteHelpQuery, ResponseMessage>
    {
        private readonly ILogger<SetDepartmentfavouriteHelpQueryHandler> _logger;
        private readonly ICompanyHelpCategoryRepository _companyHelpCategoryRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public SetDepartmentfavouriteHelpQueryHandler(ILogger<SetDepartmentfavouriteHelpQueryHandler> logger
            , ICompanyHelpCategoryRepository companyHelpCategoryRepository
            , ICompanyUserRepository companyUserRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _companyHelpCategoryRepository= companyHelpCategoryRepository;
            _companyUserRepository= companyUserRepository;
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(SetDepartmentfavouriteHelpQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var setDepartmentHelp = new DepartmentFavouriteHelp().SetDepartmentFavouriteHelp(request.Id, user.Id);
                var departmenthelp = await _companyHelpCategoryRepository.GetDepartmentFavouriteForUserAsync(request.Id, user.Id);
                if(departmenthelp != null)
                {
                    await _companyHelpCategoryRepository.DeleteDepartmentFavouriteForUserAsync(request.Id, user.Id);
                    response.Message = _localizer["FavoriteHelp_Removed"].Value; 
                    response.Id = request.Id;
                    return response;
                }
                await _companyHelpCategoryRepository.AddDepartmentFavouriteHelpAsync(setDepartmentHelp);
                response.IsSuccess = true;
                response.Message = _localizer["FavoriteHelp_added"].Value  ; 
                response.Id= setDepartmentHelp.Id;
                return response;
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
