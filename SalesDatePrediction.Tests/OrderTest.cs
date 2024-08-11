using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace SalesDatePrediction.Tests
{
    public class OrderTests
    {
        private readonly Mock<IOrderData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderBusiness _orderBusiness;

        public OrderTests()
        {
            _dataMock = new Mock<IOrderData>();
            _mapperMock = new Mock<IMapper>();
            _orderBusiness = new OrderBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Save()
        {
            // Arrange
            var dto = new OrderDTO
            {
                OrderId = 1,
                OrderDate = new DateTime(2023, 1, 1),
                RequiredDate = new DateTime(2023, 1, 10),
                ShippedDate = null,
                ShipName = "ShipName",
                ShipAddress = "ShipAddress",
                ShipCity = "ShipCity",
                ShipCountry = "ShipCountry"
            };
            var entity = new Order
            {
                OrderId = 1,
                OrderDate = new DateTime(2023, 1, 1),
                RequiredDate = new DateTime(2023, 1, 10),
                ShippedDate = null,
                ShipName = "ShipName",
                ShipAddress = "ShipAddress",
                ShipCity = "ShipCity",
                ShipCountry = "ShipCountry"
            };
            _mapperMock.Setup(m => m.Map<Order>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<OrderDTO>(entity)).Returns(dto);

            // Act
            var result = await _orderBusiness.Save(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var dto = new OrderDTO
            {
                OrderId = 1,
                OrderDate = new DateTime(2023, 1, 1),
                RequiredDate = new DateTime(2023, 1, 10),
                ShippedDate = null,
                ShipName = "ShipName",
                ShipAddress = "ShipAddress",
                ShipCity = "ShipCity",
                ShipCountry = "ShipCountry"
            };
            var entity = new Order
            {
                OrderId = 1,
                OrderDate = new DateTime(2023, 1, 1),
                RequiredDate = new DateTime(2023, 1, 10),
                ShippedDate = null,
                ShipName = "ShipName",
                ShipAddress = "ShipAddress",
                ShipCity = "ShipCity",
                ShipCountry = "ShipCountry"
            };
            _mapperMock.Setup(m => m.Map<Order>(dto)).Returns(entity);

            // Act
            await _orderBusiness.Update(dto);

            // Assert
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }

        [Fact]
        public async Task GetDataTable()
        {
            // Arrange
            var dtos = new List<OrderDTO>
            {
                new OrderDTO
                {
                    OrderId = 1,
                    OrderDate = new DateTime(2023, 1, 1),
                    RequiredDate = new DateTime(2023, 1, 10),
                    ShippedDate = null,
                    ShipName = "ShipName",
                    ShipAddress = "ShipAddress",
                    ShipCity = "ShipCity",
                    ShipCountry = "ShipCountry"
                }
            };
            _dataMock.Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>())).ReturnsAsync(dtos);

            // Act
            var result = await _orderBusiness.GetDataTable(new QueryFilterDto());

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].OrderId);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var entity = new Order
            {
                OrderId = 1,
                OrderDate = new DateTime(2023, 1, 1),
                RequiredDate = new DateTime(2023, 1, 10),
                ShippedDate = null,
                ShipName = "ShipName",
                ShipAddress = "ShipAddress",
                ShipCity = "ShipCity",
                ShipCountry = "ShipCountry"
            };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<OrderDTO>(entity)).Returns(new OrderDTO
            {
                OrderId = 1,
                OrderDate = new DateTime(2023, 1, 1),
                RequiredDate = new DateTime(2023, 1, 10),
                ShippedDate = null,
                ShipName = "ShipName",
                ShipAddress = "ShipAddress",
                ShipCity = "ShipCity",
                ShipCountry = "ShipCountry"
            });

            // Act
            var result = await _orderBusiness.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            _dataMock.Setup(d => d.Delete(1)).ReturnsAsync(1);

            // Act
            var result = await _orderBusiness.Delete(1);

            // Assert
        }
    }
}
