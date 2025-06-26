using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using abpCorrelation.Domain.Correlation;
using abpCorrelation.EntityFrameworkCore;

namespace abpCorrelation.EntityFrameworkCore.Correlation;

/// <summary>
/// Repository implementation for correlation logs
/// </summary>
public class CorrelationLogRepository : EfCoreRepository<abpCorrelationDbContext, CorrelationLog, Guid>, ICorrelationLogRepository
{
    public CorrelationLogRepository(IDbContextProvider<abpCorrelationDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }

    public async Task<List<CorrelationLog>> GetByCorrelationIdAsync(string correlationId)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.CorrelationLogs
            .Where(x => x.CorrelationId == correlationId)
            .OrderBy(x => x.CreationTime)
            .ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetByParentCorrelationIdAsync(string parentCorrelationId)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.CorrelationLogs
            .Where(x => x.ParentCorrelationId == parentCorrelationId)
            .OrderBy(x => x.CreationTime)
            .ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetByTraceIdAsync(string traceId)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.CorrelationLogs
            .Where(x => x.TraceId == traceId)
            .OrderBy(x => x.CreationTime)
            .ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetByUserIdAsync(Guid userId, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.Where(x => x.UserId == userId);

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        return await query.OrderByDescending(x => x.CreationTime).ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetByOperationTypeAsync(string operationType, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.Where(x => x.OperationType == operationType);

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        return await query.OrderByDescending(x => x.CreationTime).ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetBySeverityAsync(string severity, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.Where(x => x.Severity == severity);

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        return await query.OrderByDescending(x => x.CreationTime).ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.CorrelationLogs
            .Where(x => x.CreationTime >= fromDate && x.CreationTime <= toDate)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetErrorsAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.Where(x => !x.IsSuccess);

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        return await query.OrderByDescending(x => x.CreationTime).ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetSlowOperationsAsync(long minDurationMs, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.Where(x => x.DurationMs >= minDurationMs);

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        return await query.OrderByDescending(x => x.DurationMs).ToListAsync();
    }

    public async Task<List<CorrelationLog>> GetByUrlAsync(string url, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.Where(x => x.Url == url);

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        return await query.OrderByDescending(x => x.CreationTime).ToListAsync();
    }

    public async Task<(List<CorrelationLog> Items, long TotalCount)> GetPagedAsync(
        int skipCount,
        int maxResultCount,
        string? correlationId = null,
        string? operationType = null,
        string? severity = null,
        bool? isSuccess = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        string? sorting = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(correlationId))
            query = query.Where(x => x.CorrelationId == correlationId);

        if (!string.IsNullOrEmpty(operationType))
            query = query.Where(x => x.OperationType == operationType);

        if (!string.IsNullOrEmpty(severity))
            query = query.Where(x => x.Severity == severity);

        if (isSuccess.HasValue)
            query = query.Where(x => x.IsSuccess == isSuccess.Value);

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        // Apply sorting
        if (!string.IsNullOrEmpty(sorting))
        {
            query = sorting.ToLower() switch
            {
                "creationtime desc" => query.OrderByDescending(x => x.CreationTime),
                "creationtime asc" => query.OrderBy(x => x.CreationTime),
                "durationms desc" => query.OrderByDescending(x => x.DurationMs),
                "durationms asc" => query.OrderBy(x => x.DurationMs),
                _ => query.OrderByDescending(x => x.CreationTime)
            };
        }
        else
        {
            query = query.OrderByDescending(x => x.CreationTime);
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip(skipCount).Take(maxResultCount).ToListAsync();

        return (items, totalCount);
    }

    public async Task<CorrelationStatistics> GetStatisticsAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        var totalLogs = await query.CountAsync();
        var errorLogs = await query.CountAsync(x => !x.IsSuccess);
        var successLogs = await query.CountAsync(x => x.IsSuccess);

        var durationStats = await query
            .Where(x => x.DurationMs > 0)
            .Select(x => x.DurationMs)
            .ToListAsync();

        var averageDuration = durationStats.Any() ? durationStats.Average() : 0;
        var maxDuration = durationStats.Any() ? durationStats.Max() : 0;
        var minDuration = durationStats.Any() ? durationStats.Min() : 0;

        var operationTypeCounts = await query
            .GroupBy(x => x.OperationType)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Key, x => (long)x.Count);

        var severityCounts = await query
            .GroupBy(x => x.Severity)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Key, x => (long)x.Count);

        var httpStatusCodeCounts = await query
            .Where(x => x.HttpStatusCode.HasValue)
            .GroupBy(x => x.HttpStatusCode.Value)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Key, x => (long)x.Count);

        return new CorrelationStatistics
        {
            TotalLogs = totalLogs,
            ErrorLogs = errorLogs,
            SuccessLogs = successLogs,
            AverageDurationMs = averageDuration,
            MaxDurationMs = maxDuration,
            MinDurationMs = minDuration,
            OperationTypeCounts = operationTypeCounts,
            SeverityCounts = severityCounts,
            HttpStatusCodeCounts = httpStatusCodeCounts
        };
    }

    public async Task<int> CleanupOldLogsAsync(DateTime cutoffDate)
    {
        var dbContext = await GetDbContextAsync();
        var logsToDelete = await dbContext.CorrelationLogs
            .Where(x => x.CreationTime < cutoffDate)
            .ToListAsync();

        dbContext.CorrelationLogs.RemoveRange(logsToDelete);
        await dbContext.SaveChangesAsync();

        return logsToDelete.Count;
    }

    public async Task<List<string>> GetUniqueCorrelationIdsAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.CorrelationLogs.AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(x => x.CreationTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.CreationTime <= toDate.Value);

        return await query
            .Select(x => x.CorrelationId)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }
} 