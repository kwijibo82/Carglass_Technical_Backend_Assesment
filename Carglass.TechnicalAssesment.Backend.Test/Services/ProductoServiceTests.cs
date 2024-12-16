using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carglass.TechnicalAssessment.Backend.BL.Productos;
using Carglass.TechnicalAssessment.Backend.DL.Interfaces;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;
using AutoMapper;
using Carglass.TechnicalAssessment.Backend.Dtos.Products;

public class ProductoServiceTests
{
    private readonly Mock<IProductoRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductoService _service;

    public ProductoServiceTests()
    {
        _mockRepository = new Mock<IProductoRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new ProductoService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProductos()
    {
        //Arrange
        var productos = new List<Producto> { new Producto { Id = 1 }, new Producto { Id = 2 } };
        var mappedProductos = new List<ProductoDto> { new ProductoDto { Id = 1 }, new ProductoDto { Id = 2 } };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(productos);
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductoDto>>(productos)).Returns(mappedProductos);

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProducto_WhenExists()
    {
        //Arrange
        var producto = new Producto { Id = 1 };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(producto);

        //Act
        var result = await _service.GetByIdAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        //Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Producto?)null);

        //Act
        var result = await _service.GetByIdAsync(999);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProducto_WhenDoesNotExist()
    {
        //Arrange
        var producto = new Producto { ProductName = "Product1", ProductType = 1, NumTerminal = 123 };

        _mockRepository.Setup(r => r.ExistsAsync(producto.ProductName, producto.ProductType, producto.NumTerminal)).ReturnsAsync(false);

        //Act
        await _service.AddAsync(producto);

        //Assert
        _mockRepository.Verify(r => r.AddAsync(producto), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenProductoExists()
    {
        //Arrange
        var producto = new Producto { ProductName = "Product1", ProductType = 1, NumTerminal = 123 };

        _mockRepository.Setup(r => r.ExistsAsync(producto.ProductName, producto.ProductType, producto.NumTerminal)).ReturnsAsync(true);

        //Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(producto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProducto()
    {
        //Arrange
        var producto = new Producto { Id = 1 };

        //Act
        await _service.UpdateAsync(producto);

        //Assert
        _mockRepository.Verify(r => r.UpdateAsync(producto), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProducto()
    {
        //Arrange
        var id = 1;

        //Act
        await _service.DeleteAsync(id);

        //Assert
        _mockRepository.Verify(r => r.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task GetProductsClientsAsync_ShouldReturnPagedResult()
    {
        //Arrange
        var productos = new List<Producto>
        {
            new Producto { Id = 1, ProductName = "Product1" },
            new Producto { Id = 2, ProductName = "Product2" }
        };
        var pagedResult = new PagedResultDto<Producto>
        {
            Items = productos,
            TotalCount = 2
        };
        var mappedProductos = new List<ProductoDto>
        {
            new ProductoDto { Id = 1, ProductName = "MappedProduct1" },
            new ProductoDto { Id = 2, ProductName = "MappedProduct2" }
        };

        _mockRepository.Setup(r => r.GetPagedAsync<Producto>(1, 2)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductoDto>>(productos)).Returns(mappedProductos);

        //Act
        var result = await _service.GetProductsClientsAsync(1, 2);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Items.Count());
    }

    [Fact]
    public async Task CreateAsync_ShouldAddProducto_WhenValid()
    {
        //Arrange
        var dto = new CreateProductoDto { ProductName = "Product1", ProductType = 1, NumTerminal = 123 };
        var entity = new Producto { Id = 1, ProductName = "Product1" };
        var mappedDto = new ProductoDto { Id = 1, ProductName = "MappedProduct1" };

        _mockRepository.Setup(r => r.ExistsAsync(dto.ProductName, dto.ProductType, dto.NumTerminal)).ReturnsAsync(false);
        _mockMapper.Setup(m => m.Map<Producto>(dto)).Returns(entity);
        _mockMapper.Setup(m => m.Map<ProductoDto>(entity)).Returns(mappedDto);

        //Act
        var result = await _service.CreateAsync(dto);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _mockRepository.Verify(r => r.AddAsync(entity), Times.Once);
        _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenProductoExists()
    {
        //Arrange
        var dto = new CreateProductoDto { ProductName = "Product1", ProductType = 1, NumTerminal = 123 };

        _mockRepository.Setup(r => r.ExistsAsync(dto.ProductName, dto.ProductType, dto.NumTerminal)).ReturnsAsync(true);

        //Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(dto));
    }
}
