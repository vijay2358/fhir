using Volo.Abp;
using Volo.Abp.MongoDB;

namespace NirvanaHealth.Fhir.MongoDB;

public static class FhirMongoDbContextExtensions
{
    public static void ConfigureFhir(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
