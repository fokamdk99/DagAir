namespace DagAir.PolicyNode.PolicyEvaluator
{
    public static class MessageConsts
    {
        public const string TemperatureTooHigh = "Current temperature level is too high. Consider turning off the heat.";
        public const string TemperatureTooLow = "Current temperature level is too low. Consider turning on the heat.";

        public const string IlluminanceTooHigh =
            "Current illuminance level is too high. Consider switching off the lights.";

        public const string IlluminanceTooLow =
            "Current illuminance level is too low. Consider switching on the lights.";

        public const string HumidityTooHigh = "Current humidity level is too high. Consider opening the windows.";
        public const string HumidityTooLow = "Current humidity level is too low. Consider closing the windows.";
        public const string MeasurementsInOrder = "Current room condition is compliant with expectations.";
    }
}