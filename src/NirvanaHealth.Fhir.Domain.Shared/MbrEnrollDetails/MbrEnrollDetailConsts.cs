namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public static class MbrEnrollDetailConsts
    {
        private const string DefaultSorting = "{0}MbrEnrollDetail_ID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "MbrEnrollDetail." : string.Empty);
        }

    }
}