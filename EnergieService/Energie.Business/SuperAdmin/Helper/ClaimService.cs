using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.Helper
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _useremail;
        private readonly string _language;
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var language = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
            _language = language;
            ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User as ClaimsPrincipal;
            if (null != principal)
            {
                foreach (Claim claim in principal.Claims)
                {
                    if (claim.Type == "emails")
                    {
                        _useremail = claim.Value;
                    }
                }
            }
            else
            {
                _useremail = default;
            }
        }
        public string GetUserEmail()
        {
            return _useremail;
        }
        public string GetLanguage()
        {
            return _language;
        }
    }
    
}
