using System;
using Volo.Abp.Application.Dtos;

namespace abpCorrelation.Application.Contracts.ProductAppService.Orders;

public class OrderDto : AuditedEntityDto<Guid>
{
    public string OrderNumber { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
} 