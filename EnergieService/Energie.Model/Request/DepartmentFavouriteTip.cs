using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
   
    public class DepartmentFavouriteTip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TipBy { get; set; }
        public DateTime AddedDate { get; set; }

        public bool IsSelected { get; set; } 

        public int Count { get; set; }

        public bool IsLiked { get; set; } 
    }
   
    public class DepartmentFavouriteTipList
    {
        public List<DepartmentFavouriteTip> DepartmentFavouriteTips { get; set; }
    }
}
