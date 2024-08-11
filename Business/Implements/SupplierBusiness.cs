using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class SupplierBusiness : ISupplierBusiness
{
    private readonly ISupplierData _data;
    private readonly IMapper _mapper;

    public SupplierBusiness(ISupplierData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<SupplierDTO> Save(SupplierDTO dto)
    {
        Supplier entity = _mapper.Map<Supplier>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<SupplierDTO>(entity);
    }

    public async Task Update(SupplierDTO dto)
    {
        Supplier entity = _mapper.Map<Supplier>(dto);
        await _data.Update(entity);
    }

    public async Task<List<SupplierDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<SupplierDTO> suppliers = await _data.GetDataTable(filters);
        return suppliers.ToList();
    }

    public async Task<SupplierDTO> GetById(int id)
    {
        Supplier entity = await _data.GetById(id);
        return _mapper.Map<SupplierDTO>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await _data.Delete(id);
    }
}
