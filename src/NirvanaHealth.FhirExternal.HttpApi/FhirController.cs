using Hl7.Fhir.Serialization;
using NirvanaHealth.Fhir.Localization;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace NirvanaHealth.FhirExternal;

public abstract class FhirExternalController : AbpControllerBase
{
    protected FhirExternalController()
    {
        LocalizationResource = typeof(FhirResource);
    }
    public virtual async Task<string> FhirResponse<T>(T result)
    {
        var options = new JsonSerializerOptions().ForFhir(typeof(T).Assembly).Pretty();
        string fhirResponseJson = JsonSerializer.Serialize(result, options);
        return fhirResponseJson;
    }
}