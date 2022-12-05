using AutoMapper;
using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.DTOs;
using BackendSolidario.Core.Models;
using BackendSolidario.Core.Response;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BackendSolidario.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ClienteController : Controller {

        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService = Substitute.For<IClienteService>();

        public ClienteController(IClienteService clienteService, IMapper mapper) {
            _mapper = mapper;
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            try {

                ApiResponse<IEnumerable<ClienteDto>> res = new();
                var lstClientes = await _clienteService.GetClientesAsync();
                res.Data = _mapper.Map<IEnumerable<ClienteDto>>(lstClientes);
                if (!lstClientes.Any()) {
                    return BadRequest(Constants.MULTIPLENOTFOUND);
                }
                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("ById/{id}", Name = "GetClienteByIdAsync")]
        public async Task<IActionResult> GetClienteByIdAsync(int id) {
            try {
                if(id == 0) {
                    return BadRequest(Constants.CLIENTNOTEXISTS);
                }

                var cliente = await _clienteService.GetClienteByIdAsync(id);
                ApiResponse<ClienteDto> res = new();
                res.Data = _mapper.Map<ClienteDto>(cliente);

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("ByUsername/{username}", Name = "GetClienteByUsername")]
        public async Task<IActionResult> GetClienteByUsername(string username) {
            try {

                var cliente = await _clienteService.GetClienteByUsername(username);
                ApiResponse<ClienteDto> res = new();
                res.Data = _mapper.Map<ClienteDto>(cliente);

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddClienteAsync(ClienteDto clienteDTO) {
            try {

                ApiResponse<ClienteDto> res = new();
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                Cliente clienteNuevo = await _clienteService.AddClienteAsync(cliente);
                res.Success = true;
                res.Message = "OK";
                res.Data = _mapper.Map<ClienteDto>(clienteNuevo);

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateClienteAsync([FromBody] ClienteDto clienteDTO) {
            try {

                ApiResponse<bool> res = new();
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                bool success = await _clienteService.UpdateClienteAsync(cliente);
                res.Success = true;
                res.Data = success;
                res.Message = success ? "OK" : "Error al actualizar";

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClienteAsync(int id) {
            try {
                ApiResponse<bool> res = new();
                bool success = await _clienteService.DeleteClienteAsync(id);
                res.Success = true;
                res.Data = success;
                res.Message = success ? "OK" : "Error al eliminar";
                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
