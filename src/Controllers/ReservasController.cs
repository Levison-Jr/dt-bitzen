using AutoMapper;
using DTBitzen.Dtos;
using DTBitzen.Models;
using DTBitzen.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DTBitzen.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public ReservasController(IReservaService reservaService,
            IMapper mapper,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _reservaService = reservaService;
            _mapper = mapper;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodas(DateOnly? filtroData, string? filtroStatus)
        {
            var reservas = await _reservaService.BuscarTodas(filtroData, filtroStatus);
            var reservasDto = _mapper.Map<IEnumerable<Reserva>, IEnumerable<ReservaDto>>(reservas);

            foreach (var reserva in reservasDto)
            {
                reserva.Links ??= [];

                reserva.Links.Add(new LinkRef(
                    _urlHelper.Link(nameof(CancelarReserva), new { id = reserva.ReservaId })!,
                    "cancelar",
                    "PATCH"
                ));
            }

            return Ok(reservasDto);
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> BuscarPorUsuarioId(string id, DateOnly? filtroData, string? filtroStatus)
        {
            var reservasPorUsuario = await _reservaService.BuscarPorUsuarioId(id, filtroData, filtroStatus);
            var reservasDto = _mapper.Map<IEnumerable<Reserva>, IEnumerable<ReservaDto>>(reservasPorUsuario);

            foreach (var reserva in reservasDto)
            {
                reserva.Links!.Add(new LinkRef(
                    _urlHelper.Link(nameof(CancelarReserva), new { id = reserva.ReservaId })!,
                    "cancelar",
                    "PATCH"
                ));
            }

            return Ok(reservasDto);
        }

        [HttpGet("sala/{id}")]
        public async Task<IActionResult> BuscarPorSalaId(int id, DateOnly? filtroData, string? filtroStatus)
        {
            var reservasPorSala = await _reservaService.BuscarPorSalaId(id, filtroData, filtroStatus);
            var reservasDto = _mapper.Map<IEnumerable<Reserva>, IEnumerable<ReservaDto>>(reservasPorSala);

            foreach (var reserva in reservasDto)
            {
                reserva.Links!.Add(new LinkRef(
                    _urlHelper.Link(nameof(CancelarReserva), new { id = reserva.ReservaId })!,
                    "cancelar",
                    "PATCH"
                ));
            }

            return Ok(reservasDto);
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

        [HttpPatch("{id}", Name = nameof(CancelarReserva))]
        public async Task<IActionResult> CancelarReserva(Guid id)
        {
            bool sucesso = await _reservaService.CancelarReserva(id);

            if (!sucesso)
                return BadRequest();

            return NoContent();
        }
    }
}
