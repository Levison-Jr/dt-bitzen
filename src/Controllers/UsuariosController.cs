using AutoMapper;
using DTBitzen.Dtos;
using DTBitzen.Identity;
using DTBitzen.Models;
using DTBitzen.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DTBitzen.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IUrlHelper _urlHelper;

        public UsuariosController(IIdentityHelper identityHelper,
            IMapper mapper,
            IUsuarioService usuarioService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _identityHelper = identityHelper;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUsuarioPorId(string id)
        {
            Usuario? usuario = await _usuarioService.BuscarPorId(id);

            if (usuario is null)
                return NotFound();

            UsuarioDto usuarioDto = _mapper.Map<Usuario?, UsuarioDto>(usuario);

            usuarioDto.Links!.Add(new LinkRef(
                _urlHelper.Link(nameof(Editar), new { id = usuarioDto.Id })!,
                "update",
                "PUT"
            ));

            usuarioDto.Links!.Add(new LinkRef(
                _urlHelper.Link(nameof(Excluir), new { id = usuarioDto.Id })!,
                "delete",
                "DELETE"
            ));

            return Ok(usuarioDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Senha))
                return BadRequest();


            LoginResposta resultado = await _identityHelper.Login(loginDto.Email, loginDto.Senha);

            if (!resultado.Sucesso)
                return Unauthorized();

            return Ok(resultado);
        }


        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarDto registrarDto)
        {
            if (string.IsNullOrEmpty(registrarDto.Nome) || 
                string.IsNullOrEmpty(registrarDto.Email) || 
                string.IsNullOrEmpty(registrarDto.Senha))
            {
                return BadRequest();
            }

            RegistrarResposta resultado =
                await _identityHelper.Registrar(registrarDto.Nome,
                    registrarDto.Email,
                    registrarDto.Senha);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Mensagem);

            return CreatedAtAction(
                actionName: nameof(BuscarUsuarioPorId),
                routeValues: new { id = resultado.Usuario?.Id },
                value: _mapper.Map<Usuario?, UsuarioDto>(resultado.Usuario));
        }

        [Authorize]
        [HttpPut("{id}", Name = nameof(Editar))]
        public async Task<IActionResult> Editar(string id, [FromBody]UsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<UsuarioDto, Usuario>(usuarioDto);
            bool sucessoNaAtualizacao = await _usuarioService.Editar(id, usuario);

            if (!sucessoNaAtualizacao)
                return BadRequest();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}", Name = nameof(Excluir))]
        public async Task<IActionResult> Excluir(string id)
        {
            bool sucessoNaExclusao = await _usuarioService.Excluir(id);

            if (!sucessoNaExclusao)
                return BadRequest();

            return NoContent();
        }
    }
}
