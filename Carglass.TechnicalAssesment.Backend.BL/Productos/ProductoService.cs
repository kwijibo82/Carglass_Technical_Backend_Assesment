using AutoMapper;
using Carglass.TechnicalAssessment.Backend.BL.Interfaces;
using Carglass.TechnicalAssessment.Backend.DL.Interfaces;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Dtos.Products;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;

namespace Carglass.TechnicalAssessment.Backend.BL.Productos;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;

    public ProductoService(IProductoRepository productoRepository, IMapper mapper)
    {
        _productoRepository = productoRepository;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<ProductoDto>> GetAllAsync()
    {
        var productos = await _productoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductoDto>>(productos);
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _productoRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Producto producto)
    {
        if (await _productoRepository.ExistsAsync(producto.ProductName, producto.ProductType, producto.NumTerminal))
        {
            throw new InvalidOperationException("Ya existe un producto con con el mismo tipo y número");
        }
        await _productoRepository.AddAsync(producto);
    }

    public async Task UpdateAsync(Producto producto)
    {
        await _productoRepository.UpdateAsync(producto);
    }

    public async Task DeleteAsync(int id)
    {
        await _productoRepository.DeleteAsync(id);
    }

    public async Task<PagedResultDto<ProductoDto>> GetProductsClientsAsync(int page, int pageSize)
    {
        var result = await _productoRepository.GetPagedAsync<Producto>(page, pageSize);
        return new PagedResultDto<ProductoDto>
        {
            Items = _mapper.Map<IEnumerable<ProductoDto>>(result.Items),
            TotalCount = result.TotalCount
        };
    }
    public async Task<ProductoDto> CreateAsync(CreateProductoDto dto)
    {
        if (await _productoRepository.ExistsAsync(dto.ProductName, dto.ProductType, dto.NumTerminal))
        {
            throw new InvalidOperationException("El producto ya existe con los mismos datos.");
        }

        var productoEntity = _mapper.Map<Producto>(dto);
        await _productoRepository.AddAsync(productoEntity);
        await _productoRepository.SaveChangesAsync();

        return _mapper.Map<ProductoDto>(productoEntity);
    }


}
