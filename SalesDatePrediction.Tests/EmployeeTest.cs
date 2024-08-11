using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace SalesDatePrediction.Tests
{
    public class EmployeeTests
    {
        private readonly Mock<IEmployeeData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly EmployeeBusiness _employeeBusiness;

        public EmployeeTests()
        {
            // Crear mocks
            _dataMock = new Mock<IEmployeeData>();
            _mapperMock = new Mock<IMapper>();

            // Crear instancia de la clase que se va a probar
            _employeeBusiness = new EmployeeBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetById()
        {
            // Configurar el mock para que devuelva una entidad
            var entity = new Employee { EmpId = 1, FirstName = "John", LastName = "Doe", Title = "Manager", BirthDate = new DateTime(1980, 1, 1), HireDate = new DateTime(2010, 5, 15), City = "City A", Country = "Country A" };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<EmployeeDTO>(entity)).Returns(new EmployeeDTO { EmpId = 1, FirstName = "John", LastName = "Doe", Title = "Manager", BirthDate = new DateTime(1980, 1, 1), HireDate = new DateTime(2010, 5, 15), City = "City A", Country = "Country A" });

            // Actuar
            var result = await _employeeBusiness.GetById(1);

            // Verificar
            Assert.NotNull(result);
            Assert.Equal(1, result.EmpId);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("John Doe", result.FullName);
        }

        [Fact]
        public async Task Save()
        {
            // Configurar el mock para que mapee el dto a la entidad y viceversa
            var dto = new EmployeeDTO { EmpId = 1, FirstName = "Jane", LastName = "Smith", Title = "CEO", BirthDate = new DateTime(1985, 2, 10), HireDate = new DateTime(2015, 6, 20), City = "City B", Country = "Country B" };
            var entity = new Employee { EmpId = 1, FirstName = "Jane", LastName = "Smith", Title = "CEO", BirthDate = new DateTime(1985, 2, 10), HireDate = new DateTime(2015, 6, 20), City = "City B", Country = "Country B" };
            _mapperMock.Setup(m => m.Map<Employee>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<EmployeeDTO>(entity)).Returns(dto);

            // Actuar
            var result = await _employeeBusiness.Save(dto);

            // Verificar
            Assert.NotNull(result);
            Assert.Equal(1, result.EmpId);
            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Smith", result.LastName);
        }

        [Fact]
        public async Task Update()
        {
            // Configurar el mock para verificar que se llama a Update
            var dto = new EmployeeDTO { EmpId = 1, FirstName = "Alice", LastName = "Brown", Title = "Director", BirthDate = new DateTime(1990, 3, 15), HireDate = new DateTime(2020, 8, 25), City = "City C", Country = "Country C" };
            var entity = new Employee { EmpId = 1, FirstName = "Alice", LastName = "Brown", Title = "Director", BirthDate = new DateTime(1990, 3, 15), HireDate = new DateTime(2020, 8, 25), City = "City C", Country = "Country C" };
            _mapperMock.Setup(m => m.Map<Employee>(dto)).Returns(entity);

            // Actuar
            await _employeeBusiness.Update(dto);

            // Verificar que se llamó a la actualización
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }

        [Fact]
        public async Task Delete()
        {
            // Configurar el mock para que devuelva el número de filas afectadas
            _dataMock.Setup(d => d.Delete(1)).ReturnsAsync(1);

            // Actuar
            var result = await _employeeBusiness.Delete(1);

            // Verificar
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetDataTable()
        {
            // Configurar el mock para que devuelva una lista de Dto
            var dtos = new List<EmployeeDTO>
            {
                new EmployeeDTO { EmpId = 1, FirstName = "Bob", LastName = "White", Title = "Engineer", BirthDate = new DateTime(1975, 7, 25), HireDate = new DateTime(2000, 1, 10), City = "City D", Country = "Country D" }
            };
            _dataMock.Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>())).ReturnsAsync(dtos);

            // Actuar
            var result = await _employeeBusiness.GetDataTable(new QueryFilterDto());

            // Verificar
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Bob", result[0].FirstName);
            Assert.Equal("White", result[0].LastName);
            Assert.Equal("Engineer", result[0].Title);
        }
    }
}
