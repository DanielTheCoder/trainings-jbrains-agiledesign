namespace Tests.Implementation.HappyZone
{
    public class Barcode
    {
        private readonly string _value;

        public Barcode(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}