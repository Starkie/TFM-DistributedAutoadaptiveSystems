namespace Execute.Service.Contracts;

public static class ExecuteServiceConstants
{
    private static readonly string Namespace = "Execute.Service";

    public static readonly string AppName = Namespace; //Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

    public static class Queues
    {
        public const string ExecuteServiceQueue = "execute-service";
    }
}
