using Volo.Abp.Application.Dtos;
using System;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class GetMbrEnrollDetailsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? MbrEnrollDetail_IDMin { get; set; }
        public int? MbrEnrollDetail_IDMax { get; set; }
        public int? MCPMember_IDMin { get; set; }
        public int? MCPMember_IDMax { get; set; }
        public int? BenefitPlan_IDMin { get; set; }
        public int? BenefitPlan_IDMax { get; set; }
        public string Member_ID { get; set; }
        public string Subscriber_ID { get; set; }
        public string PersonCode { get; set; }

        public GetMbrEnrollDetailsInput()
        {

        }
    }
}