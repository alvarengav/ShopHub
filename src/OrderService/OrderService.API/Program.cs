using Microsoft.EntityFrameworkCore;
using OrderService.Application.Commands;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Repositories;
using OrderService.Infrastructure.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new StronglyTypedIdJsonConverter<ProductId, long>()
        );
        options.JsonSerializerOptions.Converters.Add(
            new StronglyTypedIdJsonConverter<OrderId, long>()
        );
        options.JsonSerializerOptions.Converters.Add(
            new StronglyTypedIdJsonConverter<OrderItemId, long>()
        );
        options.JsonSerializerOptions.Converters.Add(
            new StronglyTypedIdJsonConverter<UserId, long>()
        );
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly)
);

builder.Services.AddDbContext<OrderDbContext>(options => options.UseInMemoryDatabase("OrderDb"));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
