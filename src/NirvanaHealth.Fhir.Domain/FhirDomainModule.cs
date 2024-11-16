using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace NirvanaHealth.Fhir;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FhirDomainSharedModule)
)]
public class FhirDomainModule : AbpModule
{

}
