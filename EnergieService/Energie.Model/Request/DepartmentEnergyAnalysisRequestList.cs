using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class DepartmentEnergyAnalysisRequestList
    {
        public int alluser { get; set; }
        public int activeusers { get; set; }
        public double Percentage { get; set; }
        public List<DepartmentEnergyAnalysisRequest> DepartmentEnergyAnalysisRequests { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class DepartmentEnergyAnalysisRequest
    {
        
        public string energyAnalysis { get; set; }
        public string energyAnalysisQuestions { get; set; }
        public int Precentage { get; set; }
    }
}
