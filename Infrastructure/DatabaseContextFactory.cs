using KiaKooshar.Application.Features.Interfaces.CurrentUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KiaKooshar.Infrastructure.Persistence;

public class DatabaseContextFactory
    : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext ( string[] args )
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<DatabaseContext> ();
        optionsBuilder.UseSqlServer (
            "Server=.;Database=KiaKooshyarDB;Trusted_Connection=True;TrustServerCertificate=True"
        );
        return new DatabaseContext (
            optionsBuilder.Options,
            new DesignTimeCurrentUserService ()
            );
    }
}
internal class DesignTimeCurrentUserService : ICurrentUserService
{
    public long? UserId => null;
    public string? Username => null;
    public string? IP => null;
}