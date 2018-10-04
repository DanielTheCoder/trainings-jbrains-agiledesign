using Xunit;

namespace Tests.HappyZone
{
    public class SellOneItemTests
    {
        [Fact]
        public void KnownProductBarcode_WillResultIn_ProductPrice()
        {
            var barcodeQuery = new BarcodeQuery();
            var productPriceResult = new ProductPriceResult();

            var cachRegisterProcessorRendererTestDouble = new CashRegisterProcessorRendererTestDouble();
            var productPriceProviderTestDouble = new ProductPriceProviderTestDouble(productPriceResult);
            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cachRegisterProcessorRendererTestDouble);

            cashRegisterProcessor.OnBarcode(barcodeQuery);

            Assert.Equal(productPriceResult, cachRegisterProcessorRendererTestDouble.ResultToRender);
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
