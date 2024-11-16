using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace NirvanaHealth.Fhir;

[DependsOn(
    typeof(FhirApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class FhirHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(FhirApplicationContractsModule).Assembly,
            FhirRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FhirHttpApiClientModule>();
        });
    }
}
