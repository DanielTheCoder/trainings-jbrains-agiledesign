namespace Tests.Implementation.HappyZone
{

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
}