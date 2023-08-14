using Energie.Business.CompanyAdmin.Command;
using Energie.Business.SuperAdmin.CommandHandler;
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
    public class AddCompanyUserCommandHandler : IRequestHandler<AddCompanyUserCommand, ResponseMessage>
    {
        private readonly ILogger<AddCompanyUserCommandHandler> _logger;
        private readonly ICreateCompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddCompanyUserCommandHandler(ILogger<AddCompanyUserCommandHandler> logger,
            ICreateCompanyUserRepository companyUserRepository, IStringLocalizer<Resources.Resources> localizer
            )
        {
            _companyUserRepository = companyUserRepository;
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddCompanyUserCommand request, CancellationToken cancellationToken)
        {
            var responseMessage = new ResponseMessage();
            try
            {
                var adduser = new B2CCompanyUser().SetCompanyUser(request.UserName, request.Email, request.DepartmentId);
                await _companyUserRepository.CreateCompanyUserAsync(adduser);
                await _companyUserRepository.SaveChangesAsync(); 
                responseMessage.IsSuccess = true;
                responseMessage.Id = adduser.Id;             
                responseMessage.Message = _localizer["User_added"].Value;
                return responseMessage;
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Er is een fout opgetreden in {{0}}", ex.Message);
                throw;
            }
        }
    }
}
