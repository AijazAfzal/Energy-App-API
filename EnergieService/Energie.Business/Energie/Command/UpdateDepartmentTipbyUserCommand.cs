using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Command
{
    public class UpdateDepartmentTipbyUserCommand : IRequest<ResponseMessage>
    {
        public int Id { get; set; } 
        public string Description { get;  set; } 
    }
}
