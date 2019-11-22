using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.Domain.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowManager.ProcessService.ReadModel.Bus
{
    public class EventBusReceiver
    {
        private const string _queueName = "events_queue";

        private readonly Dictionary<Type, List<IEventHandler<BaseEvent>>> _eventHandlersDictionary = new Dictionary<Type, List<IEventHandler<BaseEvent>>>();


        public EventBusReceiver()
        {
            var _messageEncoding = Encoding.GetEncoding("UTF-8");
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var serializedEvent = _messageEncoding.GetString(eventArgs.Body);
                BaseEvent @event = JsonConvert.DeserializeObject<BaseEvent>(serializedEvent, serializerSettings);

                if (_eventHandlersDictionary.TryGetValue(@event.GetType(), out List<IEventHandler<BaseEvent>> eventHandlers))
                {
                    foreach (var eventHandler in eventHandlers)
                    {
                        await eventHandler.HandleAsync(@event);
                    }
                }
            };
            channel.BasicConsume(queue: _queueName,
                                  autoAck: true,
                                  consumer: consumer);
        }

        public void RegisterEventHandlers(Type type, List<IEventHandler<BaseEvent>> eventHandlers)
        {
            if (!_eventHandlersDictionary.TryAdd(type, eventHandlers))
            {
                throw new Exception("This type is registred. Cannot override it.");
            }
        }

        // TODO: Builder design pattern with Initialize() method. ??  or simply register services in DI container.
    }
}
