namespace Monitoring.Service.DTOS;

using System;

public class MeasurementDTO
{
    public Guid ProbeId { get; set; }

    public Property Property { get; set; }
}

public class Property
{
    public string Key { get; set; }

    public string Value { get; set; }
}
