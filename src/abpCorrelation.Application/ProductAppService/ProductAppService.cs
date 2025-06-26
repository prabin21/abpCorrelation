using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.Tracing;
using abpCorrelation.Application.Contracts.ProductAppService;
using abpCorrelation.Application.Contracts.ProductAppService.Dtos;
using abpCorrelation.Domain.Products;
using abpCorrelation.Domain.Correlation;

namespace abpCorrelation.Application.ProductAppService;

/// <summary>
/// Application service implementation for Product management with correlation ID tracking
/// </summary>
public class ProductAppService : ApplicationService, IProductAppService
{
    private readonly IRepository<Product, Guid> _productRepository;
    private readonly ICorrelationLogRepository _correlationLogRepository;
    private readonly ILogger<ProductAppService> _logger;
    private readonly IAsyncQueryableExecuter _asyncExecuter;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public ProductAppService(
        IRepository<Product, Guid> productRepository,
        ICorrelationLogRepository correlationLogRepository,
        ILogger<ProductAppService> logger,
        IAsyncQueryableExecuter asyncExecuter,
        ICorrelationIdProvider correlationIdProvider)
    {
        _productRepository = productRepository;
        _correlationLogRepository = correlationLogRepository;
        _logger = logger;
        _asyncExecuter = asyncExecuter;
        _correlationIdProvider = correlationIdProvider;
    }

    /// <summary>
    /// Get product by ID with correlation tracking
    /// </summary>
    public async Task<ProductDto> GetAsync(Guid id)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Getting product: {ProductId} with correlation: {CorrelationId}", id, correlationId);
            
            var product = await _productRepository.GetAsync(id);

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "GetProduct", 
                $"Retrieved product: {product.Name} (ID: {product.Id})", 
                product.Id, duration, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "GetProduct", 
                $"Error getting product: {id}", 
                id, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error getting product: {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Get product list with filtering and pagination
    /// </summary>
    public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Getting product list with correlation: {CorrelationId}", correlationId);
            
            var query = await _productRepository.GetQueryableAsync();
            query = query.Where(p => p.IsActive);

            // Apply filters
            if (!string.IsNullOrEmpty(input.Category))
                query = query.Where(p => p.Category == input.Category);

            if (!string.IsNullOrEmpty(input.Brand))
                query = query.Where(p => p.Brand == input.Brand);

            if (input.Status.HasValue)
                query = query.Where(p => p.Status == (abpCorrelation.Domain.Products.ProductStatus)input.Status.Value);

            if (input.MinPrice.HasValue)
                query = query.Where(p => p.Price >= input.MinPrice.Value);

            if (input.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= input.MaxPrice.Value);

            if (!string.IsNullOrEmpty(input.SearchTerm))
                query = query.Where(p => p.Name.Contains(input.SearchTerm) || 
                                       p.Description.Contains(input.SearchTerm) || 
                                       p.Tags.Contains(input.SearchTerm));

            // Apply sorting
            query = query.OrderBy(p => p.CreationTime);

            var totalCount = await _asyncExecuter.CountAsync(query);
            var products = await _asyncExecuter.ToListAsync(
                query.PageBy(input.SkipCount, input.MaxResultCount)
            );

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "GetProductList", 
                $"Retrieved product list. Total count: {totalCount}, Returned: {products.Count}", 
                null, duration, true, null, products.Count);

