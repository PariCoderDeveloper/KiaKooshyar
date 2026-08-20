using AutoMapper;
using FluentValidation;
using KiaKooshar.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
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
            #region MediatR
            services.AddMediatR (typeof (ApplicationServicesRegistration).Assembly);
            #endregion
            #region Mapper
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
            #endregion
            #region Behaviors
            services.AddTransient (typeof (IPipelineBehavior<,>), typeof (LoggingBehavior<,>));
            services.AddTransient (typeof (IPipelineBehavior<,>), typeof (ValidationBehavior<,>));
            #endregion
            #region FluentValidation
            services.AddValidatorsFromAssembly (typeof (ApplicationServicesRegistration).Assembly);
            #endregion
            #region Serilog
            ConfigureSerilog (configuration);
            #endregion
            services.AddHttpContextAccessor ();
        }
        private static void ConfigureSerilog ( IConfiguration configuration )
        {
            var sqlConnectionString = configuration.GetConnectionString ("DefaultConnection");

            var columnOptions = new ColumnOptions ();

            columnOptions.Store.Remove (StandardColumn.Properties);
            columnOptions.Store.Add (StandardColumn.Properties);

            columnOptions.Properties.ColumnName = "Properties";
            columnOptions.Properties.DataType = SqlDbType.NVarChar;

            columnOptions.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                {
                    ColumnName = "IP",
                    DataLength = 15,
                    PropertyName = "IP",
                }
            };

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
                .WriteTo.Seq ("http://localhost:5244")
                .CreateLogger ();
        }
    }
}
