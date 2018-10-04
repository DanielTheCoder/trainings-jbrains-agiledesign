using Tests.Implementation;
using Xunit;

namespace Tests.HappyZone
{
    public class InMemoryProductPriceProviderTests
    {
        [Fact]
        public void UnknownProductBarcode_WillResultIn_UnknownProduct()
        {
            var unknownBarcode = new BarcodeQuery();

            var productPriceProviderTestDouble = new InMemoryProductPriceProvider();

            var productPriceResult = productPriceProviderTestDouble.Query(unknownBarcode);

            Assert.IsType<UnknownProductPriceResult>(productPriceResult);
        }

        [Fact]
        public void KnownProductBarcode_WillResultIn_KnownProduct()
        {
            var knownBarcodeQuery = new BarcodeQuery();
            var knownProductPriceResult = new KnownProductPriceResult();

            var listOfKnownPrices = new[] { (knownBarcodeQuery, knownProductPriceResult) };
            var productPriceProviderTestDouble = new InMemoryProductPriceProvider(listOfKnownPrices);

            var productPriceResult = productPriceProviderTestDouble.Query(knownBarcodeQuery);

            Assert.Equal(knownProductPriceResult, productPriceResult);
        }
    }
}