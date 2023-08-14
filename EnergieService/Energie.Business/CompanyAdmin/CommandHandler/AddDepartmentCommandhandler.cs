using Energie.Business.CompanyAdmin.Command;
using Energie.Business.SuperAdmin.Helper;
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

namespace Energie.Business.CompanyAdmin.CommandHandler
{
    public class AddDepartmentCommandhandler : IRequestHandler<AddDepartmentCommand, ResponseMessage>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<AddDepartmentCommandhandler> _logger;
        private readonly ICompanyAdminRepository _addCompanyAdminRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddDepartmentCommandhandler(IDepartmentRepository departmentRepository
            , ILogger<AddDepartmentCommandhandler> logger
            , ICompanyAdminRepository addCompanyAdminRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
            _addCompanyAdminRepository = addCompanyAdminRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var responseMessage = new ResponseMessage();
            try
            {
                var user = await _addCompanyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    var departments = await _departmentRepository.GetDepartmentByCompanyIdAsync((int)user.CompanyId);
                    if (departments.Contains(request.DepartmentName, StringComparer.OrdinalIgnoreCase))
                    {

                        //responseMessage.Message = request.DepartmentName +" "+_localizer["Bestaat al"].Value;
                        responseMessage.Message = _localizer["Already_exists",request.DepartmentName].Value;
                        _logger.LogError(responseMessage.Message, $"{{0}}");
                        return responseMessage;
                    }
                    var department = new Department().SetCompanyDepartment(request.DepartmentName, (int)user.CompanyId);
                    var departmentid = await _departmentRepository.AddDepartmentAsync(department);
                    //responseMessage.Message = request.DepartmentName +" "+ _localizer["met"].Value +" "+ departmentid +" "+ _localizer["Toegevoegd"].Value;
                    responseMessage.Message = _localizer["DepartmentName_with_ID_added",request.DepartmentName].Value;
                    responseMessage.IsSuccess = true;
                    responseMessage.Id = departmentid;
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error occured in {{0}}", ex.Message);
                _logger.LogError($"Er is een fout opgetreden in {{0}}", ex.Message);
                throw;
            }
        }
    }
}
