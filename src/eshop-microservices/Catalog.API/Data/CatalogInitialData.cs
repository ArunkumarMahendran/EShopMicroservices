using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;
            session.Store<Product>(GetPreProductData());
            await session.SaveChangesAsync();
        }

        private IEnumerable<Product> GetPreProductData() => new List<Product>
        {
             new Product
            {
                 Id=Guid.NewGuid(),
                 Name="IPhone -X",
                 Description="This phone is the biggest company",
                 ImageFile="product-1.png",
                 Price=950.9M,
                 Category=new List<string>{"Smart Phone"}

            },
             new Product
            {
                 Id=Guid.NewGuid(),
                 Name="Samsung",
                 Description="This phone is the second largest company",
                 ImageFile="product-2.png",
                 Price=250.9M,
                 Category=new List<string>{"Smart Phone"}
            }

        };
    }
}
