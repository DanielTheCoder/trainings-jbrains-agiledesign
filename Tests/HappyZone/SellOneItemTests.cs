using Xunit;

namespace Tests.HappyZone
{
    /*
     * overall design idea
     * dmz
     * - stdin => BarcodeQuery (interface for different barcode types?)
     * - CashRegisterProcessorRenderer =>
     *   - ProductPriceResult => stdout (interface of different result types like unknownproduct vs. knownproduct => dmz is responsible to react how to render)
     * - ProductPriceProvider
     *   - Getting price from some storage based on barcode
     *
     *
     */

    public class SellOneItemTests
    {
        [Fact]
        public void KnownProductBarcode_WillResultIn_ProductPrice()
        {
            var barcodeQuery = new BarcodeQuery();
            var productPriceResult = new ProductPriceResult();

            var cashRegisterProcessorRendererTestDouble = new CashRegisterProcessorRendererTestDouble();
            var productPriceProviderTestDouble = new ProductPriceProviderTestDouble(productPriceResult);
            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cashRegisterProcessorRendererTestDouble);

            cashRegisterProcessor.OnBarcode(barcodeQuery);

            Assert.Equal(productPriceResult, cashRegisterProcessorRendererTestDouble.ResultToRender);
        }
    }

    public class CashRegisterProcessorRendererTestDouble : ICachRegisterProcessorRenderer
    {
        public ProductPriceResult ResultToRender { get; private set; }

        public void Render(ProductPriceResult result)
        {
            ResultToRender = result;
        }
    }


    public class ProductPriceProviderTestDouble : IProductPriceProvider
    {
        private readonly ProductPriceResult _productPriceResult;

        public ProductPriceProviderTestDouble(ProductPriceResult productPriceResult)
        {
            _productPriceResult = productPriceResult;
        }

        public ProductPriceResult Query(BarcodeQuery barcodeQuery)
        {
            return _productPriceResult;
        }
    }

}
