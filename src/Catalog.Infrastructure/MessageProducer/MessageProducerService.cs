using Catalog.Application.Common.MessageProducer;
using Catalog.Infrastructure.Settings;

namespace Catalog.Infrastructure.MessageProducer;

public class MessageProducerService : IMessageProducerService
{
    private readonly IConnection _connection;
    private readonly RabbitMqServiceSettings _rabbitMqServiceSettings;

    public MessageProducerService(
        IOptions<RabbitMqServiceSettings> rabbitMqServiceSettingsOptions)
    {
        _rabbitMqServiceSettings = rabbitMqServiceSettingsOptions.Value;
        var connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqServiceSettings.HostName,
            UserName = _rabbitMqServiceSettings.UserName,
            Password = _rabbitMqServiceSettings.Password,
            VirtualHost = _rabbitMqServiceSettings.VirtualHost,
            Port = _rabbitMqServiceSettings.Port
        };

        _connection = connectionFactory.CreateConnection()!;
    }

    public void SendMessage<TMessageModel>(TMessageModel messageModel)
    {
        using var channel = _connection.CreateModel();
        var queueName = _rabbitMqServiceSettings.QueueName;
        var routingKey = _rabbitMqServiceSettings.RoutingKey;
        channel.ExchangeDeclare(queueName, ExchangeType.Topic, true, false, null);
        channel.QueueDeclare(queueName, true, false, false, null);
        channel.QueueBind(queueName, queueName, routingKey, null);
        var jsonMessage = JsonSerializer.Serialize(messageModel);
        var messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(jsonMessage);
        channel.BasicPublish(queueName, routingKey, null, messageBodyBytes);
    }
}
