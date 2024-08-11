using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace SalesDatePrediction.Tests
{
    public class CustomerTests
    {
        private readonly Mock<ICustomerData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CustomerBusiness _customerBusiness;

        public CustomerTests()
        {
            // Crear mocks
            _dataMock = new Mock<ICustomerData>();
            _mapperMock = new Mock<IMapper>();

            // Crear instancia de la clase que se va a probar
            _customerBusiness = new CustomerBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetById()
        {
            // Configurar el mock para que devuelva una entidad
            var entity = new Customer { CustId = 1, CompanyName = "Company A", ContactName = "John Doe", ContactTitle = "Manager", Address = "123 Street", City = "City A", Region = "Region A", PostalCode = "12345", Country = "Country A", Phone = "123456789", Fax = "987654321" };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CustomerDTO>(entity)).Returns(new CustomerDTO { CustId = 1, CompanyName = "Company A", ContactName = "John Doe", ContactTitle = "Manager", Address = "123 Street", City = "City A", Region = "Region A", PostalCode = "12345", Country = "Country A", Phone = "123456789", Fax = "987654321" });

            // Actuar
            var result = await _customerBusiness.GetById(1);

            // Verificar
            Assert.NotNull(result);
            Assert.Equal(1, result.CustId);
            Assert.Equal("Company A", result.CompanyName);
            Assert.Equal("John Doe", result.ContactName);
        }

        [Fact]
        public async Task Save()
        {
            // Configurar el mock para que mapee el dto a la entidad y viceversa
            var dto = new CustomerDTO { CustId = 1, CompanyName = "Company B", ContactName = "Jane Smith", ContactTitle = "CEO", Address = "456 Avenue", City = "City B", Region = "Region B", PostalCode = "67890", Country = "Country B", Phone = "987654321", Fax = "123456789" };
            var entity = new Customer { CustId = 1, CompanyName = "Company B", ContactName = "Jane Smith", ContactTitle = "CEO", Address = "456 Avenue", City = "City B", Region = "Region B", PostalCode = "67890", Country = "Country B", Phone = "987654321", Fax = "123456789" };
            _mapperMock.Setup(m => m.Map<Customer>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CustomerDTO>(entity)).Returns(dto);

            // Actuar
            var result = await _customerBusiness.Save(dto);

            // Verificar
            Assert.NotNull(result);
            Assert.Equal(1, result.CustId);
            Assert.Equal("Company B", result.CompanyName);
        }

        [Fact]
        public async Task Update()
        {
            // Configurar el mock para verificar que se llama a Update
            var dto = new CustomerDTO { CustId = 1, CompanyName = "Company C", ContactName = "Alice Brown", ContactTitle = "Director", Address = "789 Boulevard", City = "City C", Region = "Region C", PostalCode = "11223", Country = "Country C", Phone = "111222333", Fax = "333222111" };
            var entity = new Customer { CustId = 1, CompanyName = "Company C", ContactName = "Alice Brown", ContactTitle = "Director", Address = "789 Boulevard", City = "City C", Region = "Region C", PostalCode = "11223", Country = "Country C", Phone = "111222333", Fax = "333222111" };
            _mapperMock.Setup(m => m.Map<Customer>(dto)).Returns(entity);

            // Actuar
            await _customerBusiness.Update(dto);

            // Verificar que se llamó a la actualización
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }

        [Fact]
        public async Task Delete()
        {
            // Configurar el mock para que devuelva el número de filas afectadas
            _dataMock.Setup(d => d.Delete(1)).ReturnsAsync(1);

            // Actuar
            var result = await _customerBusiness.Delete(1);

            // Verificar
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetDataTable()
        {
            // Configurar el mock para que devuelva una lista de Dto
            var dtos = new List<CustomerDTO>
            {
                new CustomerDTO { CustId = 1, CompanyName = "Company D", ContactName = "Bob White", ContactTitle = "Manager", Address = "1234 Road", City = "City D", Region = "Region D", PostalCode = "44556", Country = "Country D", Phone = "555666777", Fax = "777666555" }
            };
            _dataMock.Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>())).ReturnsAsync(dtos);

            // Actuar
            var result = await _customerBusiness.GetDataTable(new QueryFilterDto());

            // Verificar
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Company D", result[0].CompanyName);
        }
    }
}
