namespace Catalog.API.Products.GetProducts
{
    public record GetProductQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session,ILogger<GetProductsQueryHandler> _logger)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

            //Get Products from DB
            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductResult(products);
        }
    }
}
