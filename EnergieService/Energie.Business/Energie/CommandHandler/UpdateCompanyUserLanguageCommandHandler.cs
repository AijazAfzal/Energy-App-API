using Energie.Business.Energie.Command;
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
    public class UpdateCompanyUserLanguageCommandHandler : IRequestHandler<UpdateCompanyUserLanguageCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateCompanyUserLanguageCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateCompanyUserLanguageCommandHandler(ILogger<UpdateCompanyUserLanguageCommandHandler> logger,
                                                      ICompanyUserRepository companyUserRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;

        }
   

        public async Task<ResponseMessage> Handle(UpdateCompanyUserLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responsemessage=new ResponseMessage();  
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var updatedLanguage = user.UpdateCompanyUserLanguage(request.LanguageId);
                await _companyUserRepository.UpdateCompanyUserLanguage(updatedLanguage); 
                responsemessage.IsSuccess = true;
                responsemessage.Id = updatedLanguage.Id;
                responsemessage.Message = _localizer["Language_user_updated"].Value;
                return responsemessage;   
                
            }
            catch (Exception ex)
            {               
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(UpdateCompanyUserLanguageCommandHandler));  
                throw; 

            }
        }
    }
}
