using System;
using System.Text;
using System.Threading.Tasks;
using CQRS.Template.Domain.Bus;
using CQRS.Template.Domain.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace WorkflowManager.ProcessService.Infrastructure.Bus
{
    public class RabbitMqEventBus : IEventBus
    {
        private const string _queueName = "events_queue";
        private const string _exchangeName = "";
        private IModel _channel;
        private IBasicProperties _properties;
        private JsonSerializerSettings _serializerSettings;
        private Encoding _messageEncoding;

        public RabbitMqEventBus()
        {
            _messageEncoding = Encoding.GetEncoding("UTF-8");
            _serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            _properties = _channel.CreateBasicProperties();
            _properties.Persistent = true;
            


        }


        public async Task PublishAsync<T>(T @event) where T : BaseEvent
        {
            var serializedEvent = JsonConvert.SerializeObject(@event, _serializerSettings);
            var messageBody = _messageEncoding.GetBytes(serializedEvent);

            _channel.BasicPublish(exchange: _exchangeName,
                                  routingKey: _queueName,
                                  basicProperties: _properties,
                                  body: messageBody);
            await Task.CompletedTask;
        }
    }
}
