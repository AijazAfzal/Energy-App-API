using Energie.Domain.Domain;
using Energie.Model.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    public record GetUserDepartmentTipListQuery : IRequest<UserDepartmentTipList>
    {
       public string UserEmail { get; set; }  
    }
}
