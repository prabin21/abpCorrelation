using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;
using Volo.Abp.Timing;

namespace abpCorrelation.Correlation;

/// <summary>
/// Advanced correlation ID provider with distributed tracing support
/// </summary>
public class CorrelationIdProvider : ICorrelationIdProvider, ISingletonDependency
{
    private readonly AsyncLocal<string?> _correlationId = new();
    private readonly AsyncLocal<string?> _parentCorrelationId = new();
    private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
    private readonly IClock _clock;
    private readonly ILogger<CorrelationIdProvider> _logger;

    public CorrelationIdProvider(
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        IClock clock,
        ILogger<CorrelationIdProvider> logger)
    {
        _currentPrincipalAccessor = currentPrincipalAccessor;
        _clock = clock;
        _logger = logger;
    }

    public string? GetCorrelationId()
    {
        return _correlationId.Value;
    }

    public void SetCorrelationId(string correlationId)
    {
        if (string.IsNullOrEmpty(correlationId))
        {
            _logger.LogWarning("Attempted to set empty correlation ID");
            return;
        }

        _correlationId.Value = correlationId;
        _logger.LogDebug("Correlation ID set to: {CorrelationId}", correlationId);
    }

    public string GenerateCorrelationId()
    {
        // Generate a correlation ID with timestamp and random component
        var timestamp = _clock.Now.ToString("yyyyMMddHHmmss");
        var random = Guid.NewGuid().ToString("N")[..8];
        return $"corr-{timestamp}-{random}";
    }

    public ICorrelationContext CreateContext()
    {
        var parentId = _correlationId.Value;
        var newCorrelationId = GenerateCorrelationId();
        
        _parentCorrelationId.Value = parentId;
        _correlationId.Value = newCorrelationId;

        _logger.LogDebug("Created new correlation context: {CorrelationId} (Parent: {ParentId})", 
            newCorrelationId, parentId);

        return new CorrelationContext(newCorrelationId, GetMetadata(), () =>
        {
            // Restore parent correlation ID when context is disposed
            _correlationId.Value = parentId;
            _parentCorrelationId.Value = null;
        });
    }

    public CorrelationMetadata GetMetadata()
    {
        var principal = _currentPrincipalAccessor.Principal;
        
        return new CorrelationMetadata
        {
            CorrelationId = _correlationId.Value ?? string.Empty,
            ParentCorrelationId = _parentCorrelationId.Value,
            TraceId = Activity.Current?.TraceId.ToString(),
            SpanId = Activity.Current?.SpanId.ToString(),
            Timestamp = _clock.Now,
            UserId = principal?.FindFirst(AbpClaimTypes.UserId)?.Value,
            TenantId = principal?.FindFirst(AbpClaimTypes.TenantId)?.Value
        };
    }

    private class CorrelationContext : ICorrelationContext
    {
        private readonly Action _disposeAction;

        public CorrelationContext(string correlationId, CorrelationMetadata metadata, Action disposeAction)
        {
            CorrelationId = correlationId;
            Metadata = metadata;
            _disposeAction = disposeAction;
        }

        public string CorrelationId { get; }
        public CorrelationMetadata Metadata { get; }

        public void Dispose()
        {
            _disposeAction();
        }
    }
} 