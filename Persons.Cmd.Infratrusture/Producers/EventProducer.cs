using CQRS.Core.Config;
using CQRS.Core.Events;
using CQRS.Core.Producers;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using System.Text;
using System.Text.Json;

namespace Persons.Cmd.Infratrusture.Producers;
public class EventProducer : IEventProducer {
    public ConnectionFactory ConnectionFactory { get; }
    public EventProducer(IOptions<RabbitMqConfigParams> rabbitMqParams) {
        var configParams = rabbitMqParams.Value;
        ConnectionFactory = new ConnectionFactory {
            HostName = configParams.Hostname,
            UserName = configParams.Username,
            Password = configParams.Password,
            Port = configParams.Port
        };
    }
    public Task ProducerAsync<T>(string topic, T @event) where T : BaseEvent {
        using var connection = ConnectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        var eventName = "persons";

        channel.QueueDeclare(eventName, false, false, false, null);
        var json = JsonSerializer.Serialize(@event, @event.GetType());
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish("", eventName, null, body);
        return Task.CompletedTask;
    }
}
