using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class DepartmentTipList
    {
        public IList<DepartmentTip> DepartmentTips { get; set;}
    }
    [ExcludeFromCodeCoverage]
    public class DepartmentTip
    {
        public int EnergyAnalysisId { get; set; }
        public int EnergyAnalysisQuestionId { get; set; } 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
