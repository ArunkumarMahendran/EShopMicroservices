using Catalog.API.Products.GetProductByCategory;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile) : ICommand<UpdateProductResult>;
    public record UpdateProductResponse(bool IsUpdated);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var result = await sender.Send(new UpdateProductCommand(request.Id, request.Name, request.Description, request.Price, request.Category, request.ImageFile));
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Updates a product")
            .WithDescription("Updates the details of an existing product")
            ;
        }
    }
}
