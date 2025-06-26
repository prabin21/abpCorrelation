using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace abpCorrelation.Application.Contracts.Correlation;

/// <summary>
/// DTO for correlation log
/// </summary>
public class CorrelationLogDto : AuditedEntityDto<Guid>
{
    public string CorrelationId { get; set; } = string.Empty;
    public string? ParentCorrelationId { get; set; }
    public string? TraceId { get; set; }
    public string? SpanId { get; set; }
    public string OperationType { get; set; } = string.Empty;
    public string OperationName { get; set; } = string.Empty;
    public string? HttpMethod { get; set; }
    public string? Url { get; set; }
    public int? HttpStatusCode { get; set; }
    public string? RequestData { get; set; }
    public string? ResponseData { get; set; }
    public string? ErrorMessage { get; set; }
    public string? StackTrace { get; set; }
    public long DurationMs { get; set; }
    public string? Metadata { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public string? ClientIpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? ApplicationName { get; set; }
    public string? Environment { get; set; }
    public string Severity { get; set; } = "Info";
    public bool IsSuccess { get; set; } = true;
}

/// <summary>
/// DTO for creating correlation log
/// </summary>
public class CreateCorrelationLogDto
{
    public string CorrelationId { get; set; } = string.Empty;
    public string? ParentCorrelationId { get; set; }
    public string? TraceId { get; set; }
    public string? SpanId { get; set; }
    public string OperationType { get; set; } = string.Empty;
    public string OperationName { get; set; } = string.Empty;
    public string? HttpMethod { get; set; }
    public string? Url { get; set; }
    public int? HttpStatusCode { get; set; }
    public string? RequestData { get; set; }
    public string? ResponseData { get; set; }
    public string? ErrorMessage { get; set; }
    public string? StackTrace { get; set; }
    public long DurationMs { get; set; }
    public string? Metadata { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public string? ClientIpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? ApplicationName { get; set; }
    public string? Environment { get; set; }
    public string Severity { get; set; } = "Info";
    public bool IsSuccess { get; set; } = true;
}

/// <summary>
/// DTO for updating correlation log
/// </summary>
public class UpdateCorrelationLogDto
{
    public string? ResponseData { get; set; }
    public string? ErrorMessage { get; set; }
    public string? StackTrace { get; set; }
    public long DurationMs { get; set; }
    public string? Metadata { get; set; }
    public int? HttpStatusCode { get; set; }
    public string Severity { get; set; } = "Info";
    public bool IsSuccess { get; set; } = true;
}

/// <summary>
/// DTO for correlation log list request
/// </summary>
public class GetCorrelationLogListDto : PagedAndSortedResultRequestDto
{
    public string? CorrelationId { get; set; }
    public string? ParentCorrelationId { get; set; }
    public string? TraceId { get; set; }
    public string? OperationType { get; set; }
    public string? Severity { get; set; }
    public bool? IsSuccess { get; set; }
    public Guid? UserId { get; set; }
    public string? Url { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public long? MinDurationMs { get; set; }
}

/// <summary>
/// DTO for correlation statistics
/// </summary>
public class CorrelationStatisticsDto
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

/// <summary>
/// DTO for correlation log search request
/// </summary>
public class SearchCorrelationLogsDto
{
    public string? SearchTerm { get; set; }
    public string? CorrelationId { get; set; }
    public string? OperationType { get; set; }
    public string? Severity { get; set; }
    public bool? IsSuccess { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int SkipCount { get; set; } = 0;
    public int MaxResultCount { get; set; } = 10;
    public string? Sorting { get; set; }
} 