using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace abpCorrelation.Application.Contracts.Correlation;

/// <summary>
/// Application service for correlation log management
/// </summary>
public interface ICorrelationLogAppService : 
    ICrudAppService<
        CorrelationLogDto,
        Guid,
        GetCorrelationLogListDto,
        CreateCorrelationLogDto,
        UpdateCorrelationLogDto>
{
    /// <summary>
    /// Get correlation logs by correlation ID
    /// </summary>
    Task<List<CorrelationLogDto>> GetByCorrelationIdAsync(string correlationId);

    /// <summary>
    /// Get correlation logs by parent correlation ID
    /// </summary>
    Task<List<CorrelationLogDto>> GetByParentCorrelationIdAsync(string parentCorrelationId);

    /// <summary>
    /// Get correlation logs by trace ID
    /// </summary>
    Task<List<CorrelationLogDto>> GetByTraceIdAsync(string traceId);

    /// <summary>
    /// Get correlation logs by user ID
    /// </summary>
    Task<List<CorrelationLogDto>> GetByUserIdAsync(Guid userId, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs by operation type
    /// </summary>
    Task<List<CorrelationLogDto>> GetByOperationTypeAsync(string operationType, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs by severity
    /// </summary>
    Task<List<CorrelationLogDto>> GetBySeverityAsync(string severity, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs by date range
    /// </summary>
    Task<List<CorrelationLogDto>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

    /// <summary>
    /// Get correlation logs with errors
    /// </summary>
    Task<List<CorrelationLogDto>> GetErrorsAsync(DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get slow operations (above specified duration)
    /// </summary>
    Task<List<CorrelationLogDto>> GetSlowOperationsAsync(long minDurationMs, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation logs for a specific API endpoint
    /// </summary>
    Task<List<CorrelationLogDto>> GetByUrlAsync(string url, DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Get correlation statistics
    /// </summary>
    Task<CorrelationStatisticsDto> GetStatisticsAsync(DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Search correlation logs with advanced filtering
    /// </summary>
    Task<PagedResultDto<CorrelationLogDto>> SearchAsync(SearchCorrelationLogsDto input);

    /// <summary>
    /// Get unique correlation IDs in date range
    /// </summary>
    Task<List<string>> GetUniqueCorrelationIdsAsync(DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Clean up old correlation logs
    /// </summary>
    Task<int> CleanupOldLogsAsync(DateTime cutoffDate);

    /// <summary>
    /// Log an API call with correlation ID
    /// </summary>
    Task<CorrelationLogDto> LogApiCallAsync(
        string correlationId,
        string operationName,
        string httpMethod,
        string url,
        string? requestData = null,
        string? responseData = null,
        int? httpStatusCode = null,
        long durationMs = 0,
        string? errorMessage = null,
        string? parentCorrelationId = null,
        string? traceId = null,
        string? spanId = null);

    /// <summary>
    /// Log a database operation with correlation ID
    /// </summary>
    Task<CorrelationLogDto> LogDatabaseOperationAsync(
        string correlationId,
        string operationName,
        string? requestData = null,
        string? responseData = null,
        long durationMs = 0,
        string? errorMessage = null,
        string? parentCorrelationId = null,
        string? traceId = null,
        string? spanId = null);

    /// <summary>
    /// Log an external service call with correlation ID
    /// </summary>
    Task<CorrelationLogDto> LogExternalServiceCallAsync(
        string correlationId,
        string serviceName,
        string operationName,
        string? requestData = null,
        string? responseData = null,
        long durationMs = 0,
        string? errorMessage = null,
        string? parentCorrelationId = null,
        string? traceId = null,
        string? spanId = null);

    /// <summary>
    /// Log a business operation with correlation ID
    /// </summary>
    Task<CorrelationLogDto> LogBusinessOperationAsync(
        string correlationId,
        string operationName,
        string? requestData = null,
        string? responseData = null,
        long durationMs = 0,
        string? errorMessage = null,
        string? parentCorrelationId = null,
        string? traceId = null,
        string? spanId = null);

    /// <summary>
    /// Get correlation flow (parent-child relationships)
    /// </summary>
    Task<List<CorrelationLogDto>> GetCorrelationFlowAsync(string correlationId);

    /// <summary>
    /// Export correlation logs to CSV
    /// </summary>
    Task<byte[]> ExportToCsvAsync(SearchCorrelationLogsDto input);

    /// <summary>
    /// Get performance metrics by operation type
    /// </summary>
    Task<Dictionary<string, object>> GetPerformanceMetricsAsync(DateTime? fromDate = null, DateTime? toDate = null);
} 