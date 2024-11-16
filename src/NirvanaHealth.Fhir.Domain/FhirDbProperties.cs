namespace NirvanaHealth.Fhir;

public static class FhirDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = "";

    public const string ConnectionStringName = "MCP";
}
