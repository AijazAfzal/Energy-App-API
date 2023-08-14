using Energie.Model.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.Query
{
    public record CategoryListQuery : IRequest<CategoryList>
    {
        public string UserEmail { get; set; }
        public string Language { get; set; }
    }
}
