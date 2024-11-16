using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NirvanaHealth.Fhir.Businesses
{
    public class BusinessCreateDto
    {
        [Required]
        public int Business_ID { get; set; }
        [Required]
        public string BusinessName { get; set; }
        public string BusinessCode { get; set; }
        [Required]
        public int? BusinessCat { get; set; }
        [Required]
        public string TrackerCode { get; set; }
        public string DBA { get; set; }
        public string FEIN { get; set; }
    }
}