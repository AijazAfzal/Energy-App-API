
using Energie.Business.CompanyAdmin.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Energie.Business.CompanyAdmin.CommandHandler
{
    public class GetDepartmentUserCommandHandler : IRequestHandler<GetDepartmentUserCommand, List<CompanyUser>>
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<GetDepartmentUserCommandHandler> _logger;

        public GetDepartmentUserCommandHandler(
            IDepartmentRepository departmentRepository,
            ILogger<GetDepartmentUserCommandHandler> logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }
        public async Task<List<CompanyUser>> Handle(GetDepartmentUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var departmentuser = await _departmentRepository.GetDepartmentUserAsync(request.DepartmentId);
                return departmentuser;
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error occured in {Handle}", ex.Message);
                _logger.LogError($"Er is een fout opgetreden in {Handle}", ex.Message);
                throw;
            }

        }
    }
}
