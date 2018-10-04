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

    public class ConsoleCashRegisterProcessorRenderer : ICashRegisterProcessorRenderer
    {
        public void Render(IProductPriceResult result)
        {
            var formatedResult = result.ToString();

            System.Console.SetOut(new StringWriter());

            System.Console.WriteLine(formatedResult);
        }
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
    }

    // maybe this is an interface to abstract different implementation of barcodes ??
    public class BarcodeQuery
    {
    }
}