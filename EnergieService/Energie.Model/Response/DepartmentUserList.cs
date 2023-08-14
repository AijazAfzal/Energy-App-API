using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Energie.Model.Request;

namespace Energie.Model.Response
{
    [ExcludeFromCodeCoverage]
    public class DepartmentUserList
    {
        public List<DepartmentUser> DepartmentUsers { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class DepartmentUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public virtual int? DepartmentID { get; set; }
        public virtual Department? Department { get; set; }
    }
}
