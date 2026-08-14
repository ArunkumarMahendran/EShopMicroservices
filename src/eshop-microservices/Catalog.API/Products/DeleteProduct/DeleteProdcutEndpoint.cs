using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductRequest(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResponse(bool IsDeleted);
    public class DeleteProdcutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}",async(Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Deletes a product")
            .WithDescription("Deletes an existing product")
            ;
        }
    }
}
