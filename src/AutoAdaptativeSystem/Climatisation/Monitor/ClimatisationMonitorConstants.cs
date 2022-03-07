namespace Climatisation.Monitor;

using System;

public static class ClimatisationMonitorConstants
{
    private static readonly string Namespace = typeof(Program).Namespace;

    public static readonly string AppName = Namespace; //Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

    public static readonly Guid MonitorId = new Guid("92bb183c-c94f-41b4-b1b8-916fec3e5db8");
}
