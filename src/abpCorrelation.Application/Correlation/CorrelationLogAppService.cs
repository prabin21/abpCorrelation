using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using abpCorrelation.Domain.Correlation;
using abpCorrelation.Application.Contracts.Correlation;

namespace abpCorrelation.Application.Correlation;

/// <summary>
/// Application service for correlation log management, using IRepository directly (no custom repository)
/// </summary>
public class CorrelationLogAppService : ApplicationService, ICorrelationLogAppService
{
    private readonly IRepository<CorrelationLog, Guid> _correlationLogRepository;
    private readonly IAsyncQueryableExecuter _asyncExecuter;

    public CorrelationLogAppService(
        IRepository<CorrelationLog, Guid> correlationLogRepository,
        IAsyncQueryableExecuter asyncExecuter)
    {
        _correlationLogRepository = correlationLogRepository;
        _asyncExecuter = asyncExecuter;
    }

    // CRUD
    public async Task<CorrelationLogDto> GetAsync(Guid id)
    {
        var entity = await _correlationLogRepository.GetAsync(id);
        return ObjectMapper.Map<CorrelationLog, CorrelationLogDto>(entity);
    }

    public async Task<PagedResultDto<CorrelationLogDto>> GetListAsync(GetCorrelationLogListDto input)
    {
        var query = await _correlationLogRepository.GetQueryableAsync();
        if (!string.IsNullOrEmpty(input.CorrelationId))
            query = query.Where(x => x.CorrelationId == input.CorrelationId);
        if (!string.IsNullOrEmpty(input.ParentCorrelationId))
            query = query.Where(x => x.ParentCorrelationId == input.ParentCorrelationId);
        if (!string.IsNullOrEmpty(input.TraceId))
            query = query.Where(x => x.TraceId == input.TraceId);
        if (!string.IsNullOrEmpty(input.OperationType))
            query = query.Where(x => x.OperationType == input.OperationType);
        if (!string.IsNullOrEmpty(input.Severity))
            query = query.Where(x => x.Severity == input.Severity);
        if (input.IsSuccess.HasValue)
            query = query.Where(x => x.IsSuccess == input.IsSuccess);
        if (input.UserId.HasValue)
            query = query.Where(x => x.UserId == input.UserId);
        if (!string.IsNullOrEmpty(input.Url))
            query = query.Where(x => x.Url == input.Url);
        if (input.FromDate.HasValue)
            query = query.Where(x => x.CreationTime >= input.FromDate);
        if (input.ToDate.HasValue)
            query = query.Where(x => x.CreationTime <= input.ToDate);
        if (input.MinDurationMs.HasValue)
            query = query.Where(x => x.DurationMs >= input.MinDurationMs);
        var totalCount = await _asyncExecuter.CountAsync(query);
        var items = await _asyncExecuter.ToListAsync(query.OrderByDescending(x => x.CreationTime)
            .PageBy(input.SkipCount, input.MaxResultCount));
        return new PagedResultDto<CorrelationLogDto>(totalCount, ObjectMapper.Map<List<CorrelationLog>, List<CorrelationLogDto>>(items));
    }

    public async Task<CorrelationLogDto> CreateAsync(CreateCorrelationLogDto input)
    {
        var entity = ObjectMapper.Map<CreateCorrelationLogDto, CorrelationLog>(input);
        entity = await _correlationLogRepository.InsertAsync(entity, autoSave: true);
        return ObjectMapper.Map<CorrelationLog, CorrelationLogDto>(entity);
    }

    public async Task<CorrelationLogDto> UpdateAsync(Guid id, UpdateCorrelationLogDto input)
    {
        var entity = await _correlationLogRepository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        entity = await _correlationLogRepository.UpdateAsync(entity, autoSave: true);
        return ObjectMapper.Map<CorrelationLog, CorrelationLogDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _correlationLogRepository.DeleteAsync(id);
    }

    // Custom methods (examples, implement more as needed)
    public async Task<List<CorrelationLogDto>> GetByCorrelationIdAsync(string correlationId)
    {
        var query = await _correlationLogRepository.GetQueryableAsync();
        var items = await _asyncExecuter.ToListAsync(query.Where(x => x.CorrelationId == correlationId));
        return ObjectMapper.Map<List<CorrelationLog>, List<CorrelationLogDto>>(items);
    }

    public async Task<List<CorrelationLogDto>> GetByParentCorrelationIdAsync(string parentCorrelationId)
    {
        var query = await _correlationLogRepository.GetQueryableAsync();
        var items = await _asyncExecuter.ToListAsync(query.Where(x => x.ParentCorrelationId == parentCorrelationId));
        return ObjectMapper.Map<List<CorrelationLog>, List<CorrelationLogDto>>(items);
    }

    public async Task<List<CorrelationLogDto>> GetByTraceIdAsync(string traceId)
    {
        var query = await _correlationLogRepository.GetQueryableAsync();
        var items = await _asyncExecuter.ToListAsync(query.Where(x => x.TraceId == traceId));
        return ObjectMapper.Map<List<CorrelationLog>, List<CorrelationLogDto>>(items);
    }

    // Add more methods as needed, following the same pattern
} 