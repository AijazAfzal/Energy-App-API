using Azure;
using Azure.Identity;
using Energie.Domain;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmailAddress = SendGrid.Helpers.Mail.EmailAddress;
using Response = SendGrid.Response;

namespace Energie.Infrastructure.Repository
{
    public class CompanyAdminRepository : ICompanyAdminRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly GraphServiceClient _graphClient;
        private readonly CompanyAdminB2C _companyAdminB2C;
        public EmailSenderOptions EmailSenderOptions { get; set; }
        public CompanyAdminRepository(AppDbContext appDbContext
            , IOptions<EmailSenderOptions> options
            , IOptions<CompanyAdminB2C> companyAdminB2C)
        {
            _appDbContext = appDbContext;
            _companyAdminB2C = companyAdminB2C.Value;
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(_companyAdminB2C.clientID)
                .WithTenantId(_companyAdminB2C.tenantId)
                .WithClientSecret(_companyAdminB2C.clientSecret)
                .Build();
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var clientSecretCredential = new ClientSecretCredential(_companyAdminB2C.tenantId, _companyAdminB2C.clientID, _companyAdminB2C.clientSecret);

            GraphServiceClient graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            _graphClient = graphClient;
            _appDbContext = appDbContext;
            EmailSenderOptions = options.Value;

        }
        public async Task CreateCompanyAdmin(AddB2CCompanyAdmin addB2CCompanyAdmin)
        {
            await _graphClient.Users
                    .Request()
                    .AddAsync(new User
                    {

                        GivenName = addB2CCompanyAdmin.UserName,
                        Surname = addB2CCompanyAdmin.UserName,
                        DisplayName = addB2CCompanyAdmin.UserName,
                        JobTitle = "CompanyAdmin",
                        CompanyName = addB2CCompanyAdmin.CompanyName,

                        Identities = new List<ObjectIdentity>
                        {
                            new ObjectIdentity()
                            {
                                SignInType = "emailAddress",
                                Issuer = _companyAdminB2C.tenantId,
                                IssuerAssignedId = addB2CCompanyAdmin.Email
                            }
                        },
                        PasswordProfile = new PasswordProfile()
                        {
                            Password = addB2CCompanyAdmin.Password,
                            ForceChangePasswordNextSignIn = false
                        },
                        PasswordPolicies = "DisablePasswordExpiration",

                    });
                    var setCompanyAdmin = new CompanyAdmin()
                        .SetCompanyAdmin
                        (
                              addB2CCompanyAdmin.CompanyId
                            , addB2CCompanyAdmin.UserName
                            , addB2CCompanyAdmin.Email
                        );
                    await AddCompanyAdminAsync(setCompanyAdmin);
                    await SendEmail(EmailSenderOptions.ApiKey, addB2CCompanyAdmin.Email, addB2CCompanyAdmin.Password, addB2CCompanyAdmin.UserName);
        }

        #region Create CompanyAdmin in Db
        public async Task AddCompanyAdminAsync(CompanyAdmin companyAdmin)
        {
            await _appDbContext.CompanyAdmin.AddAsync(companyAdmin);
            await _appDbContext.SaveChangesAsync();
        }
        #endregion

        #region for Email

        private async Task<Response> SendEmail(
            string apiKey
            , string email
            , string password
            , string userName
            )
        {
            var client = new SendGridClient(apiKey);

            string mailbody = await PopulateBodyForProfessorActivity(userName, email, password);

            var Message = new SendGridMessage()
            {
                From = new EmailAddress(EmailSenderOptions.SenderEmail, EmailSenderOptions.SenderName),
                Subject = "Login details for EnergyApp",
                PlainTextContent = "",
                HtmlContent = mailbody,
            };
            Message.AddTo(new EmailAddress(email));
            return await client.SendEmailAsync(Message);
        }

        private async Task<string> PopulateBodyForProfessorActivity(string UserName, string email, string password)
        {
            var currentdate=DateTime.Now; 
            string body = string.Empty;
            string fileFullPath = string.Format(@".\EmailPage\CompanyAdmin\CompanyAdminEmail.html");
            string logopath = string.Format(@".\EmailPage\CompanyAdmin\main-logo.png");   
            using (StreamReader reader = System.IO.File.OpenText(fileFullPath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{username}}", UserName);
            Regex rgx = new Regex("<p>|</p>");
            Regex rgx2 = new Regex("&nbsp;");
            body = body.Replace("{{logo}}", logopath);
            body = body.Replace("{{email}}", email);
            body = body.Replace("{{password}}", password);
            body = body.Replace("{{year}}", currentdate.Year.ToString()); 
            return body;
        }
        #endregion


        public async Task<CompanyAdmin> GetCompanyAdminByEmailAsync(string email)
        {
            var companyAdmin = await _appDbContext.CompanyAdmin
                                     .Where(x => x.Email == email)
                                     .Include(x=> x.Company)
                                     .FirstOrDefaultAsync();
            return companyAdmin;
        }
    }
}
