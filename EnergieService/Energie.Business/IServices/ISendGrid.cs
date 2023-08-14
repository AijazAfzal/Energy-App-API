using Energie.Domain.Domain;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.IServices
{
    public interface ISendGrid
    {
        Task SendMailForPlanDeadline (CompanyUser userDetails, string body);

        Task<string> PopulateBodyForDeadlineEmail(string UserName, string description, DateTime expiryDate, string path);

        Task SendMailForAnalysis(CompanyUser userDetails, string body);

        Task<string> PopulateBodyForAnalysisEmail(string UserName, string path);

        Task SendMailForMonthlyScore(CompanyUser userinfo, string body);  

        Task<string> PopulateBodyForMonthlyScoreEmail(string UserName, string path); 
    }
}
