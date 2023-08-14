using AutoMapper;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;

namespace Energie.Business.SuperAdmin.MediatR.Queries.Company
{
    public class CompaniesQueryHandler : IRequestHandler<CompaniesQuery, CompanyList>
    {
        private readonly ICompanyRepository<Domain.Domain.Company> _companyRepository;
        private readonly IMapper _mapper;
        public CompaniesQueryHandler(ICompanyRepository<Domain.Domain.Company> companyRepository
            , IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<CompanyList> Handle(CompaniesQuery request, CancellationToken cancellationToken)
        {
            return new CompanyList
            {
                Companies = _mapper.Map<IList<Model.Request.Company>>(await _companyRepository.CompanyListAsync())
            };

        }
    }
}
