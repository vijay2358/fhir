using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using NirvanaHealth.Fhir;
using NirvanaHealth.Fhir.Localization;

namespace NirvanaHealth.FhirExternal;

[DependsOn(
    typeof(FhirApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FhirExternalHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(FhirExternalHttpApiModule).Assembly);
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
