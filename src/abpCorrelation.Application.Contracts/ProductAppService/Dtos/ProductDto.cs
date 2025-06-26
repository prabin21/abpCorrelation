using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace abpCorrelation.Application.Contracts.ProductAppService.Dtos;

/// <summary>
/// Main Product DTO for API responses - contains all product information
/// </summary>
public class ProductDto : AuditedEntityDto<Guid>
{
    /// <summary>
    /// Product name - required field for product identification
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description - detailed information about the product
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Product SKU (Stock Keeping Unit) - unique identifier for inventory management
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Product price - current selling price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Product cost - cost price for profit calculation
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Product category - classification of the product
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Product brand - manufacturer or brand name
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Product weight in grams - for shipping calculations
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Product dimensions - length x width x height in cm
    /// </summary>
    public string? Dimensions { get; set; }

    /// <summary>
    /// Product color - available colors for the product
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Product size - available sizes for the product
    /// </summary>
    public string? Size { get; set; }

    /// <summary>
    /// Product material - material composition
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Product images - comma-separated list of image URLs
    /// </summary>
    public string? Images { get; set; }

    /// <summary>
    /// Product tags - comma-separated tags for search and filtering
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Product specifications - JSON string containing technical specifications
    /// </summary>
    public string? Specifications { get; set; }

    /// <summary>
    /// Product warranty information
    /// </summary>
    public string? Warranty { get; set; }

    /// <summary>
    /// Product availability status (InStock, OutOfStock, Discontinued, etc.)
    /// </summary>
    public ProductStatus Status { get; set; } = ProductStatus.InStock;

    /// <summary>
    /// Product stock quantity - current available inventory
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Minimum stock level - threshold for low stock alerts
    /// </summary>
    public int MinStockLevel { get; set; }

    /// <summary>
    /// Maximum stock level - maximum inventory capacity
    /// </summary>
    public int MaxStockLevel { get; set; }

    /// <summary>
    /// Product rating - average customer rating (1-5 stars)
    /// </summary>
    public decimal Rating { get; set; }

    /// <summary>
    /// Number of reviews - total customer reviews count
    /// </summary>
    public int ReviewCount { get; set; }

    /// <summary>
    /// Product popularity score - for recommendation algorithms
    /// </summary>
    public int PopularityScore { get; set; }

    /// <summary>
    /// Product is featured - for promotional displays
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Product is active - soft delete flag
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Product launch date - when the product was first introduced
    /// </summary>
    public DateTime? LaunchDate { get; set; }

    /// <summary>
    /// Product expiry date - for perishable products
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Product barcode - for scanning and inventory management
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Product manufacturer - company that produces the product
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Product country of origin
    /// </summary>
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// Product shipping class - for shipping cost calculations
    /// </summary>
    public string? ShippingClass { get; set; }

    /// <summary>
    /// Product tax class - for tax calculations
    /// </summary>
    public string? TaxClass { get; set; }

    /// <summary>
    /// Product SEO title - for search engine optimization
    /// </summary>
    public string? SeoTitle { get; set; }

    /// <summary>
    /// Product SEO description - for search engine optimization
    /// </summary>
    public string? SeoDescription { get; set; }

    /// <summary>
    /// Product SEO keywords - for search engine optimization
    /// </summary>
    public string? SeoKeywords { get; set; }

    /// <summary>
    /// Product meta data - additional JSON data for extensibility
    /// </summary>
    public string? MetaData { get; set; }

    /// <summary>
    /// Tenant ID for multi-tenancy support
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Calculated profit margin percentage
    /// </summary>
    public decimal ProfitMargin { get; set; }

    /// <summary>
    /// Indicates if product is in low stock
    /// </summary>
    public bool IsLowStock { get; set; }

    /// <summary>
    /// Indicates if product is out of stock
    /// </summary>
    public bool IsOutOfStock { get; set; }
}

/// <summary>
/// DTO for creating a new product - contains only the fields needed for creation
/// </summary>
public class CreateProductDto
{
    /// <summary>
    /// Product name - required field for product identification
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description - detailed information about the product
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Product SKU (Stock Keeping Unit) - unique identifier for inventory management
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Product price - current selling price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Product cost - cost price for profit calculation
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Product category - classification of the product
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Product brand - manufacturer or brand name
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Product weight in grams - for shipping calculations
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Product dimensions - length x width x height in cm
    /// </summary>
    public string? Dimensions { get; set; }

