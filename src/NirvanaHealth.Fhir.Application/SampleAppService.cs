using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NirvanaHealth.Fhir.Samples;

public class FhirAuthAppService : FhirAppService
{
    ClientConfiguration _externalClients;
    public FhirAuthAppService(ClientConfiguration externalClients)
    {
        _externalClients = externalClients;
    }

    [Authorize]
    public void GetAccessToken()
    {

    }
}
