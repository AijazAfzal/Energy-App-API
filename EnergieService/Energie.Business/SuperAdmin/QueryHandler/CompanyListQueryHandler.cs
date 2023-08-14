using AutoMapper;
using Energie.Business.SuperAdmin.Query;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.SuperAdmin.QueryHandler
{
    public class CompanyListQueryHandler : IRequestHandler<CompanyListQuery, CompanyList>
    {
        private readonly ICompanyRepository<Domain.Domain.Company> _companyRepository;
        private readonly ILogger<CompanyListQueryHandler> _logger;
        private readonly IMapper _mapper;
        public CompanyListQueryHandler(ICompanyRepository<Domain.Domain.Company> companyRepository
            , ILogger<CompanyListQueryHandler> logger
            , IMapper mapper)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            _mapper= mapper;
        }
        public async Task<CompanyList> Handle(CompanyListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var companylist = await _companyRepository.CompanyListAsync();
                var respone = _mapper.Map<IList<Company>>(companylist);
                return new CompanyList { Companies = respone };
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}" , nameof(CompanyListQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(CompanyListQueryHandler));
                throw;
            }
            
        }
    }
}
