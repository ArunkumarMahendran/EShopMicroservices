namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> _logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
          _logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query.Category);

         var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync();
            return new GetProductByCategoryResult(products);
        }
    }
}
