
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string userName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);
    internal class GetBasketQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket=await _repository.GetBasket(query.userName);
            return new GetBasketResult(basket);
        }
    }
}
