namespace NirvanaHealth.Fhir.Businesses
{
    public static class BusinessConsts
    {
        private const string DefaultSorting = "{0}Business_ID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Business." : string.Empty);
        }

    }
}