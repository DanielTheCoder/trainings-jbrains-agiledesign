using Tests.Implementation.HappyZone;
using Xunit;

namespace Tests.Tests
{
    /*
     * overall design idea
     * dmz
     * - stdin => Barcode (interface for different barcode types?)
     * - CashRegisterProcessorRenderer =>
     *   - IProductPriceResult => stdout (interface of different result types like unknownproduct vs. knownproduct => dmz is responsible to react how to render)
     *   - is IProductPriceResult a message ???
     * - InMemoryProductPriceProvider
     *   - Getting price from some storage based on barcode
     *
     *
     */


     /// <summary>
     /// colaboration tests => shows what's possible
     /// contract tests show that's possible, it works
     ///
     /// these End2EndTests are are collaboration tests
     /// </summary>
    public class SellOneItemTests
    {
        [Fact]
        public void End2End_KnownProductBarcode_WillResultIn_ProductPrice()
        {
            var knownProductPriceResult = new KnownProductPriceResult();

            var cashRegisterProcessorRendererTestDouble = new CashRegisterRendererTestDouble();
            var productPriceProviderTestDouble = new ProductCatalogTestDouble(knownProductPriceResult);
            var barcodeInterpreterTestDouble = new BarcodeInterpreterTestDouble();
            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cashRegisterProcessorRendererTestDouble, barcodeInterpreterTestDouble);

            cashRegisterProcessor.OnBarcode("123");

            Assert.Equal(knownProductPriceResult, cashRegisterProcessorRendererTestDouble.ResultToRender);
        }

        [Fact]
        public void End2End_UnknownProductBarcode_WillResultIn_UnknownProduct()
        {
            //discuss because behaviour of result known/unknow and renderer ????
            // smell => internals are external visibile, implementation detail


            var unknownProductPriceResult = new UnknownProductPriceResult();

            var cashRegisterProcessorRendererTestDouble = new CashRegisterRendererTestDouble();
            var productPriceProviderTestDouble = new ProductCatalogTestDouble(unknownProductPriceResult);
            var barcodeInterpreterTestDouble = new BarcodeInterpreterTestDouble();
            var cashRegisterProcessor = new CashRegisterProcessor(productPriceProviderTestDouble, cashRegisterProcessorRendererTestDouble, barcodeInterpreterTestDouble);

            cashRegisterProcessor.OnBarcode("123");

            Assert.Equal(unknownProductPriceResult, cashRegisterProcessorRendererTestDouble.ResultToRender);
        }

        public class CashRegisterRendererTestDouble : ICashRegisterRenderer
        {
            public IProductPriceResult ResultToRender { get; private set; }

            public void Render(IProductPriceResult result)
            {
                ResultToRender = result;
            }
        }


        public class ProductCatalogTestDouble : IProductCatalog
        {
            private readonly IProductPriceResult _productPriceResult;

            public ProductCatalogTestDouble(IProductPriceResult productPriceResult)
            {
                _productPriceResult = productPriceResult;
            }

            public IProductPriceResult GetPrice(Barcode barcode)
            {
                return _productPriceResult;
            }
        }

        public class BarcodeInterpreterTestDouble : IBarcodeInterpreter
        {
            public Barcode Interpret(string value)
            {
                return new Barcode(value);
            }
        }
    }
}
