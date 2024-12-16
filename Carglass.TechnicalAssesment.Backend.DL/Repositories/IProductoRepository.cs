using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Interfaces;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task AddAsync(Producto producto);
    Task<bool> ExistsAsync(string productName, int productType, long numTerminal);
    Task SaveChangesAsync();
    Task UpdateAsync(Producto producto);
    Task DeleteAsync(int id);
    Task<PagedResultDto<T>> GetPagedAsync<T>(int page, int pageSize) where T : class;


}
