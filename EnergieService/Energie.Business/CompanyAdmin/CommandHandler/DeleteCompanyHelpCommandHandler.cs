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
    public class DeleteCompanyHelpCommandHandler : IRequestHandler<DeleteCompanyHelpCommand, ResponseMessage>
    {
        private readonly ILogger<DeleteCompanyHelpCommandHandler> _logger;
        private readonly ICompanyHelpRepository _companyHelpTipRepository;
        private readonly ICompanyAdminRepository _companyAdminRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public DeleteCompanyHelpCommandHandler(ILogger<DeleteCompanyHelpCommandHandler> logger
            , ICompanyHelpRepository companyHelpTipRepository
            , ICompanyAdminRepository companyAdminRepository
            , IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyHelpTipRepository = companyHelpTipRepository;
            _companyAdminRepository = companyAdminRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(DeleteCompanyHelpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var companyAdmin = await _companyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                await _companyHelpTipRepository.DeleteCompanyHelpAsync(request.Id, (int)companyAdmin.CompanyId);
                responseMessage.IsSuccess = true;
                // responseMessage.Message = _localizer["Bedrijfstips met id :"].Value + " " + request.Id + " "+_localizer["verwijderd"].Value;
                responseMessage.Message = _localizer["Business_tips_with_ID_remote",request.Id].Value;
                responseMessage.Id = request.Id;
                return responseMessage;
                
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
