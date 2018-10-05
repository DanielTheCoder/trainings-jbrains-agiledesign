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
            var unknownBarcode = GenerateBarcode();

            var productPriceProvider = new InMemoryProductCatalog();

            var productPriceResult = productPriceProvider.GetPrice(unknownBarcode);

            Assert.IsType<UnknownProductPriceResult>(productPriceResult);
        }

        [Fact]
        public void UnknownProductBarcode_WillResultIn_UnknownProduct()
        {
            var unknownBarcode = GenerateBarcode();

            (Barcode, IProductPriceResult)[] listOfKnownPrices = {(GenerateAnotherBarcode(), new KnownProductPriceResult())};
            var productPriceProvider = new InMemoryProductCatalog(listOfKnownPrices);

            var productPriceResult = productPriceProvider.GetPrice(unknownBarcode);

            Assert.IsType<UnknownProductPriceResult>(productPriceResult);
        }

        [Fact]
        public void KnownProductBarcode_WillResultIn_KnownProductPriceResult()
        {
            var knownBarcode = GenerateBarcode();
            var knownProductPriceResult = new KnownProductPriceResult();

            (Barcode, IProductPriceResult)[] listOfKnownPrices = {(knownBarcode, knownProductPriceResult), (GenerateAnotherBarcode(), new UnknownProductPriceResult())};
            var productPriceProvider = new InMemoryProductCatalog(listOfKnownPrices);

            var productPriceResult = productPriceProvider.GetPrice(knownBarcode);

            Assert.Equal(knownProductPriceResult, productPriceResult);
        }

        private static Barcode GenerateBarcode()
        {
            return new Barcode("12345");
        }

        private static Barcode GenerateAnotherBarcode()
        {
            return new Barcode("3456");
        }
    }
}