using CQRS.Core.Config;
using CQRS.Core.Consumers;
using CQRS.Core.Events;

using Microsoft.Extensions.Options;

using Persons.Query.Infrastructure.Converters;
using Persons.Query.Infrastructure.Handlers;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Text.Json;

namespace Persons.Query.Infrastructure.Consumers;
public class EventConsumer : IEventConsumer {
    private readonly IEventHandler _eventHandler;

    public ConnectionFactory ConnectionFactory { get; }
    public EventConsumer(IOptions<RabbitMqConfigParams> rabbitMqParams, IEventHandler eventHandler) {
        var configParams = rabbitMqParams.Value;
        ConnectionFactory = new ConnectionFactory {
            HostName = configParams.Hostname,
            UserName = configParams.Username,
            Password = configParams.Password,
            Port = configParams.Port,
            DispatchConsumersAsync = true
        };
        _eventHandler = eventHandler;
    }
    public void Consume(string topic) {
        var connection = ConnectionFactory.CreateConnection();
        var channel = connection.CreateModel();
        channel.QueueDeclare(topic, false, false, false, null);
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (model, ea) => {
            var message = Encoding.UTF8.GetString(ea.Body.Span);
            var options = new JsonSerializerOptions() {
                Converters = { new EventJsonConvert() }
            };
            try {
                var @event = JsonSerializer.Deserialize<BaseEvent>(message, options);
                var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });
                if(handlerMethod == null)
                    throw new ArgumentNullException(nameof(handlerMethod), "No se puede encontrar el manejador del evento");

                handlerMethod.Invoke(_eventHandler, new object[] { @event });
                channel.BasicAck(ea.DeliveryTag, false);
            } catch(Exception ex) {
                throw;
            }
        };
        channel.BasicConsume(topic, false, consumer);
    }
}