using Microsoft.Extensions.DependencyInjection;
using RecomandationSystem.Application.Interfaces.UseCases;
using System.Collections.Concurrent;
using System.Reflection;
namespace RecomandationSystem.Application.UseCases
{
    public interface IDispatcher
    {
        Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
            where TResponse : class;
        Task<TResponse> Dispatch<TResponse>(ICachedQuery<TResponse> query, CancellationToken cancellationToken)
           where TResponse : class;
    }

    internal sealed class Dispatcher(
        IServiceProvider serviceProvider) : IDispatcher
    {
        private static readonly ConcurrentDictionary<Type, Type> handlerTypes = new();
        private static readonly ConcurrentDictionary<Type, Type> wrapperTypeDictionary = new();

        public async Task<TResponse> Dispatch<TResponse>(ICachedQuery<TResponse> query, CancellationToken cancellationToken)
           where TResponse : class
        {
            await using var scope = serviceProvider.CreateAsyncScope();

            var handlerType = handlerTypes.GetOrAdd(query.GetType(),
                type => typeof(ICachedQueryHandler<,>).MakeGenericType(type, typeof(TResponse)));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            if (handler is null)
            {
                throw new NotImplementedException();
            }

            var handlerWrapper = CachedQueryHandlerWrapper<TResponse>.Create(handler, query);

            return await handlerWrapper.Handle(query, cancellationToken);
        }

        private abstract class CachedQueryHandlerWrapper<TResponse> where TResponse : class
        {
            public abstract Task<TResponse> Handle(ICachedQuery<TResponse> query, CancellationToken cancellationToken);

            public static CachedQueryHandlerWrapper<TResponse> Create(object handler, ICachedQuery<TResponse> query)
            {
                var wrapperType = wrapperTypeDictionary.GetOrAdd(query.GetType(),
                    qt => typeof(CachedQueryHandlerWrapper<,>).MakeGenericType(qt, typeof(TResponse)));

                return (CachedQueryHandlerWrapper<TResponse>)Activator.CreateInstance(wrapperType, handler)!;
            }
        }

        private sealed class CachedQueryHandlerWrapper<TQuery, TResponse>(object handler)
            : CachedQueryHandlerWrapper<TResponse>
            where TQuery : ICachedQuery<TResponse>
            where TResponse : class
        {
            private readonly ICachedQueryHandler<TQuery, TResponse> handler = (ICachedQueryHandler<TQuery, TResponse>)handler;

            public override async Task<TResponse> Handle(ICachedQuery<TResponse> query, CancellationToken cancellationToken)
            {
                return await handler.HandleAsync((TQuery)query, cancellationToken);
            }
        }

        public async Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
            where TResponse : class
        {
            await using var scope = serviceProvider.CreateAsyncScope();

            var handlerType = handlerTypes.GetOrAdd(query.GetType(), 
                type => typeof(IQueryHandler<,>).MakeGenericType(type, typeof(TResponse)));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            if(handler is null)
            {
                throw new NotImplementedException();
            }

            var handlerWrapper = QueryHandlerWrapper<TResponse>.Create(handler, query);

            return await handlerWrapper.Handle(query, cancellationToken);
        }

        private abstract class QueryHandlerWrapper<TResponse> where TResponse : class
        {
            public abstract Task<TResponse> Handle(IQuery<TResponse> query, CancellationToken cancellationToken);

            public static QueryHandlerWrapper<TResponse> Create(object handler, IQuery<TResponse> query)
            {
                var wrapperType = wrapperTypeDictionary.GetOrAdd(query.GetType(),
                    qt => typeof(QueryHandlerWrapper<,>).MakeGenericType(qt, typeof(TResponse)));

                return (QueryHandlerWrapper<TResponse>)Activator.CreateInstance(wrapperType, handler)!;
            }
        }

        private sealed class QueryHandlerWrapper<TQuery, TResponse>(object handler) 
            : QueryHandlerWrapper<TResponse> 
            where TQuery : IQuery<TResponse> 
            where TResponse : class
        {
            private readonly IQueryHandler<TQuery, TResponse> handler = (IQueryHandler<TQuery, TResponse>)handler;

            public override async Task<TResponse> Handle(IQuery<TResponse> query, CancellationToken cancellationToken)
            {
                return await handler.HandleAsync((TQuery) query, cancellationToken);
            }
        }

        public async Task<TResponse> Dispatch<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken)
            where TResponse : class
        {
            await using var scope = serviceProvider.CreateAsyncScope();

            var handlerType = handlerTypes.GetOrAdd(command.GetType(),
                type => typeof(ICommandHandler<,>).MakeGenericType(type, typeof(TResponse)));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            if (handler is null)
            {
                throw new NotImplementedException();
            }

            var handlerWrapper = CommandHandlerWrapper<TResponse>.Create(handler, command);

            return await handlerWrapper.Handle(command, cancellationToken);
        }

        private abstract class CommandHandlerWrapper<TResponse> where TResponse : class
        {
            public abstract Task<TResponse> Handle(ICommand<TResponse> command, CancellationToken cancellationToken);

            public static CommandHandlerWrapper<TResponse> Create(object handler, ICommand<TResponse> command)
            {
                var wrapperType = wrapperTypeDictionary.GetOrAdd(command.GetType(),
                    qt => typeof(CommandHandlerWrapper<,>).MakeGenericType(qt, typeof(TResponse)));

                return (CommandHandlerWrapper<TResponse>)Activator.CreateInstance(wrapperType, handler)!;
            }
        }

        private sealed class CommandHandlerWrapper<TQuery, TResponse>(object handler)
            : CommandHandlerWrapper<TResponse>
            where TQuery : ICommand<TResponse>
            where TResponse : class
        {
            private readonly ICommandHandler<TQuery, TResponse> handler = (ICommandHandler<TQuery, TResponse>)handler;

            public override async Task<TResponse> Handle(ICommand<TResponse> command, CancellationToken cancellationToken)
            {
                return await handler.HandleAsync((TQuery)command, cancellationToken);
            }
        }

        public async Task Dispatch(IBaseCommand command, CancellationToken cancellationToken)
        {
            await using var scope = serviceProvider.CreateAsyncScope();

            var handlerType = handlerTypes.GetOrAdd(command.GetType(),
                type => typeof(IBaseCommandHandler<>).MakeGenericType(type));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            if (handler is null)
            {
                throw new NotImplementedException();
            }

            var handlerWrapper = BaseCommandHandlerWrapper.Create(handler, command);

            await handlerWrapper.Handle(command, cancellationToken);
        }

        private abstract class BaseCommandHandlerWrapper 
        {
            public abstract Task Handle(IBaseCommand command, CancellationToken cancellationToken);

            public static BaseCommandHandlerWrapper Create(object handler, IBaseCommand command)
            {
                var wrapperType = wrapperTypeDictionary.GetOrAdd(command.GetType(),
                    qt => typeof(BaseCommandHandlerWrapper<>).MakeGenericType(qt));

                return (BaseCommandHandlerWrapper)Activator.CreateInstance(wrapperType, handler)!;
            }
        }

        private sealed class BaseCommandHandlerWrapper<TCommand>(object handler)
            : BaseCommandHandlerWrapper
            where TCommand : IBaseCommand
        {
            private readonly IBaseCommandHandler<TCommand> handler = (IBaseCommandHandler<TCommand>)handler;

            public override async Task Handle(IBaseCommand command, CancellationToken cancellationToken)
            {
                await handler.HandleAsync((TCommand)command, cancellationToken);
            }
        }
    }
}
