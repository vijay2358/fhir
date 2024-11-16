using Volo.Abp.Application.Dtos;
using System;

namespace NirvanaHealth.Fhir.Businesses
{
    public class GetBusinessesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public int? Business_ID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessCode { get; set; }
        public string TrackerCode { get; set; }
        public string DBA { get; set; }
        public string FEIN { get; set; }

        public GetBusinessesInput()
        {

        }
    }
}