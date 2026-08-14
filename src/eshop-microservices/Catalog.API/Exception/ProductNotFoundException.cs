namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : System.Exception
    {
        public ProductNotFoundException() : base("Product not found.")
        {
        }
    }
}
