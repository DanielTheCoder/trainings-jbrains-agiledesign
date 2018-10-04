namespace Tests
{
    public class CashRegisterProcessor
    {
        private readonly IProductPriceProvider _productPriceProvider;
        private readonly ICachRegisterProcessorRenderer _cachRegisterProcessorRenderer;

        public CashRegisterProcessor(
            IProductPriceProvider productPriceProvider,
            ICachRegisterProcessorRenderer cachRegisterProcessorRenderer)
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


    public interface ICachRegisterProcessorRenderer
    {
        void Render(ProductPriceResult result);
    }


    public interface IProductPriceProvider
    {
        ProductPriceResult Query(ProductPriceQuery productPriceQuery);
    }


    public class ProductPriceResult
    {
    }


    public class ProductPriceQuery
    {
    }
}