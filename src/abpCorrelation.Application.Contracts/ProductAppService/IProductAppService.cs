using abpCorrelation.Application.Contracts.ProductAppService.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace abpCorrelation.Application.Contracts.ProductAppService;

/// <summary>
/// Application service interface for Product management with correlation ID tracking
/// Provides essential CRUD operations and business logic for products
/// </summary>
public interface IProductAppService : IApplicationService
{
    /// <summary>
    /// Get product by ID with correlation tracking
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product DTO</returns>
    Task<ProductDto> GetAsync(Guid id);

    /// <summary>
    /// Get product list with filtering and pagination
    /// </summary>
    /// <param name="input">Filter and pagination parameters</param>
    /// <returns>Paged list of products</returns>
    Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input);

    /// <summary>
    /// Search products by name, description, or tags with correlation tracking
    /// </summary>
    /// <param name="searchTerm">Search term to look for</param>
    /// <param name="maxResultCount">Maximum number of results to return</param>
    /// <returns>List of matching products</returns>
    Task<ListResultDto<ProductDto>> SearchAsync(string searchTerm, int maxResultCount = 10);

    /// <summary>
    /// Create a new product with correlation tracking
    /// </summary>
    /// <param name="input">Product creation data</param>
    /// <returns>Created product DTO</returns>
    Task<ProductDto> CreateAsync(CreateProductDto input);

    /// <summary>
    /// Update an existing product with correlation tracking
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="input">Product update data</param>
    /// <returns>Updated product DTO</returns>
    Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input);

    /// <summary>
    /// Delete a product with correlation tracking
    /// </summary>
    /// <param name="id">Product ID</param>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Add stock to a product with correlation tracking
    /// </summary>
    /// <param name="input">Stock operation details</param>
    /// <returns>Updated product DTO</returns>
    Task<ProductDto> AddStockAsync(StockOperationDto input);

    /// <summary>
    /// Remove stock from a product with correlation tracking
    /// </summary>
    /// <param name="input">Stock operation details</param>
    /// <returns>Updated product DTO</returns>
    Task<ProductDto> RemoveStockAsync(StockOperationDto input);
} 