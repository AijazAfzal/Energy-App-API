using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class DepartmentScoreAverage
    {
        public int alluser { get; set; }
        public int activeusers { get; set; }
        public double Percentage { get; set; }
        public double AverageDepartmentScore { get; set; }
        public List<DepartmentScoreAverageMonths> DepartmentScoreAverageMonths { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class DepartmentScoreAverageMonths
    {
        public int score { get; set; } 
        public string month { get; set; }
       
    }
}
