using System;
using Volo.Abp.Application.Dtos;

namespace abpCorrelation.Application.Contracts.ProductAppService.Dtos;

public class GetProductListDto : PagedAndSortedResultRequestDto
{
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public ProductStatus? Status { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsFeatured { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SearchTerm { get; set; }
    public string? Manufacturer { get; set; }
    public string? CountryOfOrigin { get; set; }
} 