using Tests.Implementation.Dmz;
using Tests.Implementation.HappyZone;
using Xunit;

namespace Tests.Tests.Dmz
{
    public class InMemoryProductPriceProviderTests
    {
        [Fact]
        public void EmptyProductPriceProvider_WillResultIn_UnknownProduct()
        {
            var unknownBarcode = new Barcode();

            var productPriceProvider = new InMemoryProductCatalog();

            var productPriceResult = productPriceProvider.GetPrice(unknownBarcode);

            Assert.IsType<UnknownProductPriceResult>(productPriceResult);
        }

        [Fact]
        public void UnknownProductBarcode_WillResultIn_UnknownProduct()
        {
            var unknownBarcode = new Barcode();

            (Barcode, IProductPriceResult)[] listOfKnownPrices = { (new Barcode(), new KnownProductPriceResult()) };
            var productPriceProvider = new InMemoryProductCatalog(listOfKnownPrices);

            var productPriceResult = productPriceProvider.GetPrice(unknownBarcode);

            Assert.IsType<UnknownProductPriceResult>(productPriceResult);
        }

        [Fact]
        public void KnownProductBarcode_WillResultIn_KnownProductPriceResult()
        {
            var knownBarcode = new Barcode();
            var knownProductPriceResult = new KnownProductPriceResult();

            (Barcode, IProductPriceResult)[] listOfKnownPrices = {(knownBarcode, knownProductPriceResult), (new Barcode(), new UnknownProductPriceResult())};
            var productPriceProvider = new InMemoryProductCatalog(listOfKnownPrices);

            var productPriceResult = productPriceProvider.GetPrice(knownBarcode);

            Assert.Equal(knownProductPriceResult, productPriceResult);
        }
    }
}