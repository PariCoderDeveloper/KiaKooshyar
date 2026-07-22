using KiaKooshar.Application;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Infrastructure.Persistence;
using KiaKooshar.Infrastructure.Persistence.Logger;
using KiaKooshar.Infrastructure.Persistence.Security;
using KiaKooshar.Peresentation.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;


var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddControllers ();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

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

#endregion

#region AutoMapper

builder.Services.AddAutoMapper (cfg => { }, typeof (AssemblyReference).Assembly);

#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork> ();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher> ();
builder.Services.AddScoped<IBaseLogger, BaseLogger> ();

builder.Host.UseSerilog ();

builder.Services
    .AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer (options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],

                ValidAudience = builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey (
                        Encoding.UTF8.GetBytes (
                            builder.Configuration["Jwt:Key"]!
                        ))
            };
    });
builder.Services.AddAuthorization ();
var app = builder.Build ();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment () )
{
    //  app.MapOpenApi();
}


app.UseAuthentication ();
app.UseAuthorization ();

app.UseMiddleware<GlobalExceptionHandler> ();
app.UseSwagger ();
app.UseSwaggerUI ();

//app.UseHttpsRedirection ();
//app.UseExceptionHandler ("/error");
//app.UseAuthorization ();

app.MapControllers ();

app.Run ();
