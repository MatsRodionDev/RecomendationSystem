using Microsoft.Extensions.Logging;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Interfaces.UseCases;

namespace RecomandationSystem.Application.Decorators
{
    internal static class LogginDecorator
    {
        internal sealed class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            ILogger<QueryHandler<TQuery, TResponse>> logger) 
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            public async Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken)
            {
                var requestName = typeof(TQuery).Name;

                logger.LogInformation("Processing requets {RequestName}", requestName);

                try
                {
                    var response = await innerHandler.HandleAsync(query, cancellationToken);

                    logger.LogInformation("Completed requets {RequestName}", requestName);

                    return response;
                }   
                catch(Exception ex)
                {
                    logger.LogInformation("Completed requets {RequestName} with error: {Error}", requestName, ex.Message);

                    throw;
                }
            }
        }

        internal sealed class CommandHandler<TCommand, TResponse>(
            ICommandHandler<TCommand, TResponse> innerHandler,
            ILogger<CommandHandler<TCommand, TResponse>> logger)
            : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken)
            {
                var requestName = typeof(TCommand).Name;

                logger.LogInformation("Processing requets {RequestName}", requestName);

                try
                {
                    var response = await innerHandler.HandleAsync(command, cancellationToken);

                    logger.LogInformation("Completed requets {RequestName}", requestName);

                    return response;
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Completed requets {RequestName} with error: {Error}", requestName, ex.Message);

                    throw;
                }
            }
        }

        internal sealed class BaseCommandHandler<TCommand>(
            IBaseCommandHandler<TCommand> innerHandler,
            ILogger<BaseCommandHandler<TCommand>> logger)
            : IBaseCommandHandler<TCommand>
            where TCommand : IBaseCommand
        {
            public async Task HandleAsync(TCommand command, CancellationToken cancellationToken)
            {
                var requestName = typeof(TCommand).Name;

                logger.LogInformation("Processing requets {RequestName}", requestName);

                try
                {
                    await innerHandler.HandleAsync(command, cancellationToken);

                    logger.LogInformation("Completed requets {RequestName}", requestName);
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Completed requets {RequestName} with error: {Error}", requestName, ex.Message);

                    throw;
                }
            }
        }

        internal sealed class CachedQueryHandler<TQuery, TResponse>(
            ICachedQueryHandler<TQuery, TResponse> innerHandler,
            ILogger<CachedQueryHandler<TQuery, TResponse>> logger)
            : ICachedQueryHandler<TQuery, TResponse>
            where TQuery : ICachedQuery<TResponse>
        {
            public async Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken)
            {
                var requestName = typeof(TQuery).Name;

                logger.LogInformation("Processing requets {RequestName}", requestName);

                try
                {
                    var response = await innerHandler.HandleAsync(query, cancellationToken);

                    logger.LogInformation("Completed requets {RequestName}", requestName);

                    return response;
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Completed requets {RequestName} with error: {Error}", requestName, ex.Message);

                    throw;
                }
            }
        }
    }

    internal static class CacheDecorator
    {
        internal sealed class QueryHandler<TQuery, TResponse>(
            ICachedQueryHandler<TQuery, TResponse> innerHandler,
            ICacheService cacheService)
            : ICachedQueryHandler<TQuery, TResponse>
            where TQuery : ICachedQuery<TResponse>
        {
            public async Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken)
            {
                var response = await cacheService.GetAsync<TResponse>(query.Key, cancellationToken);

                if(response is not null)
                {
                    return response;
                }

                response = await innerHandler.HandleAsync(query, cancellationToken);

                await cacheService.SetAsync(query.Key, response, TimeSpan.FromMinutes(1), cancellationToken);

                return response;
            }
        }
    }
}
