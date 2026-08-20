using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Hangfire;
using HealthChecks.UI.Client;
using KiaKooshar.Application;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Infrastructure;
using KiaKooshar.Infrastructure.BackgroundJobs.JobSchaduler;
using KiaKooshar.Infrastructure.DependencyInjection;
using KiaKooshar.Infrastructure.Persistence;
using KiaKooshar.Infrastructure.Persistence.Authentication.Security;
using KiaKooshar.Infrastructure.Persistence.Logger;
using KiaKooshar.Peresentation.Authorization;
using KiaKooshar.Peresentation.Middleware;
using KiaKooshar.Peresentation.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder (args);
builder.Services.AddControllers ();


#region DataBaseConfig

builder.Services.AddDbContext<DatabaseContext> (options =>
{
    options.UseSqlServer (builder.Configuration.GetConnectionString ("DefaultConnection"));
});

#endregion
#region Configration

builder.Services.ConfigureApplicationServices (builder.Configuration);
builder.Services.AddInfrastructureServices (builder.Configuration);

#endregion
#region AutoMapper

builder.Services.AddAutoMapper (cfg => { }, typeof (AssemblyReference).Assembly);

#endregion
#region UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork> ();
#endregion
#region PasswordHasher
builder.Services.AddScoped<IPasswordHasher, PasswordHasher> ();
#endregion
#region BaseLogger
builder.Services.AddScoped<IBaseLogger, BaseLogger> ();
#endregion
#region Authorization
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler> ();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider> ();
#endregion
#region ApiVersion
builder.Services
    .AddApiVersioning (options =>
    {
        options.DefaultApiVersion = new ApiVersion (1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddMvc ()
    .AddApiExplorer (options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddSwaggerGen ();

builder.Services.AddTransient<
    IConfigureOptions<SwaggerGenOptions>,
    ConfigureSwaggerOptions> ();
#endregion
builder.Host.UseSerilog ();

builder.Services.AddAuthorization ();

var stopwatch = Stopwatch.StartNew ();

var app = builder.Build ();

#region HealthCheckMap
app.MapHealthChecks ("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks ("/health/live",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => false
    });
app.MapHealthChecks ("/health/ready",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains ("db") ||
            check.Tags.Contains ("cache"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
#endregion
#region HealthCheckUI
app.MapHealthChecksUI (config => config.UIPath = "/health-ui");
#endregion
#region HangfireDashboard
app.UseHangfireDashboard ("/hangfire");
#endregion


Log.Information (
    "Application started at {Time}",
    DateTime.UtcNow);

var apiVersionProvider =
    app.Services.GetRequiredService<IApiVersionDescriptionProvider> ();

app.UseRateLimiter ();
app.UseMiddleware<GlobalExceptionHandler> ();
app.UseAuthentication ();
app.UseAuthorization ();
app.UseSwagger ();
app.UseHangfireDashboard ("/hangfire");
app.Services.CleanupRefreshToken ();
app.UseSwaggerUI (options =>
{
    options.SwaggerEndpoint (
        "/swagger/v1/swagger.json",
        "KiaKooshar.Presentation V1");

    options.SwaggerEndpoint (
        "/swagger/v2/swagger.json",
        "KiaKooshar.Presentation V2");
});
app.MapControllers ();

app.Run ();
