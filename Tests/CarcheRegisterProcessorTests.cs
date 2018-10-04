using System;
using Xunit;

namespace Tests
{
    public class CarcheRegisterProcessorTests
    {
        [Fact]
        public void CashRegisterProcessor_WithKnownProductBarcode_ProvidesKnownProductPrice()
        {
            var expectedPriceForProduct = "sample price for product";

            var cachRegisterProcessorRendererTestDouble = new CachRegisterProcessorRendererTestDouble();
            var productPriceProviderTestDouble = new ProductPriceProviderTestDouble();

            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cachRegisterProcessorRendererTestDouble);


            var productPriceQuery = new ProductPriceQuery();

            cashRegisterProcessor.Process(productPriceQuery);

            Assert.Equal(expectedPriceForProduct, cachRegisterProcessorRendererTestDouble.LastRenderedResult);
        }
    }


    public class CashRegisterProcessor
    {
        private readonly ProductPriceProviderTestDouble _productPriceProvider;
        private readonly CachRegisterProcessorRendererTestDouble _cachRegisterProcessorRenderer;

        public CashRegisterProcessor(ProductPriceProviderTestDouble productPriceProvider, CachRegisterProcessorRendererTestDouble cachRegisterProcessorRenderer)
        {
            _productPriceProvider = productPriceProvider;
            _cachRegisterProcessorRenderer = cachRegisterProcessorRenderer;
        }

        public void Process(ProductPriceQuery productPriceQuery)
        {
            var result = _productPriceProvider.Query(productPriceQuery);
            _cachRegisterProcessorRenderer.Render(result);

        }
    }

    public class CachRegisterProcessorRendererTestDouble
    {
        public string LastRenderedResult { get; private set; }

        public void Render(object result)
        {
            LastRenderedResult = result.ToString();
        }
    }

    public class ProductPriceProviderTestDouble
    {
        public object Query(ProductPriceQuery productPriceQuery)
        {
            return "sample price for product";
        }
    }

    public class ProductPriceQuery
    {
        public ProductPriceQuery()
        {
        }
    }
}
