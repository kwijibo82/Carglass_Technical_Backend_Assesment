using Microsoft.AspNetCore.Mvc;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;
using Carglass.TechnicalAssessment.Backend.BL.Interfaces;
using Autofac.Core;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService ?? throw new ArgumentNullException(nameof(productoService));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound(new { Message = $"Producto con ID {id} no encontrado." });
            }
            return Ok(producto); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var producto = new Producto
            {
                ProductName = dto.ProductName,
                ProductType = dto.ProductType,
                NumTerminal = dto.NumTerminal,
                SoldAt = dto.SoldAt
            };

            await _productoService.AddAsync(producto);
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productoExistente = await _productoService.GetByIdAsync(id);
            if (productoExistente == null)
            {
                return NotFound(new { Message = $"Producto con ID {id} no encontrado." });
            }
         
            productoExistente.ProductName = dto.ProductName;
            productoExistente.ProductType = dto.ProductType;
            productoExistente.NumTerminal = dto.NumTerminal;
            productoExistente.SoldAt = dto.SoldAt;

            await _productoService.UpdateAsync(productoExistente);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound(new { Message = $"Producto con ID {id} no encontrado." });
            }

            await _productoService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(int page = 1, int pageSize = 10)
        {
            var result = await _productoService.GetProductsClientsAsync(page, pageSize);
            return Ok(result);
        }

    }
}
