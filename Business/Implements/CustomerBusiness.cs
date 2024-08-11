using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class CustomerBusiness : ICustomerBusiness
{
    private readonly ICustomerData _data;
    private readonly IMapper _mapper;

    public CustomerBusiness(ICustomerData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<CustomerDTO> Save(CustomerDTO dto)
    {
        Customer entity = _mapper.Map<Customer>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<CustomerDTO>(entity);
    }

    public async Task Update(CustomerDTO dto)
    {
        Customer entity = _mapper.Map<Customer>(dto);
        await _data.Update(entity);
    }

    public async Task<List<CustomerDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<CustomerDTO> customers = await _data.GetDataTable(filters);
        return customers.ToList();
    }

    public async Task<List<SalesDatePredictionDto>> GetSalesDatePrediction(QueryFilterDto filters)
    {
        IEnumerable<SalesDatePredictionDto> customers = await _data.GetSalesDatePrediction(filters);
        return customers.ToList();
    }

    public async Task<CustomerDTO> GetById(int id)
    {
        Customer entity = await _data.GetById(id);
        return _mapper.Map<CustomerDTO>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await _data.Delete(id);
    }
}
