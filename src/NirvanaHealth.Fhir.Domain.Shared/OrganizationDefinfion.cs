using System;
using System.Collections.Generic;
using System.Text;

namespace NirvanaHealth.Fhir
{
    /// <summary>
    /// http://hl7.org/fhir/R4/valueset-organization-type.html
    /// </summary>
    public enum OrganizationType
    {       
        prov,
        dept,
        team,
        govt,
        ins,
        pay,
        edu,
        reli,
        crs,
        cg,
        bus,
        other
    }

    //public class OrganizationDefinfions
    //{
    //    public List<OrganizationDefinfion> Items { get; set; }
    //    public OrganizationDefinfions()
    //    {
    //        this.Items = new List<OrganizationDefinfion>();
    //    }
    //}

    public class OrganizationDefinfion
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public OrganizationDefinfion(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        public OrganizationDefinfion(string code, string name, string definition)
        {
            this.Code = code;
            this.Name = name;
            this.Definition = definition;
        }
    }
}
