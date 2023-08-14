using Energie.Business.CompanyAdmin.Command;
using Energie.Business.IServices;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.CommandHandler
{
    public class AddMultipleUserCommandListHandler : IRequestHandler<AddMultipleUserCommandList, ApiResponse>
    {
        private readonly ILogger<AddCompanyUserCommandHandler> _logger; 
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ICreateCompanyUserRepository _createcompanyUserRepository;
        public AddMultipleUserCommandListHandler(ILogger<AddCompanyUserCommandHandler> logger,
            ICreateCompanyUserRepository createcompanyUserRepository,
            ICompanyUserRepository companyUserRepository,
            IStringLocalizer<Resources.Resources> localizer
            )
        {
            _logger = logger;
            _localizer = localizer;
            _companyUserRepository = companyUserRepository;
            _createcompanyUserRepository = createcompanyUserRepository; 

        }

        public async Task<ApiResponse> Handle(AddMultipleUserCommandList request, CancellationToken cancellationToken)
        {
            try
            {
                int noofrecordsinexcel= request.AddMultipleUserCommands.Count; //total records in excel file
                int count = 0;
                foreach (var item in request.AddMultipleUserCommands)
                {
                    var databasedata = await _companyUserRepository.GetCompanyUserAsync(item.Email);
                    if (databasedata == null)
                    {

                        var adduser = new B2CCompanyUser().SetCompanyUser(item.Name, item.Email, item.DepartmentId);
                        await _createcompanyUserRepository.CreateCompanyUserAsync(adduser);
                        count++; 
                    }
                  
                }
                await _createcompanyUserRepository.SaveChangesAsync();
                int recordsnotadded = noofrecordsinexcel - count; 
                if (count == noofrecordsinexcel) //if all users in excel are added
                {
                    return ApiResponse.ResponseMessages(1, true, $" {count} Records added successfully");
                }
                else
                {
                    return ApiResponse.ResponseMessages(1, true, $" {count} Records added successfully while {recordsnotadded} records already existed"); 
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddMultipleUserCommandListHandler)); 
                throw;
            }
        }
    }
}
