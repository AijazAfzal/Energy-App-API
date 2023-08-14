using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.Command
{
    public class DeleteEnergyTipCommand : IRequest<ResponseMessage>
    {
        public int TipId { get; set; }
    }
}
