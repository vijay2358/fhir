using Volo.Abp.Reflection;

namespace NirvanaHealth.Fhir.Permissions;

public class FhirPermissions
{
    public const string GroupName = "Fhir";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FhirPermissions));
    }

    public class EsmMembers
    {
        public const string Default = GroupName + ".EsmMembers";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class Businesses
    {
        public const string Default = GroupName + ".Businesses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class BenefitPlans
    {
        public const string Default = GroupName + ".BenefitPlans";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class MbrEnrollDetails
    {
        public const string Default = GroupName + ".MbrEnrollDetails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}