using System;

namespace abpCorrelation.Application.Contracts.ProductAppService.Dtos;

/// <summary>
/// DTO for creating a product
/// </summary>
public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public decimal? Weight { get; set; }
    public string? Dimensions { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public string? Material { get; set; }
    public string? Images { get; set; }
    public string? Tags { get; set; }
    public string? Specifications { get; set; }
    public string? Warranty { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockLevel { get; set; }
    public int MaxStockLevel { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime? LaunchDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Barcode { get; set; }
    public string? Manufacturer { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? ShippingClass { get; set; }
    public string? TaxClass { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? SeoKeywords { get; set; }
    public string? MetaData { get; set; }
} 