using Autofac.Core;
using Carglass.TechnicalAssessment.Backend.BL.Clients;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientAppService;

        public ClientsController(IClientService clientAppService)
        {
            _clientAppService = clientAppService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientAppService.GetAllAsync();
            return Ok(clients); 
        }

    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientAppService.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound(new { Message = $"Cliente con ID {id} no encontrado." });
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientDto = new ClientDto
            {
                DocType = dto.DocType,
                DocNum = dto.DocNum,
                Email = dto.Email,
                GivenName = dto.GivenName,
                FamilyName1 = dto.FamilyName1,
                Phone = dto.Phone
            };

            var createdClient = await _clientAppService.CreateAsync(clientDto);
            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientPutDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientDto = new ClientDto
            {
                Id = id, 
                DocType = dto.DocType,
                DocNum = dto.DocNum,
                GivenName = dto.GivenName,
                FamilyName1 = dto.FamilyName1,
            };

            await _clientAppService.UpdateAsync(clientDto);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _clientAppService.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound(new { Message = $"Cliente con ID {id} no encontrado." });
            }

            await _clientAppService.DeleteAsync(id); 

            return NoContent(); 
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 10)
        {
            var result = await _clientAppService.GetPagedClientsAsync(page, pageSize);
            return Ok(result);
        }

    }
}
