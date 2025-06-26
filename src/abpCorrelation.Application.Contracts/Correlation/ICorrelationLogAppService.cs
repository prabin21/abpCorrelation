using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace abpCorrelation.Application.Contracts.Correlation;

/// <summary>
/// Application service for correlation log management (CRUD + basic queries)
/// </summary>
public interface ICorrelationLogAppService : IApplicationService
{
    Task<CorrelationLogDto> GetAsync(Guid id);
    Task<PagedResultDto<CorrelationLogDto>> GetListAsync(GetCorrelationLogListDto input);
    Task<CorrelationLogDto> CreateAsync(CreateCorrelationLogDto input);
    Task<CorrelationLogDto> UpdateAsync(Guid id, UpdateCorrelationLogDto input);
    Task DeleteAsync(Guid id);
    Task<List<CorrelationLogDto>> GetByCorrelationIdAsync(string correlationId);
    Task<List<CorrelationLogDto>> GetByParentCorrelationIdAsync(string parentCorrelationId);
    Task<List<CorrelationLogDto>> GetByTraceIdAsync(string traceId);
} 