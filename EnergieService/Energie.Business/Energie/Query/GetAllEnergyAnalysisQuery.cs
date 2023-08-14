using Energie.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    public class GetAllEnergyAnalysisQuery : IRequest<ListEnergyAnalysisQuestions>
    {
        public string language { get; set; }
        public string UserEmail { get; set; }
    }
}
