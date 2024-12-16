using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients
                .Where(client => client != null)
                .ToListAsync();
        }


        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task AddAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client entity)
        {
            _context.Clients.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string docNum, string? docType = null, int? id = null)
        {
            return await _context.Clients.AnyAsync(client =>
                client.DocNum == docNum &&
                (docType == null || client.DocType == docType) &&
                (!id.HasValue || client.Id != id));
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChangesAsync()
        {
            var changes = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var change in changes)
            {
                change.State = EntityState.Detached;
            }
        }

        public async Task DeleteChangesAsync(string docType)
        {
            var changes = _context.Clients.Where(c => c.DocType == docType).ToList();

            _context.Clients.RemoveRange(changes);
            await _context.SaveChangesAsync();
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

    }
}
