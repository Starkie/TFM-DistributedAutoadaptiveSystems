namespace RoomMonitor.DTOS
{
    public class TemperatureMeasurementDTO
    {
        /// <summary>
        ///     The value of the temperature measurement.
        /// </summary>
        public double Value { get; set; }

        public TemperatureUnit Unit { get; set; }
    }

    public enum TemperatureUnit
    {
        KELVIN = 0,
        CELSIUS = 1,
        FAHRENHEIT = 2,
    }
}