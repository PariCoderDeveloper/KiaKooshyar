using KiaKooshar.Application.DTOs.Commons;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Domain.Enums;
using Serilog;

namespace KiaKooshar.Infrastructure.Persistence.Logger
{
    public class BaseLogger : IBaseLogger
    {
        public void Logging ( LogOptionsDTO logOptionsDTO )
        {
            var result = new BaseLogger ();
            switch ( logOptionsDTO.Level )
            {
                case LogLevel.Verbose:
                    Log.ForContext ("IP", logOptionsDTO.IP)
                        .Information (
                            logOptionsDTO.Message!,
                            logOptionsDTO.Args);
                    break;

                case LogLevel.Debug:
                    Log.ForContext ("IP", logOptionsDTO.IP)
                       .Information (
                           logOptionsDTO.Message!,
                           logOptionsDTO.Args);
                    break;

                case LogLevel.Information:
                    Log.ForContext ("IP", logOptionsDTO.IP)
                       .Information (
                           logOptionsDTO.Message!,
                           logOptionsDTO.Args);
                    break;

                case LogLevel.Warning:
                    Log.ForContext ("IP", logOptionsDTO.IP)
                        .Information (
                            logOptionsDTO.Message!,
                            logOptionsDTO.Args);
                    break;

                case LogLevel.Error:
                    Log.ForContext ("IP", logOptionsDTO.IP)
                        .Information (
                            logOptionsDTO.Message!,
                            logOptionsDTO.Args);
                    break;

                case LogLevel.Fatal:
                    Log.ForContext ("IP", logOptionsDTO.IP)
                        .Information (
                            logOptionsDTO.Message!,
                            logOptionsDTO.Args);
                    break;
            }
        }
    }
}