    /// <summary>
    /// Product color - available colors for the product
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Product size - available sizes for the product
    /// </summary>
    public string? Size { get; set; }

    /// <summary>
    /// Product material - material composition
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Product images - comma-separated list of image URLs
    /// </summary>
    public string? Images { get; set; }

    /// <summary>
    /// Product tags - comma-separated tags for search and filtering
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Product specifications - JSON string containing technical specifications
    /// </summary>
    public string? Specifications { get; set; }

    /// <summary>
    /// Product warranty information
    /// </summary>
    public string? Warranty { get; set; }

    /// <summary>
    /// Product stock quantity - current available inventory
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Minimum stock level - threshold for low stock alerts
    /// </summary>
    public int MinStockLevel { get; set; }

    /// <summary>
    /// Maximum stock level - maximum inventory capacity
    /// </summary>
    public int MaxStockLevel { get; set; }

    /// <summary>
    /// Product is featured - for promotional displays
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Product launch date - when the product was first introduced
    /// </summary>
    public DateTime? LaunchDate { get; set; }

    /// <summary>
    /// Product expiry date - for perishable products
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Product barcode - for scanning and inventory management
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Product manufacturer - company that produces the product
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Product country of origin
    /// </summary>
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// Product shipping class - for shipping cost calculations
    /// </summary>
    public string? ShippingClass { get; set; }

    /// <summary>
    /// Product tax class - for tax calculations
    /// </summary>
    public string? TaxClass { get; set; }

    /// <summary>
    /// Product SEO title - for search engine optimization
    /// </summary>
    public string? SeoTitle { get; set; }

    /// <summary>
    /// Product SEO description - for search engine optimization
    /// </summary>
    public string? SeoDescription { get; set; }

    /// <summary>
    /// Product SEO keywords - for search engine optimization
    /// </summary>
    public string? SeoKeywords { get; set; }

    /// <summary>
    /// Product meta data - additional JSON data for extensibility
    /// </summary>
    public string? MetaData { get; set; }
}

/// <summary>
/// DTO for updating an existing product - contains only the fields that can be updated
/// </summary>
public class UpdateProductDto
{
    /// <summary>
    /// Product name - required field for product identification
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description - detailed information about the product
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Product price - current selling price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Product cost - cost price for profit calculation
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Product category - classification of the product
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Product brand - manufacturer or brand name
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Product weight in grams - for shipping calculations
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Product dimensions - length x width x height in cm
    /// </summary>
    public string? Dimensions { get; set; }

    /// <summary>
    /// Product color - available colors for the product
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Product size - available sizes for the product
    /// </summary>
    public string? Size { get; set; }

    /// <summary>
    /// Product material - material composition
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Product images - comma-separated list of image URLs
    /// </summary>
    public string? Images { get; set; }

    /// <summary>
    /// Product tags - comma-separated tags for search and filtering
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Product specifications - JSON string containing technical specifications
    /// </summary>
    public string? Specifications { get; set; }

    /// <summary>
    /// Product warranty information
    /// </summary>
    public string? Warranty { get; set; }

    /// <summary>
    /// Product stock quantity - current available inventory
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Minimum stock level - threshold for low stock alerts
    /// </summary>
    public int MinStockLevel { get; set; }

    /// <summary>
    /// Maximum stock level - maximum inventory capacity
    /// </summary>
    public int MaxStockLevel { get; set; }

    /// <summary>
    /// Product is featured - for promotional displays
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Product is active - soft delete flag
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Product launch date - when the product was first introduced
    /// </summary>
    public DateTime? LaunchDate { get; set; }

    /// <summary>
    /// Product expiry date - for perishable products
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Product barcode - for scanning and inventory management
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Product manufacturer - company that produces the product
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Product country of origin
    /// </summary>
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// Product shipping class - for shipping cost calculations
    /// </summary>
    public string? ShippingClass { get; set; }

    /// <summary>
    /// Product tax class - for tax calculations
    /// </summary>
    public string? TaxClass { get; set; }

