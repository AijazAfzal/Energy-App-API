using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Energie.Business.IServices;
using Energie.Business.Services;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Energie.Infrastructure.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(Energie.FunctionApp.Startup))]
namespace Energie.FunctionApp
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddCustomDbContext();
            builder.Services.AddTransient<IEnergyPlanRepository, EnergyPlanRepository>();
            builder.Services.AddTransient<IUserEnergyTipRepository, UserEnergyTipRepository>();
            builder.Services.AddTransient<ISendGrid, SendGridd>();
            builder.Services.AddTransient<IEnergyAnalysisRepository, EnergyAnalysisRepository>();
            builder.Services.AddTransient<IEnergyScoreRepository, EnergyScoreRepository>(); 

        }
    }
    public static class testClass
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=tcp:energie.database.windows.net,1433;Initial Catalog=db_Energies;");
            }, ServiceLifetime.Scoped);
            //services.AddDbContext<AppDbContext>((provider, options) =>
            //{
            //    //var configuration = provider.GetRequiredService<IConfiguration>();
            //    options.UseSqlServer("Server=tcp:energie.database.windows.net,1433;Initial Catalog=db_Energies;",
            //    sqlServerOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //        sqlOptions.CommandTimeout(120);
            //    });
            //},
            //ServiceLifetime.Scoped
            //);

            return services;
        }
    }
    }
