namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsDeleted);
    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userName);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteBasket")
                .WithDescription("Deletes a basket for a given user.")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .WithDisplayName("Delete Basket");
        }
    }
}
