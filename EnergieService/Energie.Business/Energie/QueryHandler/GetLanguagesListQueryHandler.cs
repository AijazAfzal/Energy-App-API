using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Business.SuperAdmin.QueryHandler;
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

namespace Energie.Business.Energie.QueryHandler
{
    public class GetLanguagesListQueryHandler : IRequestHandler<GetLanguagesListQuery, LanguageList> 
    {
        private readonly ILogger<GetLanguagesListQueryHandler> _logger; 
        private readonly ICompanyUserRepository _companyUserRepository; 
        private readonly IMapper _mapper;
        public GetLanguagesListQueryHandler(ILogger<GetLanguagesListQueryHandler> logger,
                                           ICompanyUserRepository companyUserRepository,
                                           IMapper mapper) 
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _mapper = mapper; 
            
        }
        public async Task<LanguageList> Handle(GetLanguagesListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var languagelist = await _companyUserRepository.GetLanguagesAsync();
                var response = _mapper.Map<IList<Model.Request.Language>>(languagelist);
                return new LanguageList { Languages = response };  
                
            }
            catch (Exception ex) 
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(GetLanguagesListQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetLanguagesListQueryHandler));
                throw;  
            }
            
        }
    }
}
