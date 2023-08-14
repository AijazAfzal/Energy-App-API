using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class UserEnergyTipList
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<UserEnergyTip>? UserEnergyTip { get; set; }

    }
    [ExcludeFromCodeCoverage]
    public class UserEnergyTip
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set;}
        public bool IsSelected { get; set; } = false;
    }
}
