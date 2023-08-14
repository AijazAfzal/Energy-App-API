using Energie.Domain.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    public class CompanyCategoryListQuery : IRequest<List<CompanyHelpCategory>>
    {
      public string UserEmail { get; set; }  
    }
}
