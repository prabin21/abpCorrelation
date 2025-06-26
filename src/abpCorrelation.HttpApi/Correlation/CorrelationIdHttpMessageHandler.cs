using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace abpCorrelation.HttpApi.Correlation;

/// <summary>
/// HTTP message handler that automatically adds correlation ID to outgoing requests
/// </summary>
public class CorrelationIdHttpMessageHandler : DelegatingHandler, ITransientDependency
{
    private readonly ICorrelationIdProvider _correlationIdProvider;
    private readonly ILogger<CorrelationIdHttpMessageHandler> _logger;

    public CorrelationIdHttpMessageHandler(
        ICorrelationIdProvider correlationIdProvider,
        ILogger<CorrelationIdHttpMessageHandler> logger)
    {
        _correlationIdProvider = correlationIdProvider;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var correlationId = _correlationIdProvider.GetCorrelationId();
        
        if (!string.IsNullOrEmpty(correlationId))
        {
            // Add correlation ID to request headers
            request.Headers.Add("X-Correlation-ID", correlationId);
            
            // Add trace context if available
            if (Activity.Current != null)
            {
                request.Headers.Add("X-Trace-ID", Activity.Current.TraceId.ToString());
                request.Headers.Add("X-Span-ID", Activity.Current.SpanId.ToString());
            }

            _logger.LogDebug("Added correlation ID {CorrelationId} to request to {Uri}", 
                correlationId, request.RequestUri);
        }
        else
        {
            _logger.LogWarning("No correlation ID available for request to {Uri}", request.RequestUri);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Log response correlation ID if present
        if (response.Headers.TryGetValues("X-Correlation-ID", out var responseCorrelationIds))
        {
            var responseCorrelationId = responseCorrelationIds.FirstOrDefault();
            _logger.LogDebug("Response from {Uri} has correlation ID: {CorrelationId}", 
                request.RequestUri, responseCorrelationId);
        }

        return response;
    }
} 