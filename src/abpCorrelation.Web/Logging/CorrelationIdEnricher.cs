using Serilog.Core;
using Serilog.Events;
using Volo.Abp.DependencyInjection;

namespace abpCorrelation.Web.Logging;

/// <summary>
/// Serilog enricher that adds correlation ID information to log entries
/// </summary>
public class CorrelationIdEnricher : ILogEventEnricher, ITransientDependency
{
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public CorrelationIdEnricher(ICorrelationIdProvider correlationIdProvider)
    {
        _correlationIdProvider = correlationIdProvider;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var metadata = _correlationIdProvider.GetMetadata();
        
        // Add correlation ID
        if (!string.IsNullOrEmpty(metadata.CorrelationId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", metadata.CorrelationId));
        }

        // Add parent correlation ID if exists
        if (!string.IsNullOrEmpty(metadata.ParentCorrelationId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ParentCorrelationId", metadata.ParentCorrelationId));
        }

        // Add trace information
        if (!string.IsNullOrEmpty(metadata.TraceId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TraceId", metadata.TraceId));
        }

        if (!string.IsNullOrEmpty(metadata.SpanId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("SpanId", metadata.SpanId));
        }

        // Add user context
        if (!string.IsNullOrEmpty(metadata.UserId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", metadata.UserId));
        }

        if (!string.IsNullOrEmpty(metadata.TenantId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TenantId", metadata.TenantId));
        }

        // Add timestamp
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationTimestamp", metadata.Timestamp));
    }
} 