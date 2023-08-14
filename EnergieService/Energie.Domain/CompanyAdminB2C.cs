using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain
{
    public class CompanyAdminB2C
    {
        public string tenantId { get; set; }
        public string clientID { get; set; }
        public string clientSecret { get; set; }
        public string b2cExtensionAppClientId { get; set; }
    }
}
