using Tests.Implementation.HappyZone;

namespace Tests.Implementation.HappyZone
{
    public interface IBarcodeInterpreter
    {
        Barcode Interpret(string value);
    }

    public class BarcodeInterpreter : IBarcodeInterpreter
    {
        public Barcode Interpret(string value)
        {
            return new Barcode(value);
        }
    }
}