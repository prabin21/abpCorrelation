using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace abpCorrelation.Domain.Correlation;

/// <summary>
/// Repository interface for correlation logs
/// </summary>
public interface ICorrelationLogRepository : IRepository<CorrelationLog, Guid>
{
    /// <summary>
    /// Get correlation logs by correlation ID
    /// </summary>
    Task<List<CorrelationLog>> GetByCorrelationIdAsync(string correlationId);

    /// <summary>
    /// Get correlation logs by parent correlation ID
    /// </summary>
    Task<List<CorrelationLog>> GetByParentCorrelationIdAsync(string parentCorrelationId);

    /// <summary>
    /// Get correlation logs by trace ID
    /// </summary>
    Task<List<CorrelationLog>> GetByTraceIdAsync(string traceId);

    /// <summary>
    /// Get correlation logs by user ID
    /// </summary>
    Task<List<CorrelationLog>> GetByUserIdAsync(Guid userId, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs by operation type
    /// </summary>
    Task<List<CorrelationLog>> GetByOperationTypeAsync(string operationType, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs by severity
    /// </summary>
    Task<List<CorrelationLog>> GetBySeverityAsync(string severity, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs by date range
    /// </summary>
    Task<List<CorrelationLog>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

    /// <summary>
    /// Get correlation logs with errors
    /// </summary>
    Task<List<CorrelationLog>> GetErrorsAsync(DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get slow operations (above specified duration)
    /// </summary>
    Task<List<CorrelationLog>> GetSlowOperationsAsync(long minDurationMs, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs for a specific API endpoint
    /// </summary>
    Task<List<CorrelationLog>> GetByUrlAsync(string url, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs with pagination
    /// </summary>
    Task<(List<CorrelationLog> Items, long TotalCount)> GetPagedAsync(
        int skipCount,
        int maxResultCount,
        string? correlationId = null,
        string? operationType = null,
        string? severity = null,
        bool? isSuccess = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        string? sorting = null);

    /// <summary>
    /// Get correlation statistics
    /// </summary>
    Task<CorrelationStatistics> GetStatisticsAsync(DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Clean up old correlation logs
    /// </summary>
    Task<int> CleanupOldLogsAsync(DateTime cutoffDate);

    /// <summary>
    /// Get unique correlation IDs in date range
    /// </summary>
    Task<List<string>> GetUniqueCorrelationIdsAsync(DateTime? fromDate = null, DateTime? toDate = null);
}

/// <summary>
/// Statistics for correlation logs
/// </summary>
public class CorrelationStatistics
{
    public long TotalLogs { get; set; }
    public long ErrorLogs { get; set; }
    public long SuccessLogs { get; set; }
    public double AverageDurationMs { get; set; }
    public long MaxDurationMs { get; set; }
    public long MinDurationMs { get; set; }
    public Dictionary<string, long> OperationTypeCounts { get; set; } = new();
    public Dictionary<string, long> SeverityCounts { get; set; } = new();
    public Dictionary<int, long> HttpStatusCodeCounts { get; set; } = new();
} 