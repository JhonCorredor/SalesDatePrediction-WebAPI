using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace SalesDatePrediction.Tests
{
    public class ShipperTests
    {
        private readonly Mock<IShipperData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ShipperBusiness _shipperBusiness;

        public ShipperTests()
        {
            _dataMock = new Mock<IShipperData>();
            _mapperMock = new Mock<IMapper>();
            _shipperBusiness = new ShipperBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Save()
        {
            // Arrange
            var dto = new ShipperDTO
            {
                ShipperId = 1,
                CompanyName = "Shipper Inc.",
                Phone = "123-456-7890"
            };
            var entity = new Shipper
            {
                ShipperId = 1,
                CompanyName = "Shipper Inc.",
                Phone = "123-456-7890"
            };
            _mapperMock.Setup(m => m.Map<Shipper>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<ShipperDTO>(entity)).Returns(dto);

            // Act
            var result = await _shipperBusiness.Save(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ShipperId);
            Assert.Equal("Shipper Inc.", result.CompanyName);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var dto = new ShipperDTO
            {
                ShipperId = 1,
                CompanyName = "Shipper Inc.",
                Phone = "123-456-7890"
            };
            var entity = new Shipper
            {
                ShipperId = 1,
                CompanyName = "Shipper Inc.",
                Phone = "123-456-7890"
            };
            _mapperMock.Setup(m => m.Map<Shipper>(dto)).Returns(entity);

            // Act
            await _shipperBusiness.Update(dto);

            // Assert
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }

        [Fact]
        public async Task GetDataTable()
        {
            // Arrange
        
    
            var dtos = new List<ShipperDTO>
            {
                new ShipperDTO
                {
                    ShipperId = 1,
                    CompanyName = "Shipper Inc.",
                    Phone = "123-456-7890"
                }
            };
            _dataMock.Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>())).ReturnsAsync(dtos);


            // Act
            var result = await _shipperBusiness.GetDataTable(new QueryFilterDto());

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].ShipperId);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var entity = new Shipper
            {
                ShipperId = 1,
                CompanyName = "Shipper Inc.",
                Phone = "123-456-7890"
            };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<ShipperDTO>(entity)).Returns(new ShipperDTO
            {
                ShipperId = 1,
                CompanyName = "Shipper Inc.",
                Phone = "123-456-7890"
            });

            // Act
            var result = await _shipperBusiness.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ShipperId);
            Assert.Equal("Shipper Inc.", result.CompanyName);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            _dataMock.Setup(d => d.Delete(1)).ReturnsAsync(1);

            // Act
            var result = await _shipperBusiness.Delete(1);

            // Assert
            Assert.Equal(1, result);
        }
    }
}
