using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace NirvanaHealth.Fhir;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FhirHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class FhirConsoleApiClientModule : AbpModule
{

}
