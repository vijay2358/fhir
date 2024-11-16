using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MbrEnrollDetailCreateDto
    {
        [Required]
        public int MbrEnrollDetail_ID { get; set; }
        [Required]
        public int MCPMember_ID { get; set; }
        public int? BenefitPlan_ID { get; set; }
        public string Member_ID { get; set; }
        public string Subscriber_ID { get; set; }
        public string PersonCode { get; set; }
    }
}