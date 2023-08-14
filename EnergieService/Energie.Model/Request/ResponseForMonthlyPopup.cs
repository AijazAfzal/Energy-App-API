using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class ResponseForMonthlyPopup
    {
        public bool IsPopUp { get; set; }
        public bool IsRecordAlredyExist { get; set; } 
    }
}
