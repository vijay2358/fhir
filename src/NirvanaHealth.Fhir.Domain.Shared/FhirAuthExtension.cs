using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace NirvanaHealth.Fhir
{
    public static class FhirAuthExtension
    {

        public static async Task<Fhir> RequestTokenAsync(Fhir fhir, string proxyserver = "")
        {
            //try
            //{
            //if (HasValidAccessToken(fhir))
            //{
            //    return fhir;
            //}
            var httpClientHandler = new HttpClientHandler
            {
                UseProxy = false,
                DefaultProxyCredentials = CredentialCache.DefaultNetworkCredentials,
                Proxy = new WebProxy(proxyserver)
            };


            var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);

            //TODO : Enable disco logic
            //var disco = await client.GetDiscoveryDocumentAsync(fhir.AuthUrl);
            //if (disco.IsError) throw new Exception(disco.Error);

            //if (string.IsNullOrWhiteSpace(disco.TokenEndpoint)) 
            //{
            //}               

            // Finally, create the HTTP client object
            //var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = fhir.TokenUrl,// TODO: not the auth url.. please make sure token url need to pass

                ClientId = fhir.ClientId,
                ClientSecret = fhir.ClientSecret,
                Scope = fhir.Scope,
                Resource = new List<string>() { fhir.ApiUrl }
            });

            if (response.IsError)
            {
                fhir.HasError = response.IsError;
                fhir.ErrorMsg = response.Error;
                return fhir;
            }
            //throw new BusinessException(response.Error);


            if (response.AccessToken != null)
            {
                fhir.AccessToken = response.AccessToken;
            }

            //catch (Exception ex)
            //{
            //    new BusinessException(ex.Message);
            //}
            //finally { 

            //}
            return fhir;
        }

        public static Boolean  HasValidAccessToken(Fhir fhir) //TODO: Add more logic for more validation 
        { 
            if (string.IsNullOrEmpty(fhir.AccessToken))
            {
                return false;
            }
            return true;

        }

        public static async Task<string> CallServiceAsync(Fhir fhir, string apiUrl)
        {
            var response = string.Empty; 
            try
            {            
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(fhir.ApiUrl);
                    if (fhir.AccessToken is not null) httpClient.SetBearerToken(fhir.AccessToken);
                    using (HttpResponseMessage rep = await httpClient.GetAsync(apiUrl))
                    {
                        string apiResponse = await rep.Content.ReadAsStringAsync();
                        response = apiResponse;                        
                    }
                }
                    //Console.WriteLine(response.PrettyPrintJson());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            finally {
               
            }
            //return response.PrettyPrintJson();
            return response;
        }

        //public static async Task<string> CallFhirServiceAsync(Fhir fhir, string apiUrl)
        //{
        //    var response = string.Empty;
        //    try
        //    {
        //        const string Endpoint = "https://ontoserver.csiro.au/stu3-latest";
        //        var client = new FhirClient(Endpoint);

        //        //uri for the value set to be searched, and text filter         
        //        var filter = new FhirString("inr");
        //        var vs_uri = new FhirUri("http://snomed.info/sct?fhir_vs=refset/1072351000168102");

        //        ValueSet result = client.ExpandValueSet(vs_uri, filter);

        //        //Write out the display term of the first result.
        //        Console.WriteLine(result.Expansion.Contains.FirstOrDefault().Display);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.InnerException);
        //    }
        //    finally
        //    {

        //    }
        //    //return response.PrettyPrintJson();
        //    return response;
        //}
    }
}


