using NirvanaHealth.Fhir;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public class EsmMemberCreateDto
    {
        [Required]
        public int MCPMember_ID { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MIddleName { get; set; }
        public string Suffix { get; set; }
        public string PreFix { get; set; }
        public DateTime? DOB { get; set; }
        [Required]
        public Gender Gender { get; set; } = ((Gender[])Enum.GetValues(typeof(Gender)))[0];
        public DateTime? DateofDeath { get; set; }
        public Race? Race { get; set; }
    }
}