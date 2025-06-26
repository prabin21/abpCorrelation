namespace abpCorrelation.Domain.Products;

/// <summary>
/// Status of a product (stock and availability)
/// </summary>
public enum ProductStatus
{
    InStock = 1,
    LowStock = 2,
    OutOfStock = 3,
    Discontinued = 4,
    TemporarilyUnavailable = 5,
    PreOrder = 6
} 