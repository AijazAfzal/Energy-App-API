using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class UserDepartmentTipList
    {
        public IList<UserDepartmentTip> userDepartmentTips { get; set; } 
    }
    [ExcludeFromCodeCoverage]
    public class UserDepartmentTip
    {

        public int Id { get; set; }
        public int categoryId { get; set; }  
        public int SourceId { get; set; }  

        public string Name { get; set; } 
        public string Description { get; set; }  
       
    }
}
