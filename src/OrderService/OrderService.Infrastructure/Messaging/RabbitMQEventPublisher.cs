using System.Text;
using System.Text.Json;
using OrderService.Application.Messaging;
using RabbitMQ.Client;

namespace OrderService.Infrastructure.Messaging;

public class RabbitMQEventPublisher : IEventPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQEventPublisher()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: "orderQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public void Publish<T>(T @event)
        where T : class
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
        _channel.BasicPublish(
            exchange: "",
            routingKey: "orderQueue",
            basicProperties: null,
            body: body
        );
    }
}
