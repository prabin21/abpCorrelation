using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace abpCorrelation.Domain.Products;

public class Order : AuditedAggregateRoot<Guid>
{
    public string OrderNumber { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    // Add more properties as needed
} 