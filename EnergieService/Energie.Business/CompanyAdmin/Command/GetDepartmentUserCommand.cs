
using Energie.Domain.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Command
{
    public class GetDepartmentUserCommand : IRequest<List<CompanyUser>>
    {
        [Required(ErrorMessage = "DepartmentIdRequired")]
        public int DepartmentId { get; set; }
    }
}
