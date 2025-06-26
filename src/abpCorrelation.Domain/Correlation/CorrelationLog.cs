using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace abpCorrelation.Domain.Correlation;

/// <summary>
/// Entity for storing correlation logs with detailed request tracking information
/// </summary>
public class CorrelationLog : AuditedAggregateRoot<Guid>, IMultiTenant
{
    /// <summary>
    /// The correlation ID that links related operations
    /// </summary>
    public string CorrelationId { get; set; } = string.Empty;

    /// <summary>
    /// Parent correlation ID for nested operations
    /// </summary>
    public string? ParentCorrelationId { get; set; }

    /// <summary>
    /// Trace ID for distributed tracing
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// Span ID for distributed tracing
    /// </summary>
    public string? SpanId { get; set; }

    /// <summary>
    /// The type of operation (e.g., "API_CALL", "DATABASE_QUERY", "EXTERNAL_SERVICE")
    /// </summary>
    public string OperationType { get; set; } = string.Empty;

    /// <summary>
    /// The name of the operation or method
    /// </summary>
    public string OperationName { get; set; } = string.Empty;

    /// <summary>
    /// HTTP method for API calls
    /// </summary>
    public string? HttpMethod { get; set; }

    /// <summary>
    /// URL for API calls
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// HTTP status code for API calls
    /// </summary>
    public int? HttpStatusCode { get; set; }

    /// <summary>
    /// Request payload or parameters
    /// </summary>
    public string? RequestData { get; set; }

    /// <summary>
    /// Response data or result
    /// </summary>
    public string? ResponseData { get; set; }

    /// <summary>
    /// Error message if operation failed
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Stack trace if error occurred
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// Duration of the operation in milliseconds
    /// </summary>
    public long DurationMs { get; set; }

    /// <summary>
    /// Additional metadata as JSON
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// Tenant ID for multi-tenancy
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// User ID who performed the operation
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Username who performed the operation
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Client IP address
    /// </summary>
    public string? ClientIpAddress { get; set; }

    /// <summary>
    /// User agent string
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Application name or service name
    /// </summary>
    public string? ApplicationName { get; set; }

    /// <summary>
    /// Environment (Development, Staging, Production)
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Severity level (Info, Warning, Error, Critical)
    /// </summary>
    public string Severity { get; set; } = "Info";

    /// <summary>
    /// Whether this operation was successful
    /// </summary>
    public bool IsSuccess { get; set; } = true;
} 