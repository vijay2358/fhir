using NirvanaHealth.Fhir;
using System;
using Volo.Abp.Application.Dtos;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public class EsmMemberDto : EntityDto<string>
    {
        public int MCPMember_ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MIddleName { get; set; }
        public string Suffix { get; set; }
        public string PreFix { get; set; }
        public DateTime? DOB { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateofDeath { get; set; }
        public Race? Race { get; set; }
    }
}