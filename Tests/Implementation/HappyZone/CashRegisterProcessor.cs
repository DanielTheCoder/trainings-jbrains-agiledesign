namespace Tests.Implementation.HappyZone
{
    public class CashRegisterProcessor
    {
        private readonly IProductCatalog _productCatalog;
        private readonly ICashRegisterRenderer _cashRegisterRenderer;
        private readonly IBarcodeInterpreter _barcodeInterpreter;

        public CashRegisterProcessor(
            IProductCatalog productCatalog,
            ICashRegisterRenderer cashRegisterRenderer,
            IBarcodeInterpreter barcodeInterpreter)
        {
            _productCatalog = productCatalog;
            _cashRegisterRenderer = cashRegisterRenderer;
            _barcodeInterpreter = barcodeInterpreter;
        }

        public void OnBarcode(string rawBarcodeString)
        {
            var barcode = _barcodeInterpreter.Interpret(rawBarcodeString);
            var productPriceResult = _productCatalog.GetPrice(barcode);
            _cashRegisterRenderer.Render(productPriceResult);

        }
    }
}