using Tests.Implementation.HappyZone;
using Xunit;

namespace Tests.Tests.UglyRealWorld
{
    public class BarcodeTests
    {
        [Fact]
        public void BarcodeTest()
        {
            var barcode = new Barcode("12345");

            Assert.Equal("12345", barcode.ToString());
        }
    }
}