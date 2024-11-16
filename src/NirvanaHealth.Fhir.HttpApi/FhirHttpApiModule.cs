using Localization.Resources.AbpUi;
using NirvanaHealth.Fhir.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace NirvanaHealth.Fhir;

[DependsOn(
    typeof(FhirApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FhirHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(FhirHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<FhirResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
