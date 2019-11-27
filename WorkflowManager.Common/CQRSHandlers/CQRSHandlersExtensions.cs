using CQRS.Template.Domain.CommandHandlers;
using CQRS.Template.Domain.Commands;
using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace WorkflowManager.Common.CQRSHandlers
{
    public static class CQRSHandlersExtensions
    {
        public static IServiceCollection AddEventHandler<TEvent, TEventHandler>(this IServiceCollection services)
            where TEvent : BaseEvent where TEventHandler : IEventHandler<TEvent>
        {
            services.AddTransient(typeof(IEventHandler<TEvent>), typeof(TEventHandler));
            return services;
        }

        public static IServiceCollection AddCommandHandler<TCommand, TCommandHandler>(this IServiceCollection services)
            where TCommand : BaseCommand where TCommandHandler : ICommandHandler<TCommand>
        {

            services.AddTransient(typeof(ICommandHandler<TCommand>), typeof(TCommandHandler));
            return services;
        }
    }
}
