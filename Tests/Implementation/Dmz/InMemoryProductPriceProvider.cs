using System.Linq;
using Tests.Implementation.HappyZone;

namespace Tests.Implementation.Dmz
{
    public class InMemoryProductPriceProvider : IProductPriceProvider
    {
        private readonly (BarcodeQuery, IProductPriceResult)[] _listOfKnownPrices;

        public InMemoryProductPriceProvider((BarcodeQuery, IProductPriceResult)[] listOfKnownPrices)
        {
            _listOfKnownPrices = listOfKnownPrices;
        }

        public InMemoryProductPriceProvider() : this(null)
        {}

        public IProductPriceResult Query(BarcodeQuery barcodeQuery)
        {
            (BarcodeQuery, IProductPriceResult) result = default;

            if (_listOfKnownPrices != null)
            {
                result = _listOfKnownPrices.SingleOrDefault(kp => kp.Item1 == barcodeQuery);
            }

            if (result != default)
                return result.Item2;

            return new UnknownProductPriceResult();
        }
    }
}