using System;
using Volo.Abp.Application.Dtos;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MbrEnrollDetailDto : EntityDto<string>
    {
        public int MbrEnrollDetail_ID { get; set; }
        public int MCPMember_ID { get; set; }
        public int? BenefitPlan_ID { get; set; }
        public string Member_ID { get; set; }
        public string Subscriber_ID { get; set; }
        public string PersonCode { get; set; }
    }
}