using Energie.Business.Energie.Command;
using Energie.Business.SuperAdmin.Helper;
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

namespace Energie.Business.Energie.CommandHandler
{
    public class RemoveUserAccountCommandHandler : IRequestHandler<RemoveUserAccountCommand, ResponseMessage>
    {
        private readonly ILogger<RemoveUserAccountCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ICreateCompanyUserRepository _createCompanyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public RemoveUserAccountCommandHandler(ILogger<RemoveUserAccountCommandHandler> logger,
                                                ICompanyUserRepository companyUserRepository,
                                                ICreateCompanyUserRepository createCompanyUserRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _createCompanyUserRepository = createCompanyUserRepository; 
            _localizer = localizer;

        }
        public async Task<ResponseMessage> Handle(RemoveUserAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responsemessage = new ResponseMessage();
                var companyuser = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail); 
                if (companyuser == null)
                {
                    return default;
                }
                else
                {
                    await _createCompanyUserRepository.DeleteUserAccountAsync(companyuser.Id);
                    responsemessage.IsSuccess = true;
                    responsemessage.Id = companyuser.Id;
                     responsemessage.Message = _localizer["User_successfully_removed", companyuser.Id].Value;  
                    return responsemessage;
                }
            }
            catch (Exception ex)
            {              
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(RemoveUserAccountCommandHandler)); 
                throw;
            }


        }
    }
}
