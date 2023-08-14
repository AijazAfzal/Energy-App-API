using Energie.Domain.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.CompanyAdmin.Query
{
    public class CompanyHelpByIdQuery : IRequest<CompanyHelp>
    {
        public string UserEmail { get; set; }
        public int Id { get; set; }
    }
}
