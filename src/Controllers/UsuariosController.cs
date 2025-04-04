using AutoMapper;
using DTBitzen.Dtos;
using DTBitzen.Identity;
using DTBitzen.Models;
using DTBitzen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DTBitzen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IIdentityHelper identityHelper,
            IMapper mapper,
            IUsuarioService usuarioService)
        {
            _identityHelper = identityHelper;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUsuarioPorId(string id)
        {
            Usuario? usuario = (await _usuarioService.BuscarPorId(id));
            return Ok(_mapper.Map<Usuario?, UsuarioDto>(usuario));
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(string id, [FromBody]UsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<UsuarioDto, Usuario>(usuarioDto);
            bool sucessoNaAtualizacao = await _usuarioService.Editar(id, usuario);

            if (!sucessoNaAtualizacao)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            bool sucessoNaExclusao = await _usuarioService.Excluir(id);

            if (!sucessoNaExclusao)
                return BadRequest();

            return NoContent();
        }
    }
}
