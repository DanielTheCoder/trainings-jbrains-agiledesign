using System.Linq;
using Tests.Implementation.HappyZone;

namespace Tests.Implementation.Dmz
{
    public class InMemoryProductCatalog : IProductCatalog
    {
        private readonly (Barcode, IProductPriceResult)[] _listOfKnownPrices;

        public InMemoryProductCatalog((Barcode, IProductPriceResult)[] listOfKnownPrices)
        {
            _listOfKnownPrices = listOfKnownPrices;
        }

        public InMemoryProductCatalog() : this(null)
        {}

        public IProductPriceResult GetPrice(Barcode barcode)
        {
            (Barcode, IProductPriceResult) result = default;

            if (_listOfKnownPrices != null)
            {
                result = _listOfKnownPrices.SingleOrDefault(kp => kp.Item1 == barcode);
            }

            if (result != default)
                return result.Item2;

            return new UnknownProductPriceResult();
        }
    }
}