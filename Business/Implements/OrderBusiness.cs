using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class OrderBusiness : IOrderBusiness
{
    private readonly IOrderData _data;
    private readonly IMapper _mapper;

    public OrderBusiness(IOrderData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<OrderDTO> Save(OrderDTO dto)
    {
        Order entity = _mapper.Map<Order>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<OrderDTO>(entity);
    }

    public async Task Update(OrderDTO dto)
    {
        Order entity = _mapper.Map<Order>(dto);
        await _data.Update(entity);
    }

    public async Task<List<OrderDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<OrderDTO> orders = await _data.GetDataTable(filters);
        return orders.ToList();
    }

    public async Task<OrderDTO> GetById(int id)
    {
        Order entity = await _data.GetById(id);
        return _mapper.Map<OrderDTO>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await _data.Delete(id);
    }
}
