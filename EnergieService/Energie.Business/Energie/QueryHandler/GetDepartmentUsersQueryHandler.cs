using AutoMapper;
using Energie.Business.Energie.Query;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.QueryHandler
{
    public class GetDepartmentUsersQueryHandler : IRequestHandler<GetDepartmentUsersQuery, DepartmentUserList>
    {
        private readonly ILogger<GetDepartmentUsersQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICompanyUserRepository _companyUserRepository;
        public GetDepartmentUsersQueryHandler(ILogger<GetDepartmentUsersQueryHandler> logger
            , ICompanyUserRepository companyUserRepository
            , IMapper mapper)
        {
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _mapper = mapper;
        }
        public async Task<DepartmentUserList> Handle(GetDepartmentUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user =await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var listOfUsers = await _companyUserRepository.GetCompanyUserByDepartmentId((int)user.DepartmentID);
                var departmentuserList = _mapper.Map<List<DepartmentUser>>(listOfUsers);
                return new DepartmentUserList
                {
                    DepartmentUsers = departmentuserList
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"error occured in {{0}}", nameof(GetDepartmentUsersQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetDepartmentUsersQueryHandler)); 
                throw;
            }
        }
    }
}
