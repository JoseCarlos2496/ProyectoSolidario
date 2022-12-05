using BackendSolidario.Business.Helper;
using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.Exceptions;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using BackendSolidario.Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendSolidario.Business.Services {
    public class AutenticacionService : IAutenticacionService {

        private ILogger<AutenticacionService> _logger;

        private readonly IClienteRepository _clienteRepository;
        private readonly IConfiguration _config;

        public AutenticacionService(IClienteRepository clienteRepository, IConfiguration config, ILogger<AutenticacionService> logger) {
            _logger = logger;
            _config = config;
            _clienteRepository = clienteRepository;

        }

        public async Task<string> Autenticacion(Login login) {


            Cliente usuario_ = null;
            var username = login.ClienteId;

            try {
                usuario_ = await _clienteRepository.GetByUsername(username);
                if (usuario_.Estado == false) { return NotFound(ErrorHelper.Response(404, "Usuario no encontrado.")); }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }


            if (HashHelper.CheckHash(login.Contrasena, usuario_.Contrasena, usuario_.Salt)) {
                var secretKey = _config.GetSection("SecretKey").Value;
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.ClienteId));

                var tokenDescriptor = new SecurityTokenDescriptor() {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);


                return bearer_token;

            }
            throw new BusinessException("Ocurrió un error en la generación del token"); ;
        }

        private string NotFound(ResponseObject responseObject) {
            throw new NotFoundException(responseObject.Message);
        }
    }
}
