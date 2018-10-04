using Tests.Implementation;
using Xunit;

namespace Tests.HappyZone.Dmz
{
    public class InMemoryProductPriceProviderTests
    {
        [Fact]
        public void EmptyProductPriceProvider_WillResultIn_UnknownProduct()
        {
            var unknownBarcode = new BarcodeQuery();

            var productPriceProvider = new InMemoryProductPriceProvider();

            var productPriceResult = productPriceProvider.Query(unknownBarcode);

            Assert.IsType<UnknownProductPriceResult>(productPriceResult);
        }

        [Fact]
        public void UnknownProductBarcode_WillResultIn_UnknownProduct()
        {
            var unknownBarcode = new BarcodeQuery();

            (BarcodeQuery, IProductPriceResult)[] listOfKnownPrices = { (new BarcodeQuery(), new KnownProductPriceResult()) };
            var productPriceProvider = new InMemoryProductPriceProvider(listOfKnownPrices);

            var productPriceResult = productPriceProvider.Query(unknownBarcode);

            Assert.IsType<UnknownProductPriceResult>(productPriceResult);
        }

        [Fact]
        public void KnownProductBarcode_WillResultIn_KnownProductPriceResult()
        {
            var knownBarcode = new BarcodeQuery();
            var knownProductPriceResult = new KnownProductPriceResult();

            (BarcodeQuery, IProductPriceResult)[] listOfKnownPrices = {(knownBarcode, knownProductPriceResult), (new BarcodeQuery(), new UnknownProductPriceResult())};
            var productPriceProvider = new InMemoryProductPriceProvider(listOfKnownPrices);

            var productPriceResult = productPriceProvider.Query(knownBarcode);

            Assert.Equal(knownProductPriceResult, productPriceResult);
        }
    }
}