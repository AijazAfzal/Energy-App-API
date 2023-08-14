using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class EnergyPlanRequest
    {
        public int FavouriteTipId { get; set; }
        public int ResponsiblePersonId { get; set; }
        public string TipBy { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsReminder { get; set; }
    }
}
