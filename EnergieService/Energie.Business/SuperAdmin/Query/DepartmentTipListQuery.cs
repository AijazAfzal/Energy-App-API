using Energie.Model.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.Query
{
    public record DepartmentTipListQuery : IRequest<DepartmentTipList>
    {
    }
}
