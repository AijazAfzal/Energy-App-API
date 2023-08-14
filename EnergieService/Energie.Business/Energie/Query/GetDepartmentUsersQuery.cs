using Energie.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    public record GetDepartmentUsersQuery : IRequest<DepartmentUserList>
    {
        public string UserEmail { get; set; }
    }
}
