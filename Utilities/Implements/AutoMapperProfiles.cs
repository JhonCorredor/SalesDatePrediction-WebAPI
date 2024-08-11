using AutoMapper;
using Entity.Dto;
using Entity.Model;
using Utilities.Interfaces;

namespace Utilities.Implements
{
    public class AutoMapperProfiles : Profile
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public AutoMapperProfiles(IJwtAuthenticationService jwtAuthenticationService) : base()
        {
            _jwtAuthenticationService = jwtAuthenticationService;
            
            //Mappers

            CreateMap<EmployeeDTO, Employee>().ReverseMap();          
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<SupplierDTO, Supplier>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();
            CreateMap<ShipperDTO, Shipper>().ReverseMap();
        }
    }
}
