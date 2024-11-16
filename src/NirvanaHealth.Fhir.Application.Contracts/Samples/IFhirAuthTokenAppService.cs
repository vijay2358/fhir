using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace NirvanaHealth.Fhir.Samples;

public interface IFhirAuthTokenAppService : IApplicationService
{
    
    Task<Fhir> GetToken();
}
