using KiaKooshar.Application;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Infrastructure;
using KiaKooshar.Infrastructure.Persistence;
using KiaKooshar.Infrastructure.Persistence.Authentication.Security;
using KiaKooshar.Infrastructure.Persistence.Logger;
using KiaKooshar.Peresentation.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder (args);
builder.Services.AddControllers ();

builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

#region DataBaseCofig

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
builder.Host.UseSerilog ();

builder.Services.AddAuthorization ();

var stopwatch = Stopwatch.StartNew ();

var app = builder.Build ();

Log.Information (
    "Application started at {Time}",
    DateTime.UtcNow);


app.Lifetime.ApplicationStopping.Register (() =>
{
    stopwatch.Stop ();

    Log.Information (
        "Application stopped. Lifetime: {Elapsed} ms",
        stopwatch.ElapsedMilliseconds);
});

app.UseAuthentication ();
app.UseAuthorization ();
app.UseMiddleware<GlobalExceptionHandler> ();
app.UseSwagger ();
app.UseSwaggerUI ();
app.MapControllers ();

app.Run ();
