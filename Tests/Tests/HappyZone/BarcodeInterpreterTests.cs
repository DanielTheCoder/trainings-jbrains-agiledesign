using Tests.Implementation.HappyZone;
using Xunit;

namespace Tests.Tests.HappyZone
{
    public class BarcodeInterpreterTests
    {
        private readonly BarcodeInterpreter _barcodeInterpreter;

        public BarcodeInterpreterTests()
        {
            _barcodeInterpreter = new BarcodeInterpreter();
        }

        [Fact]
        public void Interpret_StringInput_ResultsInBarcode()
        {
            var barcode = _barcodeInterpreter.Interpret("12345");

            Assert.IsType<Barcode>(barcode);
        }
    }
}
