using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductByCategory
{
    // public record GetProductByCategoryRequest(string Category);

    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string Category, ISender sender) => {
            
                var result = await sender.Send(new GetProductByCategoryQuery(Category));

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Retrieves products by category")
            .WithDescription("Retrieves a list of products that belong to the specified category")
            ;
        }
    }
}
