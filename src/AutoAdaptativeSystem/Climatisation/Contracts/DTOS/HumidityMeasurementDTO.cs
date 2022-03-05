namespace RoomMonitor.DTOS;

public class HumidityMeasurementDTO
{
    public double Value { get; set; }

    public HumidityUnit Unit { get; set; }
}

public enum HumidityUnit
{
    RELATIVE = 0,
    ABSOLUTE = 1,
}