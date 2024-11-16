using NirvanaHealth.Fhir.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace NirvanaHealth.Fhir;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(FhirEntityFrameworkCoreTestModule)
    )]
public class FhirDomainTestModule : AbpModule
{

}
