using Tests.Implementation.HappyZone;

namespace Tests.Implementation.HappyZone
{
    public class BarcodeInterpreter
    {
        public Barcode Interpret(string value)
        {
            return new Barcode(value);
        }
    }
}