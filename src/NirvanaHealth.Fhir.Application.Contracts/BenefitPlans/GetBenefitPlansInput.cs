using Volo.Abp.Application.Dtos;
using System;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class GetBenefitPlansInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? BenefitPlan_IDMin { get; set; }
        public int? BenefitPlan_IDMax { get; set; }
        public string BenefitName { get; set; }
        public string BenefitCode { get; set; }
        public string Description { get; set; }
        public string VersionNbr { get; set; }

        public GetBenefitPlansInput()
        {

        }
    }
}