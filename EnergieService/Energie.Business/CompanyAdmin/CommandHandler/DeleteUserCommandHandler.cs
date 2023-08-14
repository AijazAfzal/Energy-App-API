using Energie.Business.CompanyAdmin.Command;
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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseMessage>
    {
        private readonly ICreateCompanyUserRepository _companyUserRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public DeleteUserCommandHandler(ICreateCompanyUserRepository companyUserRepository
            , ILogger<DeleteUserCommandHandler> logger, IStringLocalizer<Resources.Resources> localizer)
        {
            _companyUserRepository = companyUserRepository; 
            _logger = logger;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var responseMessage = new ResponseMessage();
            try
            {
                var userId = await _companyUserRepository.GetUSerByIDAsync(request.UserId);

                //if (userId == null)
                //    return default; 
                
                await _companyUserRepository.DeleteUserAccountAsync(request.UserId); 
                responseMessage.IsSuccess = true;
                //responseMessage.Message = _localizer["Gebruiker is verwijderd"].Value
                responseMessage.Message = _localizer["User_deleted"].Value;
                return responseMessage;
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Error occured in {Handle}", ex.Message);
                _logger.LogError($"Er is een fout opgetreden in {Handle}", ex.Message);
                responseMessage.Message = ex.Message;
                return responseMessage; 
            }
        }
    }
}
