using AutoMapper;
using BackendSolidario.Core.Models;
using BackendSolidario.Core.Response;
using Microsoft.AspNetCore.Mvc;
using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.Exceptions;

namespace BackendSolidario.Controllers {
    [ApiController]
    [Route("api/[controller]")]

    public class ReporteController : Controller {

        private readonly IMapper _mapper;
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService, IMapper mapper) {
            _mapper = mapper;
            _reporteService = reporteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReporteAsync([FromQuery] string identificacion) {
            try {

                ApiResponse<IEnumerable<ReporteMovimiento>> res = new();
                var reporteMovimientos = await _reporteService.GetReporteCuenta(identificacion);
                res.Data = _mapper.Map<IEnumerable<ReporteMovimiento>>(reporteMovimientos);

                if (!reporteMovimientos.Any()) {
                    return BadRequest(Constants.NONACCOUNT);
                }

                return Ok(res);

            } catch (BusinessException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
