using AutoMapper;
using DTBitzen.Dtos;
using DTBitzen.Identity;
using DTBitzen.Models;
using Microsoft.AspNetCore.Mvc;

namespace DTBitzen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IMapper _mapper;

        public UsuariosController(IIdentityHelper identityHelper, IMapper mapper)
        {
            _identityHelper = identityHelper;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public string BuscarUsuarioPorId(int id)
        {
            return "value";
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
        public void Editar(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Excluir(int id)
        {
        }
    }
}
