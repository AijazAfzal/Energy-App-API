using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Command
{
    public class AddCompanyUserCommand : IRequest<ResponseMessage>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }
}
