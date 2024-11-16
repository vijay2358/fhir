using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace NirvanaHealth.Fhir;

[DependsOn(
    typeof(FhirDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class FhirApplicationContractsModule : AbpModule
{

}
