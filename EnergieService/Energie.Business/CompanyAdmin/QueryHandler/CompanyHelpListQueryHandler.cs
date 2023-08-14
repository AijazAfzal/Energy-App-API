using AutoMapper;
using Energie.Business.CompanyAdmin.Query;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.CompanyAdmin.QueryHandler
{
    public class CompanyHelpListQueryHandler : IRequestHandler<CompanyHelpListQuery, CompanyHelpList>
    {
        private readonly ICompanyHelpRepository _companyHelpTipRepository;
        private readonly ILogger<CompanyHelpListQueryHandler> _logger;
        private readonly ICompanyAdminRepository _companyAdminRepository;
        private readonly IMapper _mapper;
        public CompanyHelpListQueryHandler(ICompanyHelpRepository companyHelpTipRepository
            , ILogger<CompanyHelpListQueryHandler> logger
            , ICompanyAdminRepository companyAdminRepository
            , IMapper mapper)
        {
            _companyHelpTipRepository = companyHelpTipRepository;
            _logger = logger;
            _companyAdminRepository = companyAdminRepository;
            _mapper = mapper;
        }
        public async Task<CompanyHelpList> Handle(CompanyHelpListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _companyAdminRepository.GetCompanyAdminByEmailAsync(request.userEmail);
                var companyHelp = await _companyHelpTipRepository.GetCompanyHelpsAsync((int)user.CompanyId);
                var companyHelpList = _mapper.Map<List<CompanyHelp>>(companyHelp);
                return new CompanyHelpList { CompanyHelpTips = companyHelpList };
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(CompanyHelpListQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(CompanyHelpListQueryHandler));
                throw;
            }
        }
    }
}
