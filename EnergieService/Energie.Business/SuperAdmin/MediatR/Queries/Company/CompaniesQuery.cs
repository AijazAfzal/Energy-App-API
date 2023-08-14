
using Energie.Model.Request;
using MediatR;

namespace Energie.Business.SuperAdmin.MediatR.Queries.Company
{
    public record CompaniesQuery : IRequest<CompanyList> { }
}
