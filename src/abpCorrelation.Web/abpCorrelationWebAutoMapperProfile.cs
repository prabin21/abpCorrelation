using AutoMapper;
using abpCorrelation.Domain.Products;
using abpCorrelation.Application.Contracts.ProductAppService.Dtos;
using abpCorrelation.Domain.Correlation;
using abpCorrelation.Application.Contracts.Correlation;

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
    }
}
