using System;
using System.Collections.Generic;
using System.Text;

namespace NirvanaHealth.Fhir
{
    public class ClientConfiguration //: IClientConfiguration
    {
        //public Business Selected { get; set; }
        public ClientConfiguration()
        {
            this.Fhir = new List<Fhir>();
        }
        public List<Fhir> Fhir { get; set; }
    }


    public class Fhir
    {

        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string AuthUrl { get; set; }
        public string TokenUrl { get; set; }
        public string RequireHttpsMetadata { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string AccessToken { get; set; }
        public string ApiUrl    { get; set; }
        public bool HasError { get; set; } = false;
        public string ErrorMsg { get; set; } = string.Empty;
    }
}
