using Energie.Model.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    public class GetUserEnergyTipQuery : IRequest<UserEnergyTipList>
    {
        public int id { get; set; }
        public string UserEmail { get; set; }
        public string Language { get;set; }
    }
}
