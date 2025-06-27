using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using abpCorrelation.Domain.Products;
using abpCorrelation.Application.Contracts.ProductAppService.Orders;
using abpCorrelation.Application.Contracts.Correlation;
using Volo.Abp.Tracing;
using System.Linq;
using System.Collections.Generic;
using abpCorrelation.Application.Contracts.ProductAppService;
using abpCorrelation.Application.Contracts.ProductAppService.Dtos;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Microsoft.FeatureManagement;
using Volo.Abp.Uow;

namespace abpCorrelation.Application.ProductAppService.Orders;

public class OrderAppService : ApplicationService, IOrderAppService
{
    private readonly IRepository<Order, Guid> _orderRepository;
    private readonly ICorrelationIdProvider _correlationIdProvider;
    private readonly ICorrelationLogAppService _correlationLogAppService;
    private readonly IProductAppService _productAppService;
    private readonly ILogger<OrderAppService> _logger;
    private readonly IFeatureManager _featureManager;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public OrderAppService(
        IRepository<Order, Guid> orderRepository,
        ICorrelationIdProvider correlationIdProvider,
        ICorrelationLogAppService correlationLogAppService,
        IProductAppService productAppService,
        ILogger<OrderAppService> logger,
        IFeatureManager featureManager,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _orderRepository = orderRepository;
        _correlationIdProvider = correlationIdProvider;
        _correlationLogAppService = correlationLogAppService;
        _productAppService = productAppService;
        _logger = logger;
        _featureManager = featureManager;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto input)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        try
        {
            if (!await _featureManager.IsEnabledAsync("OrderSubmissionTimeWindow"))
            {
                _logger.LogWarning("Order submission attempted outside allowed time window. CorrelationId: {CorrelationId}", correlationId);
                throw new UserFriendlyException("Order submission is currently disabled. Please try again later.");
            }
            _logger.LogInformation("Creating order: {OrderNumber}, ProductId: {ProductId}, Quantity: {Quantity}, CorrelationId: {CorrelationId}", input.OrderNumber, input.ProductId, input.Quantity, correlationId);
            // 1. Call ProductAppService to remove stock for the product
            await _productAppService.RemoveStockAsync(new StockOperationDto
            {
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                Reason = $"Order {input.OrderNumber} placed"
            });

            // 2. Save the order
            var order = ObjectMapper.Map<CreateOrderDto, Order>(input);
            order = await _orderRepository.InsertAsync(order, autoSave: true);

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            // 3. Log the order creation in the correlation log
            await _correlationLogAppService.CreateAsync(new CreateCorrelationLogDto
            {
                CorrelationId = correlationId,
                OperationType = "ORDER_OPERATION",
                OperationName = "CreateOrder",
                RequestData = $"OrderNumber: {order.OrderNumber}, ProductId: {order.ProductId}, Quantity: {order.Quantity}",
                ResponseData = $"OrderId: {order.Id}",
                DurationMs = (long)duration,
                Severity = "Info",
                IsSuccess = true,
                ApplicationName = "abpCorrelation",
                Environment = "Development"
            });

            _logger.LogInformation("Order created successfully: {OrderId}, CorrelationId: {CorrelationId}", order.Id, correlationId);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            _logger.LogError(ex, "Error creating order: {OrderNumber}, ProductId: {ProductId}, CorrelationId: {CorrelationId}", input.OrderNumber, input.ProductId, correlationId);
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _correlationLogAppService.CreateAsync(new CreateCorrelationLogDto
                {
                    CorrelationId = correlationId,
                    OperationType = "ORDER_OPERATION",
                    OperationName = "CreateOrder",
                    RequestData = $"OrderNumber: {input.OrderNumber}, ProductId: {input.ProductId}, Quantity: {input.Quantity}",
                    ResponseData = ex.Message,
                    DurationMs = (long)duration,
                    Severity = "Error",
                    IsSuccess = false,
                    ApplicationName = "abpCorrelation",
                    Environment = "Development"
                });
                await uow.CompleteAsync();
            }
            throw;
        }
    }

    public async Task<OrderDto> GetAsync(Guid id)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        try
        {
            _logger.LogInformation("Getting order: {OrderId}, CorrelationId: {CorrelationId}", id, correlationId);
            var order = await _orderRepository.GetAsync(id);
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await _correlationLogAppService.CreateAsync(new CreateCorrelationLogDto
            {
                CorrelationId = correlationId,
                OperationType = "ORDER_OPERATION",
                OperationName = "GetOrder",
                RequestData = $"OrderId: {id}",
                ResponseData = $"OrderNumber: {order.OrderNumber}, ProductId: {order.ProductId}, Quantity: {order.Quantity}",
                DurationMs = (long)duration,
                Severity = "Info",
                IsSuccess = true,
                ApplicationName = "abpCorrelation",
                Environment = "Development"
            });
            _logger.LogInformation("Order retrieved successfully: {OrderId}, CorrelationId: {CorrelationId}", id, correlationId);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            _logger.LogError(ex, "Error getting order: {OrderId}, CorrelationId: {CorrelationId}", id, correlationId);
            await _correlationLogAppService.CreateAsync(new CreateCorrelationLogDto
            {
                CorrelationId = correlationId,
                OperationType = "ORDER_OPERATION",
                OperationName = "GetOrder",
                RequestData = $"OrderId: {id}",
                ResponseData = ex.Message,
                DurationMs = (long)duration,
                Severity = "Error",
                IsSuccess = false,
                ApplicationName = "abpCorrelation",
                Environment = "Development"
            });
            throw;
        }
    }

    public async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        try
        {
            _logger.LogInformation("Getting order list: SkipCount={SkipCount}, MaxResultCount={MaxResultCount}, CorrelationId: {CorrelationId}", input.SkipCount, input.MaxResultCount, correlationId);
            var query = await _orderRepository.GetQueryableAsync();
            var totalCount = await AsyncExecuter.CountAsync(query);
            var items = await AsyncExecuter.ToListAsync(query.PageBy(input.SkipCount, input.MaxResultCount));
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await _correlationLogAppService.CreateAsync(new CreateCorrelationLogDto
            {
                CorrelationId = correlationId,
                OperationType = "ORDER_OPERATION",
                OperationName = "GetOrderList",
                RequestData = $"SkipCount: {input.SkipCount}, MaxResultCount: {input.MaxResultCount}",
                ResponseData = $"TotalCount: {totalCount}, Returned: {items.Count}",
                DurationMs = (long)duration,
                Severity = "Info",
                IsSuccess = true,
                ApplicationName = "abpCorrelation",
                Environment = "Development"
            });
            _logger.LogInformation("Order list retrieved successfully: TotalCount={TotalCount}, CorrelationId: {CorrelationId}", totalCount, correlationId);
            return new PagedResultDto<OrderDto>(totalCount, ObjectMapper.Map<List<Order>, List<OrderDto>>(items));
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            _logger.LogError(ex, "Error getting order list: CorrelationId: {CorrelationId}", correlationId);
            await _correlationLogAppService.CreateAsync(new CreateCorrelationLogDto
            {
                CorrelationId = correlationId,
                OperationType = "ORDER_OPERATION",
                OperationName = "GetOrderList",
                RequestData = $"SkipCount: {input.SkipCount}, MaxResultCount: {input.MaxResultCount}",
                ResponseData = ex.Message,
                DurationMs = (long)duration,
                Severity = "Error",
                IsSuccess = false,
                ApplicationName = "abpCorrelation",
                Environment = "Development"
            });
            throw;
        }
    }
} 