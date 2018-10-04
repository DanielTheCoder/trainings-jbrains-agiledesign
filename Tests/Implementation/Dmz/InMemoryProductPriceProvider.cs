using System.Linq;
using Tests.Implementation;

namespace Tests.HappyZone
{
    public class InMemoryProductPriceProvider : IProductPriceProvider
    {
        private readonly (BarcodeQuery, KnownProductPriceResult)[] _listOfKnownPrices;

        public InMemoryProductPriceProvider((BarcodeQuery, KnownProductPriceResult)[] listOfKnownPrices)
        {
            _listOfKnownPrices = listOfKnownPrices;
        }

        public InMemoryProductPriceProvider() : this(null)
        {}

        public IProductPriceResult Query(BarcodeQuery barcodeQuery)
        {
            (BarcodeQuery, KnownProductPriceResult)? result = null;

            if (_listOfKnownPrices != null)
            {
                result = _listOfKnownPrices.SingleOrDefault(kp => kp.Item1 == barcodeQuery);
            }

            if (result != null)
                return result.Value.Item2;

            return new UnknownProductPriceResult();
        }
    }
}