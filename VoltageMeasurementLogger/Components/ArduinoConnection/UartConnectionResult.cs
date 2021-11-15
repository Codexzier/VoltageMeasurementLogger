namespace VoltageMeasurementLogger.Components.ArduinoConnection
{
    public class UartConnectionResult
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="message">If the message empty, the property 'Success' becomes true.</param>
        public UartConnectionResult(string message = "")
        {
            this.Message = message;
            this.Success = string.IsNullOrEmpty(message);
        }

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}