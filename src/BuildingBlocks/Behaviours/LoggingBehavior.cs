using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> (ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : notnull,IRequest<TResponse> where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[START] Handling request: {typeof(TRequest).Name} response: {typeof(TResponse).Name} request data {request}");
           
            var timer=new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var timeTaken = timer.Elapsed;

            if(timeTaken.TotalSeconds > 3)
            {
                _logger.LogWarning($"[Performace - SLOW] Handling request: {typeof(TRequest).Name} response: {typeof(TResponse).Name} took {timeTaken.TotalSeconds} seconds");
            }
            else
            {
                _logger.LogInformation($"[END] Handling request: {typeof(TRequest).Name} response: {typeof(TResponse).Name} took {timeTaken.TotalSeconds} seconds");
            }
            
            return response;
        }
    }
}
