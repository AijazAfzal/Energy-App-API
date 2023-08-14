using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Command
{
    public class AddDepartmentCommand : IRequest<ResponseMessage>
    {
        public string UserEmail { get; set; }
        public string DepartmentName { get; set; }
    }
}
