using Core.Utilities.Results;
using Entities.DTOs.Products;

namespace Business.Services.Interfaces;

public interface IProductService
{
    Task<IDataResult<List<ProductGetDto>>> GetAllAsync();
    Task<IDataResult<ProductGetDto>> GetByIdAsync(int id);
    Task<IDataResult<ProductGetDto>> GetByNameAsync(string name);
    Task<IResult> AddAsync(ProductCreateDto productCreateDto);
    Task<IResult> UpdateAsync(ProductUpdateDto productCreateDto);
    Task<IResult> DeleteAsync(int id);
}
