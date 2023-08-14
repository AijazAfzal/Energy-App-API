using AutoMapper;
using Energie.Business.Energie.Query;
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
    public class GetCompanyUserQueryHandler : IRequestHandler<GetCompanyUserQuery, CompanyUser>
    {
        private readonly ILogger<GetCompanyUserQueryHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IMapper _mapper;
        public GetCompanyUserQueryHandler(ILogger<GetCompanyUserQueryHandler> logger,
            ICompanyUserRepository companyUserRepository,
            IMapper mapper)
            
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;  
            _mapper = mapper; 
            
        }
        public async  Task<CompanyUser> Handle(GetCompanyUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var companyuser = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var user = _mapper.Map<CompanyUser>(companyuser);
                return user; 
            
            }

            catch (Exception ex) 
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(GetCompanyUserQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetCompanyUserQueryHandler)); 
                throw;
            }
            
        }
    }
}
