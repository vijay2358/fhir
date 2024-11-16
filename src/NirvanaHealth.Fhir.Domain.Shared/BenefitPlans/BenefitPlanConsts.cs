namespace NirvanaHealth.Fhir.BenefitPlans
{
    public static class BenefitPlanConsts
    {
        private const string DefaultSorting = "{0}BenefitPlan_ID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "BenefitPlan." : string.Empty);
        }

    }
}