using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{

    public class DepartmentEnergyTip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }
        public bool IsLiked { get; set; }

        public string TipBy { get; set; }
        public DateTime AddedDate { get; set; }
    } 
    public class DepartmentEnergyTipList
    {
        public int Id { get; set; }
        public string EnergyAnalysisName { get; set; }
        public string EnergyAnalysisDescription { get; set; }
        public List<DepartmentEnergyTip> DepartmentEnergyTips { get; set; }
    }

    public class DepartmentEnergyTipsList
    {
        public List<DepartmentEnergyTip> DepartmentEnergyTips { get; set; }
    }




}
