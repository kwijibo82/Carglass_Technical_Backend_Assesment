using AutoMapper;
using Carglass.TechnicalAssessment.Backend.BL.Clients;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ClientDto> _validator;

        public ClientService(IClientRepository clientRepository, IMapper mapper, IValidator<ClientDto> validator)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            var client = await FindClientOrThrowAsync(id);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateAsync(ClientDto dto)
        {
            ValidateDto(dto);

            if (await _clientRepository.ExistsAsync(dto.DocNum, dto.DocType))
            {
                throw new InvalidOperationException("Ya existe un cliente con el mismo tipo y número de documento.");
            }

            var clientEntity = _mapper.Map<Client>(dto);
            await _clientRepository.AddAsync(clientEntity);
            await _clientRepository.SaveChangesAsync();

            return _mapper.Map<ClientDto>(clientEntity);
        }

        public async Task UpdateAsync(ClientDto dto)
        {
            var client = await FindClientOrThrowAsync(dto.Id);

            ValidateDto(dto);

            if (await _clientRepository.ExistsAsync(dto.DocNum, dto.DocType, dto.Id))
            {
                throw new InvalidOperationException("Ya existe otro cliente con el mismo tipo y número de documento.");
            }

            _mapper.Map(dto, client);
            await _clientRepository.UpdateAsync(client);
            await _clientRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await FindClientOrThrowAsync(id);

            await _clientRepository.DeleteAsync(client.Id);
            await _clientRepository.SaveChangesAsync();
        }

        public async Task<PagedResultDto<ClientDto>> GetPagedClientsAsync(int page, int pageSize)
        {
            var result = await _clientRepository.GetPagedAsync<Client>(page, pageSize);

            return new PagedResultDto<ClientDto>
            {
                Items = _mapper.Map<IEnumerable<ClientDto>>(result.Items),
                TotalCount = result.TotalCount
            };
        }

        private async Task<Client> FindClientOrThrowAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Cliente con Id {id} no encontrado.");
            }
            return client;
        }

        private void ValidateDto(ClientDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
