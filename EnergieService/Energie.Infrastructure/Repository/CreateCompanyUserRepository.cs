using Azure.Identity;
using Energie.Domain;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Energie.Model;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmailAddress = SendGrid.Helpers.Mail.EmailAddress;
using Microsoft.EntityFrameworkCore;

namespace Energie.Infrastructure.Repository
{
    public class CreateCompanyUserRepository : ICreateCompanyUserRepository
    {
        private readonly GraphServiceClient _graphClient;
        private readonly CompanyUserB2C _companyUserB2C;
        public EmailSenderOptions EmailSenderOptions { get; set; }
        private readonly AppDbContext _appDbContext;
        public CreateCompanyUserRepository(AppDbContext appDbContext
           , IOptions<EmailSenderOptions> options
           , IOptions<CompanyUserB2C> companyUserB2C)
        {
            _companyUserB2C = companyUserB2C.Value;
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(_companyUserB2C.clientID)
                .WithTenantId(_companyUserB2C.tenantId)
                .WithClientSecret(_companyUserB2C.clientSecret)
                .Build();
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var clientSecretCredential = new ClientSecretCredential(_companyUserB2C.tenantId, _companyUserB2C.clientID, _companyUserB2C.clientSecret);

            GraphServiceClient graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            _graphClient = graphClient;
            _appDbContext = appDbContext;
            EmailSenderOptions = options.Value;

        }
        public async Task CreateCompanyUserAsync(B2CCompanyUser b2CCompanyUser)
        {
            var result = await _graphClient.Users
                   .Request()
                   .AddAsync(new User
                   {
                       GivenName = b2CCompanyUser.UserName,
                       Surname = b2CCompanyUser.UserName,
                       DisplayName = b2CCompanyUser.UserName,

                       Identities = new List<ObjectIdentity>
                       {
                            new ObjectIdentity()
                            {
                                SignInType = "emailAddress",
                                Issuer = _companyUserB2C.tenantId,
                                IssuerAssignedId = b2CCompanyUser.Email
                            }
                       },
                       PasswordProfile = new PasswordProfile()
                       {
                           Password = b2CCompanyUser.Password,
                           ForceChangePasswordNextSignIn = false
                       },
                       PasswordPolicies = "DisablePasswordExpiration",
                   });
            var setCompanyAdmin = new CompanyUser()
                        .SetCompanyUser(b2CCompanyUser.UserName, b2CCompanyUser.Email, (int)b2CCompanyUser.DepartmentID);

            var companyadmin = await AddCompanyUserAsync(setCompanyAdmin);
            await SendEmail(EmailSenderOptions.ApiKey, b2CCompanyUser.Email, b2CCompanyUser.Password, b2CCompanyUser.UserName);
            
        }
        #region Create CompanyAdmin in Database
        public async Task<int> AddCompanyUserAsync(CompanyUser companyUser)
        {
            await _appDbContext.CompanyUser.AddAsync(companyUser);
            //await _appDbContext.SaveChangesAsync();
            return companyUser.Id;
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
            string fileFullPath = string.Format(@".\EmailPage\CompanyAdmin\CompanyUserEmail.html");
            using (StreamReader reader = System.IO.File.OpenText(fileFullPath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{username}}", UserName);
            Regex rgx = new Regex("<p>|</p>");
            Regex rgx2 = new Regex("&nbsp;");
            body = body.Replace("{{email}}", email);
            body = body.Replace("{{password}}", password);
            body = body.Replace("{{url}}", EmailSenderOptions.URL);
            body = body.Replace("{{year}}", currentdate.Year.ToString()); 
            return body;
        }
        #endregion


        #region For Delete 
        public async Task<int> GetUSerByIDAsync(int userID)
        {
            var userId =await _appDbContext.CompanyUser.Where(x=> x.Id== userID).Select(x=> x.Id).FirstOrDefaultAsync();
            return userId;
        }
        public async Task DeleteUserAccountAsync(int userId) 
        {
 
           var usermonthlynotifications = await _appDbContext.MonthlyNotifications.Where(x => x.CompanyUserID == userId).ToListAsync();
            _appDbContext.MonthlyNotifications.RemoveRange(usermonthlynotifications);
   

            var userenergyscore = await _appDbContext.EnergyScore.Where(x => x.CompanyUserID == userId).ToListAsync();
            _appDbContext.EnergyScore.RemoveRange(userenergyscore);

            var userenergyanalysis = await _appDbContext.UserEnergyAnalyses.Where(x => x.CompanyUserID == userId).ToListAsync();
            _appDbContext.UserEnergyAnalyses.RemoveRange(userenergyanalysis);  

            var userfavtip = await _appDbContext.UserTip.Where(x => x.CompanyUserID == userId).ToListAsync();
            _appDbContext.UserTip.RemoveRange(userfavtip);

            var userfavdepttip = await _appDbContext.UserDepartmentTip.Where(x => x.CompanyUserId == userId).ToListAsync();
            _appDbContext.UserDepartmentTip.RemoveRange(userfavdepttip);

            var superadminfavtip = await _appDbContext.UserFavouriteTips.Where(x => x.CompanyUserId == userId).ToListAsync();
            _appDbContext.UserFavouriteTips.RemoveRange(superadminfavtip);

            var companyadminhelp = await _appDbContext.UserFavouriteHelp.Where(x => x.CompanyUserId == userId).ToListAsync();
            _appDbContext.UserFavouriteHelp.RemoveRange(companyadminhelp);

            var deptsuperadminfavtip = await _appDbContext.DepartmentFavouriteTip.Where(x => x.CompanyUserId == userId).ToListAsync();
            _appDbContext.DepartmentFavouriteTip.RemoveRange(deptsuperadminfavtip);

            var deptfavhelp = await _appDbContext.DepartmentFavouriteHelps.Where(x => x.CompanyUserId == userId).ToListAsync();
            _appDbContext.DepartmentFavouriteHelps.RemoveRange(deptfavhelp);

            var liketipbyuserindept = await _appDbContext.LikeTip.Where(x => x.CompanyUserID == userId).ToListAsync();
            _appDbContext.LikeTip.RemoveRange(liketipbyuserindept);

            var energyplan = await _appDbContext.EnergyPlan.Where(x => x.CompanyUserId == userId).ToListAsync();
            _appDbContext.EnergyPlan.RemoveRange(energyplan);

            var energyplans = await _appDbContext.EnergyPlan.Where(x => x.ResponsiblePersonId == userId).ToListAsync(); 
            _appDbContext.EnergyPlan.RemoveRange(energyplans);

            var departmentenergyplan = await _appDbContext.DepartmentEnergyPlans.Where(x => x.CompanyUserId == userId).ToListAsync(); 
            _appDbContext.DepartmentEnergyPlans.RemoveRange(departmentenergyplan);

            var departmentenergyplans = await _appDbContext.DepartmentEnergyPlans.Where(x => x.ResponsiblePersonId == userId).ToListAsync();
            _appDbContext.DepartmentEnergyPlans.RemoveRange(departmentenergyplans);



            await _appDbContext.SaveChangesAsync();

            var user = await _appDbContext.CompanyUser.Where(x => x.Id == userId).FirstOrDefaultAsync(); 

             await DeleteUserbyEmailIdAsync(user.Email);
            _appDbContext.CompanyUser.Remove(user);
             await _appDbContext.SaveChangesAsync();   
            
        }
        public async Task DeleteUserbyEmailIdAsync(string email)
        {
            var user = await this.GetUserByEmailAsync(email);
            var userId = user.CurrentPage[0].Id;
            // Delete user by object ID
            await _graphClient.Users[userId].Request().DeleteAsync(); 
        }

        public async Task<IGraphServiceUsersCollectionPage> GetUserByEmailAsync(string email)
        {
            try
            {
                // Get user by sign-in name
                var result = await _graphClient.Users
                    .Request()
                    .Filter($"identities/any(c:c/issuerAssignedId eq '{email}' and c/issuer eq '{_companyUserB2C.tenantId}')")
                    .Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities
                    })
                    .GetAsync();

                if (result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync(); 
        }
        #endregion

    }
}
