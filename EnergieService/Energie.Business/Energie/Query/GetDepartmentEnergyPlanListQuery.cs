using Energie.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    public class GetDepartmentEnergyPlanListQuery : IRequest<DepartmentEnergyPlanResponseList>
    {
        public string UserEmail { get; set; }  
        public string Language { get;set; }
    }
}
