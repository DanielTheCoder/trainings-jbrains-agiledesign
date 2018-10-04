using Tests.Implementation;
using Xunit;

namespace Tests.HappyZone
{
    /*
     * overall design idea
     * dmz
     * - stdin => BarcodeQuery (interface for different barcode types?)
     * - CashRegisterProcessorRenderer =>
     *   - IProductPriceResult => stdout (interface of different result types like unknownproduct vs. knownproduct => dmz is responsible to react how to render)
     *   - is IProductPriceResult a message ???
     * - InMemoryProductPriceProvider
     *   - Getting price from some storage based on barcode
     *
     *
     */

    public class SellOneItemTests
    {
        [Fact]
        public void End2End_KnownProductBarcode_WillResultIn_ProductPrice()
        {
            var knownBarcode = new BarcodeQuery();
            var knownProductPriceResult = new KnownProductPriceResult();

            var cashRegisterProcessorRendererTestDouble = new CashRegisterProcessorRendererTestDouble();
            var productPriceProviderTestDouble = new ProductPriceProviderTestDouble(knownProductPriceResult);
            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cashRegisterProcessorRendererTestDouble);

            cashRegisterProcessor.OnBarcode(knownBarcode);

            Assert.Equal(knownProductPriceResult, cashRegisterProcessorRendererTestDouble.ResultToRender);
        }

        [Fact]
        public void End2End_UnknownProductBarcode_WillResultIn_UnknownProduct()
        {
            //discuss because behaviour of result known/unknow and renderer ????

            var unknownBarcode = new BarcodeQuery();
            var unknownProductPriceResult = new UnknownProductPriceResult();

            var cashRegisterProcessorRendererTestDouble = new CashRegisterProcessorRendererTestDouble();
            var productPriceProviderTestDouble = new ProductPriceProviderTestDouble(unknownProductPriceResult);
            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cashRegisterProcessorRendererTestDouble);

            cashRegisterProcessor.OnBarcode(unknownBarcode);

            Assert.Equal(unknownProductPriceResult, cashRegisterProcessorRendererTestDouble.ResultToRender);
        }

        public class CashRegisterProcessorRendererTestDouble : ICashRegisterProcessorRenderer
        {
            public IProductPriceResult ResultToRender { get; private set; }

            public void Render(IProductPriceResult result)
            {
                ResultToRender = result;
            }
        }


        public class ProductPriceProviderTestDouble : IProductPriceProvider
        {
            private readonly IProductPriceResult _productPriceResult;

            public ProductPriceProviderTestDouble(IProductPriceResult productPriceResult)
            {
                _productPriceResult = productPriceResult;
            }

            public IProductPriceResult Query(BarcodeQuery barcodeQuery)
            {
                return _productPriceResult;
            }
        }
    }
}
