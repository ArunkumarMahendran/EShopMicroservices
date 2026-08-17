



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assemby = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assemby);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assemby);
builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
{ opt.Connection(builder.Configuration.GetConnectionString("Database")); }).UseLightweightSessions();

if(builder.Environment.IsDevelopment())
   builder.Services.InitializeMartenWith<CatalogInitialData>();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.UseExceptionHandler(options => { });
app.Run();
