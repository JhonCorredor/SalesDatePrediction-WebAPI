using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace SalesDatePrediction.Tests
{
    public class OrderDetailTests
    {
        private readonly Mock<IOrderDetailData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderDetailBusiness _orderDetailBusiness;

        public OrderDetailTests()
        {
            _dataMock = new Mock<IOrderDetailData>();
            _mapperMock = new Mock<IMapper>();
            _orderDetailBusiness = new OrderDetailBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Save()
        {
            // Arrange
            var dto = new OrderDetailDTO { OrderId = 1, ProductId = 1, UnitPrice = 10m, Qty = 2, Discount = 0m };
            var entity = new OrderDetail { OrderId = 1, ProductId = 1, UnitPrice = 10m, Qty = 2, Discount = 0m };
            _mapperMock.Setup(m => m.Map<OrderDetail>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<OrderDetailDTO>(entity)).Returns(dto);

            // Act
            var result = await _orderDetailBusiness.Save(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var dto = new OrderDetailDTO { OrderId = 1, ProductId = 1, UnitPrice = 10m, Qty = 2, Discount = 0m };
            var entity = new OrderDetail { OrderId = 1, ProductId = 1, UnitPrice = 10m, Qty = 2, Discount = 0m };
            _mapperMock.Setup(m => m.Map<OrderDetail>(dto)).Returns(entity);

            // Act
            await _orderDetailBusiness.Update(dto);

            // Assert
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }

        [Fact]
        public async Task GetDataTable()
        {
            // Arrange
            var dtos = new List<OrderDetailDTO>
            {
                new OrderDetailDTO { OrderId = 1, ProductId = 1, UnitPrice = 10m, Qty = 2, Discount = 0m }
            };
            _dataMock.Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>())).ReturnsAsync(dtos);

            // Act
            var result = await _orderDetailBusiness.GetDataTable(new QueryFilterDto());

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].OrderId);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var entity = new OrderDetail { OrderId = 1, ProductId = 1, UnitPrice = 10m, Qty = 2, Discount = 0m };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<OrderDetailDTO>(entity)).Returns(new OrderDetailDTO { OrderId = 1, ProductId = 1 });

            // Act
            var result = await _orderDetailBusiness.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            _dataMock.Setup(d => d.Delete(1, 1)).ReturnsAsync(1);

            // Act
            var result = await _orderDetailBusiness.Delete(1, 1);

            // Assert
            Assert.Equal(1, result);
        }
    }
}
