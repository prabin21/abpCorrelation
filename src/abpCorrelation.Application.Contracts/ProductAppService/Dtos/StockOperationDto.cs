using System;

namespace abpCorrelation.Application.Contracts.ProductAppService.Dtos;

public class StockOperationDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
} 