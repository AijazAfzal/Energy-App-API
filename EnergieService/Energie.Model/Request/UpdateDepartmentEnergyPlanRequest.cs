using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class UpdateDepartmentEnergyPlanRequest
    {
        public int Id { get; set; }
        public string PlanStatus { get; set; }
        public bool IsReminder { get; set; }
        public DateTime EndDate { get; set; }
    }
}
