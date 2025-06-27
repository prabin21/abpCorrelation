using System;

namespace abpCorrelation.Application.Contracts.ProductAppService.Orders;

public class CreateOrderDto
{
    public string OrderNumber { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
} 