using AutoMapper;
using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.DTOs;
using BackendSolidario.Core.Models;
using BackendSolidario.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace BackendSolidario.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MovimientoController : Controller {

        private readonly IMapper _mapper;
        private readonly IMovimientoService _movimientoService;

        public MovimientoController(IMovimientoService movimientoService, IMapper mapper) {
            _movimientoService = movimientoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            try {

                ApiResponse<IEnumerable<MovimientoDto>> res = new();
                var lstMovimientos = await _movimientoService.GetMovimientosAsync();
                res.Data = _mapper.Map<IEnumerable<MovimientoDto>>(lstMovimientos);

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("ByNumeroCuenta/{numeroCuenta}", Name = "GetMovimientosByNumeroCuenta")]
        public async Task<IActionResult> GetMovimientosByNumeroCuentaAsync(string numeroCuenta) {
            try {

                var lstMovimientos = await _movimientoService.GetMovimientosByNumeroCuentaAsync(numeroCuenta);

                ApiResponse<IEnumerable<MovimientoDto>> res = new();
                res.Data = _mapper.Map<IEnumerable<MovimientoDto>>(lstMovimientos);

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("ById/{id}", Name = "GetMovimiento")]
        public async Task<IActionResult> GetMovimientoByIdAsync(int id) {
            try {

                var movimiento = await _movimientoService.GetMovimientoByIdAsync(id);

                ApiResponse<MovimientoDto> res = new();
                res.Data = _mapper.Map<MovimientoDto>(movimiento);

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddMovimientoAsync(MovimientoDto MovimientoDto) {
            try {

                ApiResponse<MovimientoDto> res = new();
                var movimiento = _mapper.Map<Movimiento>(MovimientoDto);
                Movimiento movimientoNuevo = await _movimientoService.AddMovimientoAsync(movimiento);
                res.Success = true;
                res.Message = "OK";
                res.Data = _mapper.Map<MovimientoDto>(movimientoNuevo);

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateMovimientoAsync([FromBody] MovimientoDto MovimientoDto) {
            try {

                ApiResponse<bool> res = new();
                var movimiento = _mapper.Map<Movimiento>(MovimientoDto);
                bool success = await _movimientoService.UpdateMovimientoAsync(movimiento);
                res.Success = true;
                res.Data = success;
                res.Message = success ? "OK" : "Error al actualizar";

                return Ok(res);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientoAsync(int id) {
            try {
                ApiResponse<bool> res = new();
                bool success = await _movimientoService.DeleteMovimientoAsync(id);
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
