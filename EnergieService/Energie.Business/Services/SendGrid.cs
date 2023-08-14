using Energie.Business.IServices;
using Energie.Domain.Domain;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Services
{
    public class SendGridd : ISendGrid
    {
        private readonly ILogger<SendGridd> _logger;
        public SendGridd(ILogger<SendGridd> logger)
        {
            _logger = logger;
        }
        //For Deadline Email
        #region 
        public async Task SendMailForPlanDeadline(CompanyUser user, string body)
        {

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new SendGrid.Helpers.Mail.EmailAddress("info@scaleupecosystems.com", "EnergyApp"),
                Subject = "Energy Plan deadline",
                PlainTextContent = "Hello, Email!",
                HtmlContent = body
            };
            msg.AddTo(new SendGrid.Helpers.Mail.EmailAddress(user.Email, user.UserName));
            var response = await client.SendEmailAsync(msg);
        }

        public async Task<string> PopulateBodyForDeadlineEmail(string UserName, string description, DateTime expiryDate, string path)
        {

            string body = string.Empty;
            string fileFullPath = string.Format(@".\EmailPage\Energie\DeadlineEmail.html");
            string FullPath = Path.Combine(path, fileFullPath);
            _logger.LogInformation(FullPath);
            body = File.ReadAllText(FullPath);
            body = body.Replace("{{username}}", UserName);
            body = body.Replace("{{description}}", description);
            body = body.Replace("{{expiryDate}}", expiryDate.ToString());
            Regex rgx = new Regex("<p>|</p>");
            Regex rgx2 = new Regex("&nbsp;");
            return body;
        }
        #endregion
        //For Analysis Email
        #region
        public async Task SendMailForAnalysis(CompanyUser userDetails, string body)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new SendGrid.Helpers.Mail.EmailAddress("info@scaleupecosystems.com", "EnergyApp"),
                Subject = "Energy Analysis Update",
                PlainTextContent = "Hello, Email!",
                HtmlContent = body
            };
            msg.AddTo(new SendGrid.Helpers.Mail.EmailAddress(userDetails.Email, userDetails.UserName));
            var response = await client.SendEmailAsync(msg);
        }

        public async Task<string> PopulateBodyForAnalysisEmail(string UserName, string path)
        {
            string body = string.Empty;
            string fileFullPath = string.Format(@".\EmailPage\Energie\AnalysisEmail.html");
            string FullPath = Path.Combine(path, fileFullPath);
            _logger.LogInformation(FullPath);
            body = File.ReadAllText(FullPath);
            body = body.Replace("{{username}}", UserName);
            Regex rgx = new Regex("<p>|</p>");
            Regex rgx2 = new Regex("&nbsp;");
            return body;
        }
        #endregion
        //For MonthlyScore Email
        #region
        public async Task SendMailForMonthlyScore(CompanyUser userinfo, string body)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new SendGrid.Helpers.Mail.EmailAddress("info@scaleupecosystems.com", "EnergyApp"),
                Subject = "Fill Monthly EnergyScore",
                PlainTextContent = "Hello, Email!",
                HtmlContent = body
            };
            msg.AddTo(new SendGrid.Helpers.Mail.EmailAddress(userinfo.Email, userinfo.UserName));
            var response = await client.SendEmailAsync(msg);
        }

        public async Task<string> PopulateBodyForMonthlyScoreEmail(string UserName, string path)
        {
            string body = string.Empty;
            string fileFullPath = string.Format(@".\EmailPage\Energie\MonthlyScoreEmail.html");
            string FullPath = Path.Combine(path, fileFullPath);
            _logger.LogInformation(FullPath);
            body = File.ReadAllText(FullPath);
            body = body.Replace("{{username}}", UserName);
            Regex rgx = new Regex("<p>|</p>");
            Regex rgx2 = new Regex("&nbsp;");
            return body;
        }
        #endregion

    }
}
