using DTBitzen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DTBitzen.Identity
{
    public class IdentityHelper : IIdentityHelper
    {
        private readonly JwtOptions _jwtOptions;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly AspNetUserManager<Usuario> _aspNetUserManager;


        public IdentityHelper(IOptions<JwtOptions> jwtOptions,
            SignInManager<Usuario> signInManager,
            AspNetUserManager<Usuario> aspNetUserManager)
        {
            _jwtOptions = jwtOptions.Value;
            _signInManager = signInManager;
            _aspNetUserManager = aspNetUserManager;
        }

        public async Task<LoginResposta> Login(string email, string senha)
        {
            var verificarCredenciais = await _signInManager.PasswordSignInAsync(email, senha, false, true);

            if (!verificarCredenciais.Succeeded)
                return new LoginResposta(false);

            Usuario? usuario = await _aspNetUserManager.FindByEmailAsync(email);

            return new LoginResposta(true, GerarJwt(usuario));
        }

        public async Task<RegistrarResposta> Registrar(string nome, string email, string senha)
        {
            Usuario novoUsuario = new Usuario()
            {
                Nome = nome,
                Email = email,
                UserName = email
            };

            var resultadoRegistro = await _aspNetUserManager.CreateAsync(novoUsuario, senha);

            if (!resultadoRegistro.Succeeded)
            {
                return new RegistrarResposta(false, null, resultadoRegistro.Errors.Select(e => e.Description).First());
            }

            return new RegistrarResposta(true, novoUsuario);
        }

        private string GerarJwt(Usuario? user)
        {
            if (user == null || user.Email == null)
                return string.Empty;

            List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            ];

            var tokenJwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(_jwtOptions.TokenExpiration),
                notBefore: DateTime.Now,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenJwt);
        }
    }
}
