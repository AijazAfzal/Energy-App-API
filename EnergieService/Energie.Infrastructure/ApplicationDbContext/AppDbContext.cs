using Energie.Domain.Domain;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Energie.Infrastructure.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            var conn = (SqlConnection)this.Database.GetDbConnection();
            var credential = new Azure.Identity.DefaultAzureCredential();
            var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }, null, null, "d98fec70-4e6d-4ff3-80a1-44e51914179d"));
            conn.AccessToken = token.Token;
        }
        #region Required
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<EnergyPlan>()
        //        .HasKey(x => new { x.CompanyUserId, x.ResponsiblePersonId });
        //    modelBuilder.Entity<EnergyPlan>()
        //        .HasOne(x=> x.CompanyUser)
        //        .WithMany(x=> x)
        //        .HasF
        //        .Property(b => b.Url)
        //        .IsRequired();
        //}
        #endregion
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyAdmin> CompanyAdmin { get; set; }
        public DbSet<CompanyUser> CompanyUser { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<EnergyScore> EnergyScore { get; set; }
        public DbSet<Month> Month { get; set; }
        public DbSet<MonthlyNotification> MonthlyNotifications { get; set; }
        public DbSet<EnergyAnalysis> EnergyAnalysis { get; set; }
        public DbSet<EnergyAnalysisQuestions> EnergyAnalysisQuestions { get; set; }
        public DbSet<UserEnergyAnalysis> UserEnergyAnalyses { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UserFavouriteTip> UserFavouriteTips { get; set; }
        public DbSet<CompanyHelp> CompanyHelps { get; set; }
        public DbSet<CompanyHelpCategory> CompanyHelpCategorys { get; set;}
        public DbSet<UserFavouriteHelp> UserFavouriteHelp { get; set; }
        public DbSet<UserTip> UserTip { get; set; }
        public DbSet<DepartmentTip> DepatrmentTip { get; set; }
        public DbSet<DepartmentFavouriteTip> DepartmentFavouriteTip { get; set; }
        public DbSet<UserDepartmentTip> UserDepartmentTip { get; set; }
        public DbSet<LikeTip> LikeTip { get; set; }
        public DbSet<HelpCategory> HelpCategory { get; set;}
        public DbSet<CompanyDepartmentHelp> CompanyDepartmentHelp { get; set; }
        public DbSet<DepartmentFavouriteHelp> DepartmentFavouriteHelps { get; set; }
        public DbSet<TipType> TipType { get; set; }
        public DbSet<EnergyPlan> EnergyPlan { get; set; }
        public DbSet<PlanStatus> PlanStatus { get; set; }

        public DbSet<DepartmentEnergyPlan> DepartmentEnergyPlans { get; set; }

        public DbSet<Language> Languages { get; set; } 
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; } 
        public DbSet<Translations> Translations { get; set; }   


    }
}
