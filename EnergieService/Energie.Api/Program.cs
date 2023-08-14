
using Energie.Api;
using Energie.Api.Helper;
using Energie.Business;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Energie.Infrastructure.Repository;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Energie.Business"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);
//builder.Services.AddMediatR(typeof(CompanyListCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//App Insights
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitializer>(); 

builder.Services.AddScoped<ICompanyRepository<Company>, CompanyRepository<Company>>();
builder.Services.AddScoped<ICompanyAdminRepository, CompanyAdminRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICreateCompanyUserRepository, CreateCompanyUserRepository>();
builder.Services.AddScoped<IFlagRepository, FlagRepository>();
builder.Services.AddScoped<IEnergyScoreRepository, EnergyScoreRepository>();
builder.Services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();
builder.Services.AddScoped<IEnergyAnalysisRepository, EnergyAnalysisRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEnergyTipRepository, EnergyTipRepository>();
builder.Services.AddScoped<IUserEnergyTipRepository, UserEnergyTipRepository>();
builder.Services.AddScoped<ICompanyHelpCategoryRepository, CompanyHelpCategoryRepository>();
builder.Services.AddScoped<ICompanyHelpRepository, CompanyHelpRepository>();
builder.Services.AddScoped<IDepartmentTipRepository, DepartmentTipRepository>();
builder.Services.AddScoped<ILikeTipRepository, LikeTipRepository>();
builder.Services.AddScoped<IUserDepartmentTipRepository, UserDepartmentTipRepository>();
builder.Services.AddScoped<IEnergyPlanRepository, EnergyPlanRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped(typeof(ITranslationsRepository<>), typeof(TranslationsRepository<>));
//builder.Services.AddScoped(typeof(TranslationService<>));


// Adding Cors
builder.Services.AddCors(option =>
{
    option.AddPolicy(
        "CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Adding Multilevel Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(AuthorizationScheme.AzureAD, options =>
    {
        options.Authority = builder.Configuration["Authentication:Authority"];
        options.Audience = builder.Configuration["Authentication:ClientId"];
    })
    .AddJwtBearer(AuthorizationScheme.AzureADB2C, jwtOptions =>
    {
        jwtOptions.Authority = $"{builder.Configuration["AzureAdB2C:Instance"]}/{builder.Configuration["AzureAdB2C:TenantId"]}/{builder.Configuration["AzureAdB2C:SignUpSignInPolicyId"]}/v2.0/";
        jwtOptions.Audience = builder.Configuration["AzureAdB2C:ClientId"];
        jwtOptions.RequireHttpsMetadata = false;
    })
    .AddJwtBearer(AuthorizationScheme.AzureADB2CUser, adb2cUseroption =>
    {
        adb2cUseroption.Authority = $"{builder.Configuration["AzureAdB2CUser:Instance"]}/{builder.Configuration["AzureAdB2CUser:TenantId"]}/{builder.Configuration["AzureAdB2CUser:SignUpSignInPolicyId"]}/v2.0/";
        adb2cUseroption.Audience = builder.Configuration["AzureAdB2CUser:ClientId"];
        adb2cUseroption.RequireHttpsMetadata = false;
    });

var configuration = builder.Configuration;
builder.Services.Configure<CompanyAdminB2C>
    (configuration.GetSection("CompanyAdminB2C"));
builder.Services.Configure<CompanyUserB2C>
    (configuration.GetSection("appSettings"));
builder.Services.Configure<EmailSenderOptions>(options =>
{
    options.ApiKey = configuration["ExternalProviders:SendGrid:ApiKey"];
    options.SenderEmail = configuration["ExternalProviders:SendGrid:SenderEmail"];
    options.SenderName = configuration["ExternalProviders:SendGrid:SenderName"];
    options.SenderName = configuration["ExternalProviders:SendGrid:SenderName"];
});

//for localization
//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[]
//    {
//        new CultureInfo("en-US"),

//    };

//    options.DefaultRequestCulture = new RequestCulture("nl-NL");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//    options.FallBackToParentCultures = false;
//    options.FallBackToParentUICultures = false;

//});


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US")
                   // new CultureInfo("nl")
                };



    options.DefaultRequestCulture = new RequestCulture(culture: "nl-NL", uiCulture: "nl-NL");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;



    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
    {
        var languageHeaderValue = context.Request.Headers["Accept-Language"].ToString();
        var language = languageHeaderValue.Split(',').FirstOrDefault();
        var defaultLang = string.IsNullOrEmpty(language) ? "nl-NL" : language;
        return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
    }));
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.GetPendingMigrations().Any())
        db.Database.Migrate();
}

if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseHttpsRedirection();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();
