using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiCampTajamar.Model;
using ApiCampTajamar.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiCampTajamar.Controllers
{
    [Produces("application/json")]
    [Route("api/Jwt")]
    public class JwtController : Controller
    {
        private readonly JwtOptions _jwtOptions;
        private readonly JsonSerializerSettings _serializerSettings;
        public ProductoContext Context { get; set; }
        public JwtController(IOptions<JwtOptions> jwtOptions, ProductoContext _context)
            {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
            _serializerSettings = new JsonSerializerSettings

            {
                Formatting = Formatting.Indented
            };
            Context = _context;
            }

        private static void ThrowIfInvalidOptions(JwtOptions options)
            {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (options.ValidFor <= TimeSpan.Zero)
                {
                throw new ArgumentException("No puede ser cero.", nameof(JwtOptions.ValidFor));
                }
            if (options.SigningCredentials == null)
                {
                throw new ArgumentNullException(nameof(JwtOptions.SigningCredentials));
                }
            if (options.JtiGenerator == null)
                {
                throw new ArgumentNullException(nameof(JwtOptions.JtiGenerator));
                }
            }

        private Task<ClaimsIdentity> GetClaimsIdentity(Usuario user)

        {

            var us = Context.Usuario.FirstOrDefault(o => o.Login == user.Login 
                               && o.Password == Sha1Utils.GetSha1(user.Password));
            if (us != null)
                {
                return Task.FromResult(new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Token"),
                    new Claim[] { }));
                }
            return Task.FromResult<ClaimsIdentity>(null);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            var identity = await GetClaimsIdentity(usuario);
            if (identity == null)
            {
                return BadRequest("Credenciales incorrectas");
            }

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Login),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64),
            };
            _jwtOptions.UpdateToken();

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int) _jwtOptions.ValidFor.TotalSeconds
            };
            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
            }



        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}