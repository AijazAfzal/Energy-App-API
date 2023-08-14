using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Response
{
    [ExcludeFromCodeCoverage]
    public class ListEnergyScore
    {
        public List<EnergyScore> EnergyScores { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class EnergyScore
    {
        public double score { get; set; }
        public string month { get; set; }
    }
}
