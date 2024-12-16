

using Carglass.TechnicalAssessment.Backend.Dtos;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(string docNum, string? docType = null, int? id = null);
        Task<PagedResultDto<T>> GetPagedAsync<T>(int page, int pageSize) where T : class;
        Task SaveChangesAsync();
        Task DeleteChangesAsync();
        Task DeleteChangesAsync(string docType);
    }
}
