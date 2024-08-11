using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace  SalesDatePrediction.Tests
{
    public class SupplierBusinessTests
    {
        private readonly Mock<ISupplierData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SupplierBusiness _supplierBusiness;

        public SupplierBusinessTests()
        {
            _dataMock = new Mock<ISupplierData>();
            _mapperMock = new Mock<IMapper>();
            _supplierBusiness = new SupplierBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Save()
        {
            // Arrange
            var dto = new SupplierDTO
            {
                SupplierId = 1,
                CompanyName = "Supplier Inc.",
                ContactName = "John Doe",
                City = "Metropolis",
                Country = "Countryland"
            };
            var entity = new Supplier
            {
                SupplierId = 1,
                CompanyName = "Supplier Inc.",
                ContactName = "John Doe",
                City = "Metropolis",
                Country = "Countryland"
            };
            _mapperMock.Setup(m => m.Map<Supplier>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<SupplierDTO>(entity)).Returns(dto);

            // Act
            var result = await _supplierBusiness.Save(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.SupplierId);
            Assert.Equal("Supplier Inc.", result.CompanyName);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var dto = new SupplierDTO
            {
                SupplierId = 1,
                CompanyName = "Supplier Inc.",
                ContactName = "John Doe",
                City = "Metropolis",
                Country = "Countryland"
            };
            var entity = new Supplier
            {
                SupplierId = 1,
                CompanyName = "Supplier Inc.",
                ContactName = "John Doe",
                City = "Metropolis",
                Country = "Countryland"
            };
            _mapperMock.Setup(m => m.Map<Supplier>(dto)).Returns(entity);

            // Act
            await _supplierBusiness.Update(dto);

            // Assert
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }
        [Fact]
        public async Task GetDataTable()
        {
            // Arrange
            var suppliers = new List<SupplierDTO>
            {
                new SupplierDTO
                {
                    SupplierId = 1,
                    CompanyName = "Supplier Inc.",
                    ContactName = "John Doe",
                    City = "Metropolis",
                    Country = "Countryland"
                }
            };

            // Configura el mock para devolver una lista de SupplierDTO
            _dataMock
                .Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>()))
                .ReturnsAsync(suppliers);

            // Act
            var result = await _supplierBusiness.GetDataTable(new QueryFilterDto());

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.First().SupplierId);
            Assert.Equal("Supplier Inc.", result.First().CompanyName);
        }
    


    [Fact]
        public async Task GetById()
        {
            // Arrange
            var entity = new Supplier
            {
                SupplierId = 1,
                CompanyName = "Supplier Inc.",
                ContactName = "John Doe",
                City = "Metropolis",
                Country = "Countryland"
            };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<SupplierDTO>(entity)).Returns(new SupplierDTO
            {
                SupplierId = 1,
                CompanyName = "Supplier Inc.",
                ContactName = "John Doe",
                City = "Metropolis",
                Country = "Countryland"
            });

            // Act
            var result = await _supplierBusiness.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.SupplierId);
            Assert.Equal("Supplier Inc.", result.CompanyName);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            _dataMock.Setup(d => d.Delete(1)).ReturnsAsync(1);

            // Act
            var result = await _supplierBusiness.Delete(1);

            // Assert
            Assert.Equal(1, result);
        }
    }
}
