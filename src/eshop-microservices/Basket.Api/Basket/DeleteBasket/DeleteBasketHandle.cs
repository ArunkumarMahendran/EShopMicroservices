using System.Data;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsDeleted);

    public class DeleateBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleateBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty");
        }
    }
    internal class DeleteBasketCommandHandle(IBasketRepository _repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO: Implement the logic to delete the basket for the given user
            // For now, we will just return a successful result
            await _repository.DeleteBaseket(command.UserName, cancellationToken);
            return new DeleteBasketResult(true);
        }
    }
}
