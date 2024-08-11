using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class OrderDetailBusiness : IOrderDetailBusiness
{
    private readonly IOrderDetailData _data;
    private readonly IMapper _mapper;

    public OrderDetailBusiness(IOrderDetailData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<OrderDetailDTO> Save(OrderDetailDTO dto)
    {
        OrderDetail entity = _mapper.Map<OrderDetail>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<OrderDetailDTO>(entity);
    }

    public async Task Update(OrderDetailDTO dto)
    {
        OrderDetail entity = _mapper.Map<OrderDetail>(dto);
        await _data.Update(entity);
    }

    public async Task<List<OrderDetailDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<OrderDetailDTO> orderDetails = await _data.GetDataTable(filters);
        return orderDetails.ToList();
    }

    public async Task<OrderDetailDTO> GetById(int id)
    {
        OrderDetail entity = await _data.GetById(id);
        return _mapper.Map<OrderDetailDTO>(entity);
    }

    public async Task<int> Delete(int idOrden, int idProducto)
    {
        return await _data.Delete(idOrden, idProducto);
    }
}
