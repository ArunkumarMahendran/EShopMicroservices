namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("StoreBasket")
            .WithDisplayName("Store Basket")
            .WithDescription("Stores the shopping cart for a user in the basket service.")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            ;
        }
}
}
