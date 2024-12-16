using Carglass.TechnicalAssessment.Backend.DL.Interfaces;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly ApplicationDbContext _context;

    public ProductoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
            .Where(producto => producto != null)
            .ToListAsync();
    }


    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _context.Productos.FindAsync(id);
    }

    public async Task AddAsync(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
        await _context.SaveChangesAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Producto producto)
    {
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<PagedResultDto<T>> GetPagedAsync<T>(int page, int pageSize) where T : class
    {
        var query = _context.Set<T>();
        var totalItems = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResultDto<T>
        {
            Items = items,
            TotalCount = totalItems
        };
    }

    public async Task<bool> ExistsAsync(string productName, int productType, long numTerminal)
    {
        return await _context.Productos.AnyAsync(p =>
            p.ProductName == productName &&
            p.ProductType == productType &&
            p.NumTerminal == numTerminal);
    }

}
