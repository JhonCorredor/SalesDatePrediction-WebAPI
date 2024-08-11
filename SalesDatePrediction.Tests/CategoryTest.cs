using AutoMapper;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Moq;

namespace SalesDatePrediction.Tests
{
    public class CategoryTests
    {
        private readonly Mock<ICategoryData> _dataMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CategoryBusiness _categoryBusiness;

        public CategoryTests()
        {
            // Crear mocks
            _dataMock = new Mock<ICategoryData>();
            _mapperMock = new Mock<IMapper>();

            // Crear instancia de la clase que se va a probar
            _categoryBusiness = new CategoryBusiness(_dataMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetById()
        {
            // Configurar el mock para que devuelva una entidad
            var entity = new Category { CategoryId = 1, CategoryName = "Electronics", Description = "Electronic items" };
            _dataMock.Setup(d => d.GetById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CategoryDTO>(entity)).Returns(new CategoryDTO { CategoryId = 1, CategoryName = "Electronics", Description = "Electronic items" });

            // Actuar
            var result = await _categoryBusiness.GetById(1);

            // Verificar
            Assert.NotNull(result);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal("Electronics", result.CategoryName);
        }

        [Fact]
        public async Task Save()
        {
            // Configurar el mock para que mapee el dto a la entidad y viceversa
            var dto = new CategoryDTO { CategoryId = 1, CategoryName = "Books", Description = "All kinds of books" };
            var entity = new Category { CategoryId = 1, CategoryName = "Books", Description = "All kinds of books" };
            _mapperMock.Setup(m => m.Map<Category>(dto)).Returns(entity);
            _dataMock.Setup(d => d.Save(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CategoryDTO>(entity)).Returns(dto);

            // Actuar
            var result = await _categoryBusiness.Save(dto);

            // Verificar
            Assert.NotNull(result);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal("Books", result.CategoryName);
        }

        [Fact]
        public async Task Update()
        {
            // Configurar el mock para verificar que se llama a Update
            var dto = new CategoryDTO { CategoryId = 1, CategoryName = "Clothing", Description = "Men and Women Clothing" };
            var entity = new Category { CategoryId = 1, CategoryName = "Clothing", Description = "Men and Women Clothing" };
            _mapperMock.Setup(m => m.Map<Category>(dto)).Returns(entity);

            // Actuar
            await _categoryBusiness.Update(dto);

            // Verificar que se llamó a la actualización
            _dataMock.Verify(d => d.Update(entity), Times.Once);
        }

        [Fact]
        public async Task Delete()
        {
            // Configurar el mock para que devuelva el número de filas afectadas
            _dataMock.Setup(d => d.Delete(1)).ReturnsAsync(1);

            // Actuar
            var result = await _categoryBusiness.Delete(1);

            // Verificar
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetDataTable()
        {
            // Configurar el mock para que devuelva una lista de Dto
            var dtos = new List<CategoryDTO>
            {
                new CategoryDTO { CategoryId = 1, CategoryName = "Toys", Description = "Children's toys" }
            };
            _dataMock.Setup(d => d.GetDataTable(It.IsAny<QueryFilterDto>())).ReturnsAsync(dtos);

            // Actuar
            var result = await _categoryBusiness.GetDataTable(new QueryFilterDto());

            // Verificar
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Toys", result[0].CategoryName);
        }
    }
}
