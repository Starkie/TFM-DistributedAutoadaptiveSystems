namespace RoomMonitor.DTOS
{
    using System;
    using MediatR;

    public class TemperatureMeasurementDTO : IRequest
    {
        /// <summary>
        ///     The value of the temperature measurement.
        /// </summary>
        public double Value { get; set; }

        public TemperatureUnit Unit { get; set; }

        public Guid ProbeId { get; set; }

        public DateTime DateTime { get; set; }
    }

    public enum TemperatureUnit
    {
        KELVIN = 0,
        CELSIUS = 1,
        FAHRENHEIT = 2,
    }
}
