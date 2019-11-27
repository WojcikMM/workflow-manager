﻿using CQRS.Template.Domain.Commands;
using CQRS.Template.Domain.Events;
using System;

namespace WorkflowManager.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, Exception, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, Exception, IRejectedEvent> onError = null)
            where TEvent : IEvent;
    }
}
