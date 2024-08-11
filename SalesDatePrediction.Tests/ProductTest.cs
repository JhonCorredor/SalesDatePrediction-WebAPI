using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace SalesDatePrediction.Tests
{
    public class ProductTests
    {
        private readonly Mock<IProductData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductBusiness _productBusiness;

        public ProductTests()
        {
            _dataMock = new Mock<IProductData>();
            _mapperMock = new Mock<IMapper>();
            _productBusiness = new ProductBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Save()
        {
            // Arrange
            var dto = new ProductDTO
            {
                ProductId = 1,
                ProductName = "Product A",
                UnitPrice = 10.5m,
                Discontinued = false
            };
            var entity = new Product
            {
                ProductId = 1,
                ProductName = "Product A",
                UnitPrice = 10.5m,
                Discontinued = false
            };
            _mapperMock.Setup(m => m.Map<Product>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<ProductDTO>(entity)).Returns(dto);

            // Act
            var result = await _productBusiness.Save(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal("Product A", result.ProductName);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var dto = new ProductDTO
            {
                ProductId = 1,
                ProductName = "Product A",
                UnitPrice = 10.5m,
                Discontinued = false
            };
            var entity = new Product
            {
                ProductId = 1,
                ProductName = "Product A",
                UnitPrice = 10.5m,
                Discontinued = false
            };
            _mapperMock.Setup(m => m.Map<Product>(dto)).Returns(entity);

            // Act
            await _productBusiness.Update(dto);

            // Assert
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }

        [Fact]
        public async Task GetDataTable()
        {
            // Arrange
            var dtos = new List<ProductDTO>
            {
                new ProductDTO
                {
                    ProductId = 1,
                    ProductName = "Product A",
                    UnitPrice = 10.5m,
                    Discontinued = false
                }
            };
                _dataMock.Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>())).ReturnsAsync(dtos);

            // Act
            var result = await _productBusiness.GetDataTable(new QueryFilterDto());

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].ProductId);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var entity = new Product
            {
                ProductId = 1,
                ProductName = "Product A",
                UnitPrice = 10.5m,
                Discontinued = false
            };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<ProductDTO>(entity)).Returns(new ProductDTO
            {
                ProductId = 1,
                ProductName = "Product A",
                UnitPrice = 10.5m,
                Discontinued = false
            });

            // Act
            var result = await _productBusiness.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal("Product A", result.ProductName);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            _dataMock.Setup(d => d.Delete(1)).ReturnsAsync(1);

            // Act
            var result = await _productBusiness.Delete(1);

            // Assert
            Assert.Equal(1, result);
        }
    }
}
