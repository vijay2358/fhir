using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.EntityFrameworkCore;

public class FhirHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<FhirHttpApiHostMigrationsDbContext>
{
    public FhirHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<FhirHttpApiHostMigrationsDbContext>()
            .UseOracle(configuration.GetConnectionString("Fhir"));

        return new FhirHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
