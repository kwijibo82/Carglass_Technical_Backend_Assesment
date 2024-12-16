using Carglass.TechnicalAssessment.Backend.Dtos;

namespace Carglass.TechnicalAssessment.Backend.BL.Clients
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<ClientDto> GetByIdAsync(int id);
        Task<ClientDto> CreateAsync(ClientDto dto);
        Task UpdateAsync(ClientDto dto);
        Task DeleteAsync(int id);
        Task<PagedResultDto<ClientDto>> GetPagedClientsAsync(int page, int pageSize);
    }
}
