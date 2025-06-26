using System.Threading.Tasks;

namespace abpCorrelation.Correlation;

/// <summary>
/// Provides correlation ID functionality with advanced features
/// </summary>
public interface ICorrelationIdProvider
{
    /// <summary>
    /// Gets the current correlation ID
    /// </summary>
    string? GetCorrelationId();
    
    /// <summary>
    /// Sets the correlation ID for the current context
    /// </summary>
    void SetCorrelationId(string correlationId);
    
    /// <summary>
    /// Generates a new correlation ID
    /// </summary>
    string GenerateCorrelationId();
    
    /// <summary>
    /// Creates a new correlation context with a new ID
    /// </summary>
    ICorrelationContext CreateContext();
    
    /// <summary>
    /// Gets correlation metadata for logging
    /// </summary>
    CorrelationMetadata GetMetadata();
}

public class CorrelationMetadata
{
    public string CorrelationId { get; set; } = string.Empty;
    public string? ParentCorrelationId { get; set; }
    public string? TraceId { get; set; }
    public string? SpanId { get; set; }
    public DateTime Timestamp { get; set; }
    public string? UserId { get; set; }
    public string? TenantId { get; set; }
}

public interface ICorrelationContext : IDisposable
{
    string CorrelationId { get; }
    CorrelationMetadata Metadata { get; }
} 