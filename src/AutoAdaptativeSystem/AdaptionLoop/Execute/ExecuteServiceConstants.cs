namespace Execute.Service;

public static class ExecuteServiceConstants
{
    private static readonly string Namespace = typeof(Program).Namespace;

    public static readonly string AppName = Namespace; //Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}