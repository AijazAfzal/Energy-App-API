
using Energie.Domain.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Query
{
    public record GetDepartmentListQuery : IRequest<List<Department>>
    {
        public string UserEmail { get; set; }
    }
}
