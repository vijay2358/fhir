

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.Health.Fhir.Client;
//using Newtonsoft.Json;
using NirvanaHealth.Fhir.Localization;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace NirvanaHealth.Fhir;

public abstract class FhirController : AbpControllerBase
{
    protected FhirController()
    {
        LocalizationResource = typeof(FhirResource);
    }
    public virtual async Task<string> FhirResponse<T>(T result)
    {
        var options = new JsonSerializerOptions().ForFhir(typeof(T).Assembly).Pretty();
        string fhirResponseJson = JsonSerializer.Serialize(result, options);

        //var fhirSerializer = new FhirJsonSerializer();
        //fhirSerializer.SerializeToString( result)
        return fhirResponseJson;
    }
    //public virtual async Task<string> FhirResponseById<T>(FhirClient fhirClient, ResourceType resoruceType, string resourceId)
    //{
    //     T resourceEntity = await fhirClient.ReadAsync<T>(resoruceType, resourceId);
    //    //Hl7.Fhir.Model.Organization organization = await fhirClient.ReadAsync<Hl7.Fhir.Model.Organization>(resoruceType, resourceId);
    //    return await FhirResponse<T>(resourceEntity);
    //}

    //public class FhirResourceConverter<T> : JsonConverter<T> where T : Hl7.Fhir.Model.Base
    //{
    //    public override T ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    //    {
    //        var valStr = Newtonsoft.Json.Linq.JRaw.Create(reader).ToString();
    //        return FhirJsonNode.Parse(valStr).ToPoco<T>();
    //    }

    //    public override void WriteJson(JsonWriter writer, T? value, Newtonsoft.Json.JsonSerializer serializer)
    //    {
    //        var fhirSerializer = new FhirJsonSerializer();
    //        writer.WriteRaw(fhirSerializer.SerializeToString(value));
    //    }
    //}


}
