using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class ShipperBusiness : IShipperBusiness
{
    private readonly IShipperData _data;
    private readonly IMapper _mapper;

    public ShipperBusiness(IShipperData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<ShipperDTO> Save(ShipperDTO dto)
    {
        Shipper entity = _mapper.Map<Shipper>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<ShipperDTO>(entity);
    }

    public async Task Update(ShipperDTO dto)
    {
        Shipper entity = _mapper.Map<Shipper>(dto);
        await _data.Update(entity);
    }

    public async Task<List<ShipperDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<ShipperDTO> shippers = await _data.GetDataTable(filters);
        return shippers.ToList();
    }

    public async Task<ShipperDTO> GetById(int id)
    {
        Shipper entity = await _data.GetById(id);
        return _mapper.Map<ShipperDTO>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await _data.Delete(id);
    }
}
