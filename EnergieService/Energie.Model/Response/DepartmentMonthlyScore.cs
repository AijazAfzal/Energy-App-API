using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Response
{
    [ExcludeFromCodeCoverage]
    public class DepartmentMonthlyScore
    {
        public int alluser { get; set; }
        public int activeusers { get; set; }
        public double Percentage { get; set; }
        public List<EnergyScore> EnergyScores { get; set; }
    }
}
