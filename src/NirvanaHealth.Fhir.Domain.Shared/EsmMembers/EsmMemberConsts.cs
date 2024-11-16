namespace NirvanaHealth.Fhir.EsmMembers
{
    public static class EsmMemberConsts
    {
        private const string DefaultSorting = "{0}MCPMember_ID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EsmMember." : string.Empty);
        }

    }
}