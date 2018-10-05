namespace Tests.Implementation.HappyZone
{
    public class CashRegisterProcessor
    {
        private readonly IProductCatalog _productCatalog;
        private readonly ICashRegisterRenderer _cashRegisterRenderer;

        public CashRegisterProcessor(
            IProductCatalog productCatalog,
            ICashRegisterRenderer cashRegisterRenderer)
        {
            _productCatalog = productCatalog;
            _cashRegisterRenderer = cashRegisterRenderer;
        }

        public void OnBarcode(Barcode barcode)
        {
            var productPriceResult = _productCatalog.GetPrice(barcode);
            _cashRegisterRenderer.Render(productPriceResult);

        }
    }
}