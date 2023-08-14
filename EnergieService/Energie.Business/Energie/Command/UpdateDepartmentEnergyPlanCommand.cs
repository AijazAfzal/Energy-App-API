using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Command
{
    public class UpdateDepartmentEnergyPlanCommand : IRequest<ResponseMessage>
    {
        public int Id { get; set; }
        public string PlanStatus { get; set; }
        public bool IsReminder { get; set; }
        public DateTime EndDate { get; set; }
        public string UserEmail { get; set; }
    }
}
