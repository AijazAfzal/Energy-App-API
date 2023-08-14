using Energie.Model.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    //public class UserEnergyAnalysisQuery : IRequest<List<object>>
    public class UserEnergyAnalysisQuery : IRequest<UserEnergyAnalysisRequestList>
    {
        public string Language { get; set; }
    }
}
