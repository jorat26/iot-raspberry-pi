namespace IoT.RaspberryPi
{
    public interface IGpioStatus
    {
        int PinNumber { get; }

        bool IsOpen { get; }

        GpioModes Mode { get; }
    }
}