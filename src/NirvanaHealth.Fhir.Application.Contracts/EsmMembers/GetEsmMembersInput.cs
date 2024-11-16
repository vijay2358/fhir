using NirvanaHealth.Fhir;
using Volo.Abp.Application.Dtos;
using System;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public class GetEsmMembersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? MCPMember_IDMin { get; set; }
        public int? MCPMember_IDMax { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MIddleName { get; set; }
        public string Suffix { get; set; }
        public string PreFix { get; set; }
        public DateTime? DOBMin { get; set; }
        public DateTime? DOBMax { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateofDeathMin { get; set; }
        public DateTime? DateofDeathMax { get; set; }
        public Race? Race { get; set; }

        public GetEsmMembersInput()
        {

        }
    }
}