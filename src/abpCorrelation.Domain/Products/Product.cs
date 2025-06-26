using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace abpCorrelation.Domain.Products;

/// <summary>
/// Product entity for managing product information with correlation tracking
/// </summary>
public class Product : AuditedAggregateRoot<Guid>, IMultiTenant
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
} 