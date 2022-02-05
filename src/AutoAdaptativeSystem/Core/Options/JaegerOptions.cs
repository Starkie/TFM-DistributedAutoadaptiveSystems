namespace KnowledgeService.Options;

public class JaegerOptions
{
    public const string ConfigurationPath = "Jaeger";

    public string Host { get; set; }

    public int Port { get; set; }
}
