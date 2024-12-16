using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Dtos.Products;
using Carglass.TechnicalAssessment.Backend.Entities;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;

namespace Carglass.TechnicalAssessment.Backend.BL.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<ProductoDto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task AddAsync(Producto producto);
    Task UpdateAsync(Producto producto);
    Task DeleteAsync(int id);
    Task<PagedResultDto<ProductoDto>> GetProductsClientsAsync(int page, int pageSize);
}