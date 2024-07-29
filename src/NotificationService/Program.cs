using NotificationService.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<RabbitMQEventConsumer>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var rabbitMQEventConsumer = app.Services.GetRequiredService<RabbitMQEventConsumer>();
rabbitMQEventConsumer.StartListening();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
