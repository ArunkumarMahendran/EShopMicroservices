namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category,
        string Description,string ImageFile,decimal price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Create a new product instance and populate it with the data from the command.
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.price
            };

            // SAVE the product to the database or perform other business logic here.
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);


            // we'll just return the result with the new product ID.
            return new CreateProductResult(product.Id);
        }
    }
}
