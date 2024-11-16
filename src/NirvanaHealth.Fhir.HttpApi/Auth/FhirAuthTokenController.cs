using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace NirvanaHealth.Fhir.Samples;

[Area(FhirRemoteServiceConsts.ModuleName)]
[RemoteService(Name = FhirRemoteServiceConsts.RemoteServiceName)]
[Route("api/Fhir/auth")]
public class AuthTokenController : FhirController
{
    private readonly IFhirAuthTokenAppService  _fhirAuthTokenAppService;

    public AuthTokenController(IFhirAuthTokenAppService fhirAuthTokenAppService)
    {
        _fhirAuthTokenAppService = fhirAuthTokenAppService;
    }

    [HttpGet]
    [Route("GetToken")]
    [Authorize]
    public async Task<ActionResult> GetToken()
    {
        var fhir = await _fhirAuthTokenAppService.GetToken();
        if (!fhir.HasError)
        {
            return Ok(fhir.AccessToken);
        }
        else
        {
            return BadRequest(fhir.ErrorMsg);
        }
    }
}
