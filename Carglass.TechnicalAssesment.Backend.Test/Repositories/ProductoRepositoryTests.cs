using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Entities;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.DL;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;

public class ProductoRepositoryTests
{
    private DbContextOptions<ApplicationDbContext> CreateOptions(string dbName)
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProductos()
    {
        var options = CreateOptions("TestDatabase_GetAllAsync");
        using (var context = new ApplicationDbContext(options))
        {
            context.Productos.AddRange(new List<Producto>
            {
                new Producto { Id = 1, ProductName = "Producto1" },
                new Producto { Id = 2, ProductName = "Producto2" }
            });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ProductoRepository(context);
            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProducto_WhenExists()
    {
        var options = CreateOptions("TestDatabase_GetByIdAsync");
        using (var context = new ApplicationDbContext(options))
        {
            context.Productos.Add(new Producto { Id = 1, ProductName = "Producto1" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ProductoRepository(context);
            var result = await repository.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Producto1", result.ProductName);
        }
    }

    [Fact]
    public async Task AddAsync_ShouldAddProducto()
    {
        var options = CreateOptions("TestDatabase_AddAsync");
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ProductoRepository(context);
            var newProducto = new Producto { Id = 1, ProductName = "Producto1" };

            await repository.AddAsync(newProducto);
        }

        using (var context = new ApplicationDbContext(options))
        {
            var producto = await context.Productos.SingleOrDefaultAsync(p => p.Id == 1);
            Assert.NotNull(producto);
            Assert.Equal("Producto1", producto.ProductName);
        }
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProducto()
    {
        var options = CreateOptions("TestDatabase_UpdateAsync");
        using (var context = new ApplicationDbContext(options))
        {
            context.Productos.Add(new Producto { Id = 1, ProductName = "Producto1" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ProductoRepository(context);
            var producto = await context.Productos.SingleAsync(p => p.Id == 1);
            producto.ProductName = "ProductoUpdated";

            await repository.UpdateAsync(producto);
        }

        using (var context = new ApplicationDbContext(options))
        {
            var producto = await context.Productos.SingleOrDefaultAsync(p => p.Id == 1);
            Assert.NotNull(producto);
            Assert.Equal("ProductoUpdated", producto.ProductName);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProducto_WhenExists()
    {
        var options = CreateOptions("TestDatabase_DeleteAsync");
        using (var context = new ApplicationDbContext(options))
        {
            context.Productos.Add(new Producto { Id = 1, ProductName = "Producto1" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ProductoRepository(context);
            await repository.DeleteAsync(1);
        }

        using (var context = new ApplicationDbContext(options))
        {
            var producto = await context.Productos.SingleOrDefaultAsync(p => p.Id == 1);
            Assert.Null(producto);
        }
    }

    [Fact]
    public async Task GetPagedAsync_ShouldReturnPagedResults()
    {
        var options = CreateOptions("TestDatabase_GetPagedAsync");
        using (var context = new ApplicationDbContext(options))
        {
            context.Productos.AddRange(new List<Producto>
            {
                new Producto { Id = 1, ProductName = "Producto1" },
                new Producto { Id = 2, ProductName = "Producto2" },
                new Producto { Id = 3, ProductName = "Producto3" }
            });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ProductoRepository(context);
            var result = await repository.GetPagedAsync<Producto>(1, 2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count());
            Assert.Equal(3, result.TotalCount);
        }
    }
}
