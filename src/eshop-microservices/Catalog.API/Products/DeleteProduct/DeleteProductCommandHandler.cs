namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);
    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> _logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

            session.Delete<Product>(command.Id);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
