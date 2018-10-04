namespace Tests.HappyZone
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

        public void OnBarcode(BarcodeQuery barcodeQuery)
        {
            var result = _productPriceProvider.Query(barcodeQuery);
            _cachRegisterProcessorRenderer.Render(result);

        }
    }


    public interface ICachRegisterProcessorRenderer
    {
        void Render(ProductPriceResult result);
    }


    public interface IProductPriceProvider
    {
        ProductPriceResult Query(BarcodeQuery barcodeQuery);
    }

    //maybe this is an interface => KnownProductPriceResult and UnknownProduct ??
    public class ProductPriceResult
    {
    }

    // maybe this is an interface to abstract different implementation of barcodes ??
    public class BarcodeQuery
    {
    }
}