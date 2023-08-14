using Energie.Model.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Query
{
    public record EmployerHelpCategoryListQuery : IRequest<EmployerHelpCategoryList>
    {
        public string Language { get; set; }
    }
}
