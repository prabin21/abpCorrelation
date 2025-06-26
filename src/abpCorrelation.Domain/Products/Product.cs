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

    protected Product()
    {
        // Required for Entity Framework
    }

    /// <summary>
    /// Creates a new product with basic required information
    /// </summary>
    /// <param name="id">Unique identifier for the product</param>
    /// <param name="name">Product name</param>
    /// <param name="sku">Product SKU</param>
    /// <param name="price">Product price</param>
    /// <param name="cost">Product cost</param>
    public Product(Guid id, string name, string sku, decimal price, decimal cost)
    {
        Id = id;
        Name = name;
        Sku = sku;
        Price = price;
        Cost = cost;
        Status = ProductStatus.InStock;
        IsActive = true;
    }

    /// <summary>
    /// Updates product basic information
    /// </summary>
    /// <param name="name">New product name</param>
    /// <param name="description">New product description</param>
    /// <param name="category">New product category</param>
    /// <param name="brand">New product brand</param>
    public void UpdateBasicInfo(string name, string? description, string? category, string? brand)
    {
        Name = name;
        Description = description;
        Category = category;
        Brand = brand;
    }

    /// <summary>
    /// Updates product pricing information
    /// </summary>
    /// <param name="price">New selling price</param>
    /// <param name="cost">New cost price</param>
    public void UpdatePricing(decimal price, decimal cost)
    {
        Price = price;
        Cost = cost;
    }

    /// <summary>
    /// Updates product stock information
    /// </summary>
    /// <param name="stockQuantity">New stock quantity</param>
    /// <param name="minStockLevel">New minimum stock level</param>
    /// <param name="maxStockLevel">New maximum stock level</param>
    public void UpdateStockInfo(int stockQuantity, int minStockLevel, int maxStockLevel)
    {
        StockQuantity = stockQuantity;
        MinStockLevel = minStockLevel;
        MaxStockLevel = maxStockLevel;

        // Update status based on stock level
        Status = stockQuantity switch
        {
            0 => ProductStatus.OutOfStock,
            var q when q <= minStockLevel => ProductStatus.LowStock,
            _ => ProductStatus.InStock
        };
    }

    /// <summary>
    /// Adds stock to the product inventory
    /// </summary>
    /// <param name="quantity">Quantity to add</param>
    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        StockQuantity += quantity;
        
        // Update status if stock was previously out
        if (Status == ProductStatus.OutOfStock && StockQuantity > 0)
        {
            Status = StockQuantity <= MinStockLevel ? ProductStatus.LowStock : ProductStatus.InStock;
        }
    }

    /// <summary>
    /// Removes stock from the product inventory
    /// </summary>
    /// <param name="quantity">Quantity to remove</param>
    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (StockQuantity < quantity)
            throw new InvalidOperationException("Insufficient stock available");

        StockQuantity -= quantity;

        // Update status based on remaining stock
        Status = StockQuantity switch
        {
            0 => ProductStatus.OutOfStock,
            var q when q <= MinStockLevel => ProductStatus.LowStock,
            _ => ProductStatus.InStock
        };
    }

    /// <summary>
    /// Updates product rating based on new review
    /// </summary>
    /// <param name="newRating">New rating value (1-5)</param>
    public void UpdateRating(decimal newRating)
    {
        if (newRating < 1 || newRating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(newRating));

        // Calculate new average rating
        var totalRating = Rating * ReviewCount + newRating;
        ReviewCount++;
        Rating = totalRating / ReviewCount;
    }

    /// <summary>
    /// Marks product as featured or not featured
    /// </summary>
    /// <param name="isFeatured">Whether the product should be featured</param>
    public void SetFeatured(bool isFeatured)
    {
        IsFeatured = isFeatured;
    }

    /// <summary>
    /// Activates or deactivates the product (soft delete)
    /// </summary>
    /// <param name="isActive">Whether the product should be active</param>
    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    /// <summary>
    /// Calculates the profit margin for the product
    /// </summary>
    /// <returns>Profit margin percentage</returns>
    public decimal CalculateProfitMargin()
    {
        if (Price <= 0) return 0;
        return ((Price - Cost) / Price) * 100;
    }

    /// <summary>
    /// Checks if the product is in low stock
    /// </summary>
    /// <returns>True if stock is at or below minimum level</returns>
    public bool IsLowStock()
    {
        return StockQuantity <= MinStockLevel;
    }

    /// <summary>
    /// Checks if the product is out of stock
    /// </summary>
    /// <returns>True if stock quantity is zero</returns>
    public bool IsOutOfStock()
    {
        return StockQuantity == 0;
    }
}

/// <summary>
/// Enumeration for product status values
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