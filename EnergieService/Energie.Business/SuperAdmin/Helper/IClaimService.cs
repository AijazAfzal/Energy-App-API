using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.Helper
{
    public interface IClaimService
    {
        string GetUserEmail();
        string GetLanguage();
    }
}

