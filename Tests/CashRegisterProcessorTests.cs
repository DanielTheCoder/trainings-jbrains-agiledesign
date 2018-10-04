using System;
using Xunit;

namespace Tests
{
    public class CashRegisterProcessorTests
    {
        [Fact]
        public void CashRegisterProcessor_WithKnownProductBarcode_ProvidesKnownProductPrice()
        {
            var productPriceResult = new ProductPriceResult();
            var productPriceQuery = new ProductPriceQuery();

            var cachRegisterProcessorRendererTestDouble = new CachRegisterProcessorRendererTestDouble();
            var productPriceProviderTestDouble = new ProductPriceProviderTestDouble(productPriceResult);
            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cachRegisterProcessorRendererTestDouble);


            cashRegisterProcessor.Process(productPriceQuery);

            Assert.Equal(productPriceResult, cachRegisterProcessorRendererTestDouble.ResultToRender);
        }
    }

    public class CachRegisterProcessorRendererTestDouble : ICachRegisterProcessorRenderer
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

        public ProductPriceResult Query(ProductPriceQuery productPriceQuery)
        {
            return _productPriceResult;
        }
    }

}
