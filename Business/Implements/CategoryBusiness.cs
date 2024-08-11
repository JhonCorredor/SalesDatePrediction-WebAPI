using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class CategoryBusiness : ICategoryBusiness
{
    private readonly ICategoryData _data;
    private readonly IMapper _mapper;

    public CategoryBusiness(ICategoryData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<CategoryDTO> Save(CategoryDTO dto)
    {
        Category entity = _mapper.Map<Category>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<CategoryDTO>(entity);
    }

    public async Task Update(CategoryDTO dto)
    {
        Category entity = _mapper.Map<Category>(dto);
        await _data.Update(entity);
    }

    public async Task<List<CategoryDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<CategoryDTO> categories = await _data.GetDataTable(filters);
        return categories.ToList();
    }

    public async Task<CategoryDTO> GetById(int id)
    {
        Category entity = await _data.GetById(id);
        return _mapper.Map<CategoryDTO>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await _data.Delete(id);
    }
}
