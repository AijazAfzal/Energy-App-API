using Energie.Domain.Domain;
using Energie.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Query
{
    public record CompanyHelpListQuery : IRequest<CompanyHelpList>
    {
        public string userEmail { get; set; }
    }
}
