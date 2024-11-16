using NirvanaHealth.Fhir.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NirvanaHealth.Fhir.Permissions;

public class FhirPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FhirPermissions.GroupName, L("Permission:Fhir"));

        var esmMemberPermission = myGroup.AddPermission(FhirPermissions.EsmMembers.Default, L("Permission:EsmMembers"));
        esmMemberPermission.AddChild(FhirPermissions.EsmMembers.Create, L("Permission:Create"));
        esmMemberPermission.AddChild(FhirPermissions.EsmMembers.Edit, L("Permission:Edit"));
        esmMemberPermission.AddChild(FhirPermissions.EsmMembers.Delete, L("Permission:Delete"));

        var businessPermission = myGroup.AddPermission(FhirPermissions.Businesses.Default, L("Permission:Businesses"));
        businessPermission.AddChild(FhirPermissions.Businesses.Create, L("Permission:Create"));
        businessPermission.AddChild(FhirPermissions.Businesses.Edit, L("Permission:Edit"));
        businessPermission.AddChild(FhirPermissions.Businesses.Delete, L("Permission:Delete"));

        var benefitPlanPermission = myGroup.AddPermission(FhirPermissions.BenefitPlans.Default, L("Permission:BenefitPlans"));
        benefitPlanPermission.AddChild(FhirPermissions.BenefitPlans.Create, L("Permission:Create"));
        benefitPlanPermission.AddChild(FhirPermissions.BenefitPlans.Edit, L("Permission:Edit"));
        benefitPlanPermission.AddChild(FhirPermissions.BenefitPlans.Delete, L("Permission:Delete"));

        var mbrEnrollDetailPermission = myGroup.AddPermission(FhirPermissions.MbrEnrollDetails.Default, L("Permission:MbrEnrollDetails"));
        mbrEnrollDetailPermission.AddChild(FhirPermissions.MbrEnrollDetails.Create, L("Permission:Create"));
        mbrEnrollDetailPermission.AddChild(FhirPermissions.MbrEnrollDetails.Edit, L("Permission:Edit"));
        mbrEnrollDetailPermission.AddChild(FhirPermissions.MbrEnrollDetails.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FhirResource>(name);
    }
}