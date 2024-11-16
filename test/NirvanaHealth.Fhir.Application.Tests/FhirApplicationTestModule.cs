using Volo.Abp.Modularity;

namespace NirvanaHealth.Fhir;

[DependsOn(
    typeof(FhirApplicationModule),
    typeof(FhirDomainTestModule)
    )]
public class FhirApplicationTestModule : AbpModule
{

}
