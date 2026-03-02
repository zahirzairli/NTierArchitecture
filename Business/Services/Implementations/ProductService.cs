using AutoMapper;
using Business.Services.Interfaces;
using Business.Utilities.Constants;
using Core.Utilities.Exceptions;
using Core.Utilities.Results;
using DataAccess.Repositories.Interfaces;
using Entities;
using Entities.DTOs.Products;

namespace Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IResult> AddAsync(ProductCreateDto productCreateDto)
    {
        if (await _productRepository.ExistAsync(p => p.Name == productCreateDto.Name)) throw new AlreadyIsExistException(ExceptionMessages.ProductAlreadyExist);

        Product newProduct = _mapper.Map<Product>(productCreateDto);
        newProduct.Created = DateTime.UtcNow;
        await _productRepository.AddAsync(newProduct);
       int result = await _productRepository.SaveAsync();
        return result == 0 ? new ErrorResult("Product was not added!") : new SuccessResult("Product added successfully!");
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        Product product = await _productRepository.GetAsync(p => p.Id == id);
        if (product == null) throw new NotFoundException(ExceptionMessages.ProductDoesnotExist);
        _productRepository.Delete(product);
        int result = await _productRepository.SaveAsync();
        return result == 0 ? new ErrorResult("Product was not deleted!") : new SuccessResult("Product deleted successfully!");
    }

    public async Task<IDataResult<List<ProductGetDto>>> GetAllAsync()
    {
        List<ProductGetDto> products = _mapper.Map<List<ProductGetDto>>(await _productRepository.GetAllAsync());
        if (products.Count == 0) return new ErrorDataResult<List<ProductGetDto>>("No products found!");

        return new SuccessDataResult<List<ProductGetDto>>(products,"The list of products");
    }

    public async Task<IDataResult<ProductGetDto>> GetByIdAsync(int id)
    {
        Product product = await _productRepository.GetAsync(p => p.Id == id);

        if (product == null) return new ErrorDataResult<ProductGetDto>("Product not found!");
        return new SuccessDataResult<ProductGetDto>(_mapper.Map<ProductGetDto>(product), "Product found!");
    }

    public async Task<IDataResult<ProductGetDto>> GetByNameAsync(string name)
    {
        Product product = await _productRepository.GetAsync(p => p.Name == name);

        if (product == null) return new ErrorDataResult<ProductGetDto>("Product was not found!");
        return new SuccessDataResult<ProductGetDto>(_mapper.Map<ProductGetDto>(product), "Product found!");
    }
     
    public async Task<IResult> UpdateAsync(ProductUpdateDto productCreateDto)
    {
        if (!await _productRepository.ExistAsync(p => p.Id == productCreateDto.Id)) throw new NotFoundException(ExceptionMessages.ProductDoesnotExist);
        _productRepository.Update(_mapper.Map<Product>(productCreateDto));
       int result = await _productRepository.SaveAsync();
        return result == 0 ? new ErrorResult("Product was not updated successfullly!") : new SuccessResult("Product updated successfully!");
    }
}
