using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Response
{
    public class EnergyPlanResponseList
    {
        public IList<EnergyPlanResponse> EnergyPlanResponse { get; set; }
    }
    public class EnergyPlanResponse
    {
        public int Id { get; set; }
        public int FavouriteTipId { get; set; }
        public string ResponsiblePerson { get; set; }
        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
        public DateTime EndDate { get; set; }
        public string TipBy { get; set; }
        public string PlanStatus { get; set; }
        public bool IsReminder { get; set; }
    }
}
