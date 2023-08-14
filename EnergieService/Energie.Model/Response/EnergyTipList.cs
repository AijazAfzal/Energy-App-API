using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Response
{
    [ExcludeFromCodeCoverage]
    public class EnergyTipList
    {
        public List<EnergyTip> EnergyTips { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class EnergyTip
    {
        public int sourceId { get; set; }
        public int categoryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
