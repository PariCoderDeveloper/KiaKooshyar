using AutoMapper;
using KiaKooshar.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Reflection;

namespace KiaKooshar.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices
            (
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddMediatR (typeof (ApplicationServicesRegistration).Assembly);
            services.AddSingleton<IMapper> (sp =>
            {
                var config = new MapperConfiguration (
                    cfg =>
                    {
                        cfg.AddMaps (Assembly.GetExecutingAssembly ());
                    },
                    NullLoggerFactory.Instance
                );

                return config.CreateMapper ();
            });
            //services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());
            services.AddTransient (typeof (IPipelineBehavior<,>), typeof (LoggingBehavior<,>));

            ConfigureSerilog (configuration);
        }
        private static void ConfigureSerilog ( IConfiguration configuration )
        {
            var sqlConnectionString = configuration.GetConnectionString ("DefaultConnection");

            var columnOptions = new ColumnOptions ();

            columnOptions.Store.Remove (StandardColumn.Properties);
            columnOptions.Store.Add (StandardColumn.Properties);

            columnOptions.Properties.ColumnName = "Properties";
            columnOptions.Properties.DataType = SqlDbType.NVarChar;

            Log.Logger = new LoggerConfiguration ()
                .MinimumLevel.Debug ()
                .MinimumLevel.Override ("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext ()
                .Enrich.WithMachineName ()
                .Enrich.WithThreadId ()
                .Enrich.WithProcessId ()

                .WriteTo.MSSqlServer (
                    connectionString: sqlConnectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        SchemaName = "dbo",
                        AutoCreateSqlTable = true,

                        BatchPostingLimit = 50,
                        BatchPeriod = TimeSpan.FromSeconds (5)
                    },
                    columnOptions: columnOptions
                )
                .CreateLogger ();
        }
    }
}
