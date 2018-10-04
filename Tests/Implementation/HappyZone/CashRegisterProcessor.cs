using System.IO;

namespace Tests.Implementation
{
    public class CashRegisterProcessor
    {
        private readonly IProductPriceProvider _productPriceProvider;
        private readonly ICashRegisterProcessorRenderer _cashRegisterProcessorRenderer;

        public CashRegisterProcessor(
            IProductPriceProvider productPriceProvider,
            ICashRegisterProcessorRenderer cashRegisterProcessorRenderer)
        {
            _productPriceProvider = productPriceProvider;
            _cashRegisterProcessorRenderer = cashRegisterProcessorRenderer;
        }

        public void OnBarcode(BarcodeQuery barcodeQuery)
        {
            var result = _productPriceProvider.Query(barcodeQuery);
            _cashRegisterProcessorRenderer.Render(result);

        }
    }


    public interface ICashRegisterProcessorRenderer
    {
        void Render(IProductPriceResult result);
    }


    public interface IProductPriceProvider
    {
        IProductPriceResult Query(BarcodeQuery barcodeQuery);
    }

    //maybe this is an interface => KnownProductPriceResult and UnknownProduct ??
    public interface IProductPriceResult
    {
    }

    public class UnknownProductPriceResult : IProductPriceResult
    {
    }

    public class KnownProductPriceResult : IProductPriceResult
    {
        public decimal Value { get; }

        public KnownProductPriceResult()
        {
            Value = 0;
        }

        public KnownProductPriceResult(decimal value)
        {
            Value = value;
        }
    }

    // maybe this is an interface to abstract different implementation of barcodes ??
    public class BarcodeQuery
    {
    }
}