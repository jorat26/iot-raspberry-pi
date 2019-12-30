namespace IoT.RaspberryPi
{
    public interface IGpioPin
    {
        void Open();
        void Close();
        void SetMode(GpioModes mode);
        void Write(GpioValues value);
        GpioValues Read();
    }
}