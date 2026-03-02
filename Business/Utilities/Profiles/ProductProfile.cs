using AutoMapper;
using Entities;
using Entities.DTOs.Products;

namespace Business.Utilities.Profiles;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<ProductGetDto,Product>();
        CreateMap<ProductCreateDto,Product>();
        CreateMap<Product,ProductGetDto>();
        CreateMap<ProductUpdateDto,Product>();
    }
}
