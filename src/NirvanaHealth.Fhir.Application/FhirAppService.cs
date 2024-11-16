using NirvanaHealth.Fhir.Localization;
using Volo.Abp.Application.Services;

namespace NirvanaHealth.Fhir;

public abstract class FhirAppService : ApplicationService
{
    protected FhirAppService()
    {
        LocalizationResource = typeof(FhirResource);
        ObjectMapperContext = typeof(FhirApplicationModule);
    }
}