            return new PagedResultDto<ProductDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            };
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "GetProductList", 
                $"Error getting product list", 
                null, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error getting product list");
            throw;
        }
    }

    /// <summary>
    /// Search products by name, description, or tags with correlation tracking
    /// </summary>
    public async Task<ListResultDto<ProductDto>> SearchAsync(string searchTerm, int maxResultCount = 10)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Searching products with term: {SearchTerm} and correlation: {CorrelationId}", searchTerm, correlationId);
            
            var query = await _productRepository.GetQueryableAsync();
            query = query
                .Where(p => p.IsActive && 
                           (p.Name.Contains(searchTerm) || 
                            p.Description.Contains(searchTerm) || 
                            p.Tags.Contains(searchTerm)))
                .Take(maxResultCount);

            var products = await _asyncExecuter.ToListAsync(query);
            
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "SearchProducts", 
                $"Searched products with term: {searchTerm}", 
                null, duration, true, null, products.Count);
            
            return new ListResultDto<ProductDto>
            {
                Items = ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            };
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "SearchProducts", 
                $"Error searching products with term: {searchTerm}", 
                null, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error searching products with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    /// <summary>
    /// Create a new product with correlation tracking
    /// </summary>
    public async Task<ProductDto> CreateAsync(CreateProductDto input)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Creating new product: {ProductName} with correlation: {CorrelationId}", input.Name, correlationId);
            
            // Check if SKU already exists
            if (await ExistsBySkuAsync(input.Sku))
            {
                throw new BusinessException("Product with this SKU already exists");
            }

            var product = ObjectMapper.Map<CreateProductDto, Product>(input);
            product = await _productRepository.InsertAsync(product);

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "CreateProduct", 
                $"Created new product: {product.Name} with SKU: {product.Sku}", 
                product.Id, duration, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "CreateProduct", 
                $"Error creating product: {input.Name}", 
                null, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error creating product: {ProductName}", input.Name);
            throw;
        }
    }

    /// <summary>
    /// Update an existing product with correlation tracking
    /// </summary>
    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Updating product: {ProductId} with correlation: {CorrelationId}", id, correlationId);
            
            var product = await _productRepository.GetAsync(id);
            var oldName = product.Name;
            
            ObjectMapper.Map(input, product);
            product = await _productRepository.UpdateAsync(product);

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "UpdateProduct", 
                $"Updated product: {product.Name} (ID: {product.Id}). Old name: {oldName}", 
                product.Id, duration, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "UpdateProduct", 
                $"Error updating product: {id}", 
                id, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error updating product: {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Delete a product with correlation tracking
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Deleting product: {ProductId} with correlation: {CorrelationId}", id, correlationId);
            
            var product = await _productRepository.GetAsync(id);
            var productName = product.Name;
            
            await _productRepository.DeleteAsync(product);

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "DeleteProduct", 
                $"Deleted product: {productName} (ID: {id})", 
                id, duration, true);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "DeleteProduct", 
                $"Error deleting product: {id}", 
                id, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error deleting product: {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Add stock to a product with correlation tracking
    /// </summary>
    public async Task<ProductDto> AddStockAsync(StockOperationDto input)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Adding stock to product {ProductId}: {Quantity} with correlation: {CorrelationId}", 
                input.ProductId, input.Quantity, correlationId);
            
            var product = await _productRepository.GetAsync(input.ProductId);
            var oldStock = product.StockQuantity;
            
            // Business logic: Add stock and update status
            product.StockQuantity += input.Quantity;
            
            // Update product status based on stock level
            if (product.StockQuantity <= product.MinStockLevel)
            {
                product.Status = abpCorrelation.Domain.Products.ProductStatus.LowStock;
            }
            else if (product.StockQuantity > 0)
            {
                product.Status = abpCorrelation.Domain.Products.ProductStatus.InStock;
            }
            
            await _productRepository.UpdateAsync(product);

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "AddStock", 
                $"Added {input.Quantity} stock to product {product.Name}. Old stock: {oldStock}, New stock: {product.StockQuantity}. Reason: {input.Reason}", 
                product.Id, duration, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "AddStock", 
                $"Error adding stock to product {input.ProductId}", 
                input.ProductId, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error adding stock to product {ProductId}", input.ProductId);
            throw;
        }
    }

    /// <summary>
    /// Remove stock from a product with correlation tracking
    /// </summary>
    public async Task<ProductDto> RemoveStockAsync(StockOperationDto input)
    {
        var correlationId = _correlationIdProvider.Get();
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Removing stock from product {ProductId}: {Quantity} with correlation: {CorrelationId}", 
                input.ProductId, input.Quantity, correlationId);
            
            var product = await _productRepository.GetAsync(input.ProductId);
            var oldStock = product.StockQuantity;
            
            // Business logic: Remove stock and update status
            if (product.StockQuantity < input.Quantity)
            {
                throw new BusinessException($"Cannot remove {input.Quantity} stock. Only {product.StockQuantity} available.");
            }
            
            product.StockQuantity -= input.Quantity;
            
            // Update product status based on stock level
            if (product.StockQuantity == 0)
            {
                product.Status = abpCorrelation.Domain.Products.ProductStatus.OutOfStock;
            }
            else if (product.StockQuantity <= product.MinStockLevel)
            {
                product.Status = abpCorrelation.Domain.Products.ProductStatus.LowStock;
            }
            
            await _productRepository.UpdateAsync(product);

            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "RemoveStock", 
                $"Removed {input.Quantity} stock from product {product.Name}. Old stock: {oldStock}, New stock: {product.StockQuantity}. Reason: {input.Reason}", 
                product.Id, duration, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }
        catch (Exception ex)
        {
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogCorrelationAsync(correlationId, "RemoveStock", 
                $"Error removing stock from product {input.ProductId}", 
                input.ProductId, duration, false, ex.Message);
            
            _logger.LogError(ex, "Error removing stock from product {ProductId}", input.ProductId);
            throw;
        }
    }

    /// <summary>
    /// Helper method to log correlation information with proper correlation ID tracking
    /// </summary>
    private async Task LogCorrelationAsync(
        string correlationId, 
        string operation, 
        string details, 
        Guid? entityId = null, 
        double duration = 0, 
        bool isSuccess = true, 
        string? errorMessage = null, 
        int? recordCount = null)
    {
        try
        {
            var correlationLog = new CorrelationLog
            {
                CorrelationId = correlationId,
                OperationType = "PRODUCT_OPERATION",
                OperationName = operation,
                RequestData = details,
                ResponseData = recordCount.HasValue ? $"Records affected: {recordCount}" : null,
                DurationMs = (long)duration,
                Severity = isSuccess ? "Info" : "Error",
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage,
                ApplicationName = "abpCorrelation",
                Environment = "Development" // You can get this from configuration
            };

            await _correlationLogRepository.InsertAsync(correlationLog);
            
            _logger.LogInformation("Correlation logged: {CorrelationId} - {Operation} - {Details}", 
                correlationId, operation, details);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to log correlation information for operation: {Operation} with correlation: {CorrelationId}", 
                operation, correlationId);
        }
    }

    /// <summary>
    /// Helper method to check if product exists by SKU
    /// </summary>
    private async Task<bool> ExistsBySkuAsync(string sku)
    {
        return await _productRepository.AnyAsync(p => p.Sku == sku);
    }
} 