    /// <summary>
    /// Product SEO title - for search engine optimization
    /// </summary>
    public string? SeoTitle { get; set; }

    /// <summary>
    /// Product SEO description - for search engine optimization
    /// </summary>
    public string? SeoDescription { get; set; }

    /// <summary>
    /// Product SEO keywords - for search engine optimization
    /// </summary>
    public string? SeoKeywords { get; set; }

    /// <summary>
    /// Product meta data - additional JSON data for extensibility
    /// </summary>
    public string? MetaData { get; set; }
}

/// <summary>
/// DTO for product list requests with filtering and pagination
/// </summary>
public class GetProductListDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// Filter by product category
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Filter by product brand
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Filter by product status
    /// </summary>
    public ProductStatus? Status { get; set; }

    /// <summary>
    /// Filter by active status
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Filter by featured status
    /// </summary>
    public bool? IsFeatured { get; set; }

    /// <summary>
    /// Filter by minimum price
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Filter by maximum price
    /// </summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Search term for name, description, or tags
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Filter by manufacturer
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Filter by country of origin
    /// </summary>
    public string? CountryOfOrigin { get; set; }
}

/// <summary>
/// DTO for product statistics
/// </summary>
public class ProductStatisticsDto
{
    /// <summary>
    /// Total number of products
    /// </summary>
    public long TotalProducts { get; set; }

    /// <summary>
    /// Number of active products
    /// </summary>
    public long ActiveProducts { get; set; }

    /// <summary>
    /// Number of inactive products
    /// </summary>
    public long InactiveProducts { get; set; }

    /// <summary>
    /// Number of featured products
    /// </summary>
    public long FeaturedProducts { get; set; }

    /// <summary>
    /// Number of products in stock
    /// </summary>
    public long InStockProducts { get; set; }

    /// <summary>
    /// Number of products with low stock
    /// </summary>
    public long LowStockProducts { get; set; }

    /// <summary>
    /// Number of out of stock products
    /// </summary>
    public long OutOfStockProducts { get; set; }

    /// <summary>
    /// Number of discontinued products
    /// </summary>
    public long DiscontinuedProducts { get; set; }

    /// <summary>
    /// Average product price
    /// </summary>
    public decimal AveragePrice { get; set; }

    /// <summary>
    /// Average product rating
    /// </summary>
    public decimal AverageRating { get; set; }

    /// <summary>
    /// Total inventory value
    /// </summary>
    public decimal TotalInventoryValue { get; set; }

    /// <summary>
    /// Count of products by category
    /// </summary>
    public Dictionary<string, long> ProductsByCategory { get; set; } = new();

    /// <summary>
    /// Count of products by brand
    /// </summary>
    public Dictionary<string, long> ProductsByBrand { get; set; } = new();

    /// <summary>
    /// Count of products by status
    /// </summary>
    public Dictionary<ProductStatus, long> ProductsByStatus { get; set; } = new();
}

/// <summary>
/// DTO for stock management operations
/// </summary>
public class StockOperationDto
{
    /// <summary>
    /// Product ID to perform stock operation on
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity to add or remove from stock
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Reason for the stock operation (e.g., "Purchase", "Sale", "Return", "Adjustment")
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Additional notes about the stock operation
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// DTO for product rating operations
/// </summary>
public class ProductRatingDto
{
    /// <summary>
    /// Product ID to rate
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Rating value (1-5 stars)
    /// </summary>
    public decimal Rating { get; set; }

    /// <summary>
    /// Review comment (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// User ID who submitted the rating
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Enumeration for product status values (shared with domain)
/// </summary>
public enum ProductStatus
{
    /// <summary>
    /// Product is in stock and available for purchase
    /// </summary>
    InStock = 1,

    /// <summary>
    /// Product stock is low (at or below minimum level)
    /// </summary>
    LowStock = 2,

    /// <summary>
    /// Product is out of stock
    /// </summary>
    OutOfStock = 3,

    /// <summary>
    /// Product has been discontinued
    /// </summary>
    Discontinued = 4,

    /// <summary>
    /// Product is temporarily unavailable
    /// </summary>
    TemporarilyUnavailable = 5,

    /// <summary>
    /// Product is on pre-order
    /// </summary>
    PreOrder = 6
} 