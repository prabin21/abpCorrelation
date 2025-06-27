using AutoMapper;
using abpCorrelation.Domain.Products;
using abpCorrelation.Application.Contracts.ProductAppService.Dtos;
using abpCorrelation.Domain.Correlation;
using abpCorrelation.Application.Contracts.Correlation;
using abpCorrelation.Application.Contracts.ProductAppService.Orders;

namespace abpCorrelation.Web;

public class abpCorrelationWebAutoMapperProfile : Profile
{
    public abpCorrelationWebAutoMapperProfile()
    {
        // Product mappings
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ProductDto>();

        // CorrelationLog mappings
        CreateMap<CreateCorrelationLogDto, CorrelationLog>();
        CreateMap<UpdateCorrelationLogDto, CorrelationLog>();
        CreateMap<CorrelationLog, CorrelationLogDto>();

        // Order mappings
        CreateMap<CreateOrderDto, Order>();
        CreateMap<Order, OrderDto>();
    }
}
