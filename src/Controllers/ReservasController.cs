using AutoMapper;
using DTBitzen.Dtos;
using DTBitzen.Models;
using DTBitzen.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DTBitzen.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;
        private readonly IMapper _mapper;
        public ReservasController(IReservaService reservaService, IMapper mapper)
        {
            _reservaService = reservaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodas()
        {
            var reservas = await _reservaService.BuscarTodas();

            return Ok(_mapper.Map<IEnumerable<Reserva>, IEnumerable<ReservaDto>>(reservas));
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> BuscarPorUsuarioId(string id, DateOnly? filtroData, string? filtroStatus)
        {
            var reservasPorUsuario = await _reservaService.BuscarPorUsuarioId(id, filtroData, filtroStatus);

            return Ok(_mapper.Map<IEnumerable<Reserva>, IEnumerable<ReservaDto>>(reservasPorUsuario));
        }

        [HttpGet("sala/{id}")]
        public async Task<IActionResult> BuscarPorSalaId(int id, DateOnly? filtroData, string? filtroStatus)
        {
            var reservasPorSala = await _reservaService.BuscarPorSalaId(id, filtroData, filtroStatus);

            return Ok(_mapper.Map<IEnumerable<Reserva>, IEnumerable<ReservaDto>>(reservasPorSala));
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody]CriarReservaDto criarEditarReservaDto)
        {
            if (criarEditarReservaDto.UsuariosIds is null || criarEditarReservaDto.UsuariosIds.Count == 0)
                return BadRequest();

            Reserva reserva = _mapper.Map<CriarReservaDto, Reserva>(criarEditarReservaDto);

            bool sucesso = await _reservaService.Criar(reserva, criarEditarReservaDto.UsuariosIds);

            if (!sucesso)
                return BadRequest();

            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            bool sucesso = await _reservaService.Excluir(id);

            if (!sucesso)
                return BadRequest();

            return NoContent();
        }
    }
}
