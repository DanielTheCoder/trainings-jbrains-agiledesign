namespace Tests.Implementation.HappyZone
{
    public interface IProductCatalog
    {
        IProductPriceResult GetPrice(Barcode barcode);
    }
}