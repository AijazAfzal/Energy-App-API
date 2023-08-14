using Energie.Model.Response;
using MediatR;

namespace Energie.Business.CompanyAdmin.Query
{
    public record DepartmentEmployerHelpListQuery : IRequest<DepartmentEmployerHelpList>
    {
        public string UserEmail { get; set; }
    }
}
