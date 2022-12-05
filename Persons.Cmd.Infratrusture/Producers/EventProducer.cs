using CQRS.Core.Events;
using CQRS.Core.Producers;

using Microsoft.Extensions.Options;

using MongoDB.Bson.IO;

using Persons.Cmd.Infratrusture.Config;

using RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        var connection = ConnectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(topic);
        var json = JsonSerializer.Serialize(@event, @event.GetType());
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: "persons", body: body);
        return Task.CompletedTask;
    }
}
