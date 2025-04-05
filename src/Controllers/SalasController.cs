using DTBitzen.Dtos;
using DTBitzen.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DTBitzen.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly ISalaService _salaService;
        public SalasController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(SalaRequestDto salaRequestDto)
        {
            if (string.IsNullOrEmpty(salaRequestDto.Nome))
                return BadRequest();

            bool sucesso = await _salaService.Criar(salaRequestDto.Nome, salaRequestDto.Capacidade);

            if (!sucesso)
                return BadRequest();

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, SalaRequestDto salaRequestDto)
        {
            if (string.IsNullOrEmpty(salaRequestDto.Nome))
                return BadRequest();

            bool sucesso = await _salaService.Editar(id, salaRequestDto.Nome, salaRequestDto.Capacidade);

            if (!sucesso)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            bool sucesso = await _salaService.Excluir(id);

            if (!sucesso)
                return BadRequest();

            return NoContent();
        }
    }
}
