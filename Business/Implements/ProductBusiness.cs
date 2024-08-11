using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class ProductBusiness : IProductBusiness
{
    private readonly IProductData _data;
    private readonly IMapper _mapper;

    public ProductBusiness(IProductData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Save(ProductDTO dto)
    {
        Product entity = _mapper.Map<Product>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<ProductDTO>(entity);
    }

    public async Task Update(ProductDTO dto)
    {
        Product entity = _mapper.Map<Product>(dto);
        await _data.Update(entity);
    }

    public async Task<List<ProductDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<ProductDTO> products = await _data.GetDataTable(filters);
        return products.ToList();
    }

    public async Task<ProductDTO> GetById(int id)
    {
        Product entity = await _data.GetById(id);
        return _mapper.Map<ProductDTO>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await _data.Delete(id);
    }
}
