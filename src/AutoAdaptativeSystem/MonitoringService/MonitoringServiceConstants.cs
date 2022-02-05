namespace MonitoringService;

public static class MonitoringServiceConstants
{
    private static readonly string Namespace = typeof(Program).Namespace;

    public static readonly string AppName = Namespace; //Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}
