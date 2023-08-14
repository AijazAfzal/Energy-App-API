using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.Command
{

    public class AddTipCommand : IRequest<ResponseMessage>
    {
        public int EnergyAnalysisQuestionId { get; set; }
        public string Tip { get; set; }
        public string Description { get; set; }
    }
}
