using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.Messaging;

public class RabbitMQEventConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQEventConsumer()
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

    public void StartListening()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

            //TODO: Implement mail delivery
            Console.WriteLine(
                $"Order created: {orderCreatedEvent.OrderId}, Send email to user with Id: {orderCreatedEvent.UserId}"
            );
        };

        _channel.BasicConsume(queue: "orderQueue", autoAck: true, consumer: consumer);
    }
}
