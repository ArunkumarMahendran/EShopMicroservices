


namespace Basket.API.Basket.GetBasket
{
    //public record GetBasketRequest(string UserName);
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);

            })
            .WithName("GetBasketQuery")
            .WithDescription("Get the basket for a user")
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get the basket for a user")
            .WithDescription("Get the basket for a user by username")
            ;
        }
    }
}
