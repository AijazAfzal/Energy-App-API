using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Response
{
    [ExcludeFromCodeCoverage]
    public class TrendScoreResponse
    {
        public double resentAverage { get; set; } = 0;
        public double lastAverage { get; set; } = 0;
        public double difference { get; set; } = 0;
        public List<EnergyScoreForTrend> EnergyScoreForTrends { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class EnergyScoreForTrend
    {
        public double score { get; set; }
        public string month { get; set; }
        public int year { get; set; }
    }
}
