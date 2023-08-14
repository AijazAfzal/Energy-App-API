using AutoMapper;
using Energie.Business.CompanyAdmin.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.CompanyAdmin.QueryHandler
{
    public class CompanyHelpByIdQueryHandler : IRequestHandler<CompanyHelpByIdQuery, CompanyHelp>
    {
        private readonly ILogger<CompanyHelpByIdQueryHandler> _logger;
        private readonly ICompanyHelpRepository _companyHelpTipRepository;
        private readonly ICompanyAdminRepository _addCompanyAdminRepository;
        private readonly IMapper _mapper;
        public CompanyHelpByIdQueryHandler(ILogger<CompanyHelpByIdQueryHandler> logger
            , ICompanyHelpRepository companyHelpTipRepository
            , ICompanyAdminRepository addCompanyAdminRepository
            , IMapper mapper)
        {
            _logger = logger;
            _companyHelpTipRepository = companyHelpTipRepository;
            _mapper = mapper;
            _addCompanyAdminRepository = addCompanyAdminRepository;
        }
        public async Task<CompanyHelp> Handle(CompanyHelpByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var companyAdmin = await _addCompanyAdminRepository.GetCompanyAdminByEmailAsync(request.UserEmail);
                var companyHelp = await _companyHelpTipRepository.GetCompanyHelpByIdAsync(request.Id, (int)companyAdmin.CompanyId);
                var resonse = _mapper.Map<CompanyHelp>(companyHelp);
                return resonse;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(CompanyHelpByIdQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(CompanyHelpByIdQueryHandler));
                throw ex;
            }
        }
    }
}
