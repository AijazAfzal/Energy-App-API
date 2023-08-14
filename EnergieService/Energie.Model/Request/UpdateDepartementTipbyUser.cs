using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class UpdateDepartementTipbyUser
    {
        public int Id { get; set; }
        public string Description { get; set; }  
    }
}
