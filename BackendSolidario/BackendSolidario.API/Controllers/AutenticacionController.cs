using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendSolidario.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : Controller {

        private readonly ILogger<AutenticacionController> _logger;
        private readonly IAutenticacionService _autenticacionService;
        private readonly IConfiguration _config;


        public AutenticacionController(IAutenticacionService autenticacionService, IConfiguration configuration, ILogger<AutenticacionController> logger) {
            _autenticacionService = autenticacionService;
            _config = configuration;
            _logger = logger;
        }

        // POST: api/<AutenticacionController>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login login) {

            try {
                var bearerToken = _autenticacionService.Autenticacion(login);

                return Ok(bearerToken);
            } catch (Exception e) {                
                return BadRequest(e.Message);
            }
        }
    }
}