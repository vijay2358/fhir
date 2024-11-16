using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace NirvanaHealth.Fhir.Businesses
{
    public class BusinessDto : EntityDto<string>
    {
        public int Business_ID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessCode { get; set; }
        public int? BusinessCat { get; set; }
        public string TrackerCode { get; set; }
        public string DBA { get; set; }
        public string FEIN { get; set; }
        public List<AddressesDto> Addresses { get; set; }

    }

    public class AddressesDto
    {


        public int Address_ID { get; set; }
        public AddressTypeDto Address_Type { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

    }

    public class AddressTypeDto
    {

        public int Type_ID { get; set; }
        public string Name { get; set; }
    }
}