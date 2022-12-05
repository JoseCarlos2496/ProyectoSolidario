using AutoMapper;
using BackendSolidario.Core.DTOs;
using BackendSolidario.Core.Models;

namespace BackendSolidario.Infrastructure.Mappings {
    public class AutoMapperBackendSolidarioProfile : Profile {
        public AutoMapperBackendSolidarioProfile() {

            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>();

            CreateMap<Cuenta, CuentaDto>();
            CreateMap<CuentaDto, Cuenta>();

            CreateMap<Movimiento, MovimientoDto>();
            CreateMap<MovimientoDto, Movimiento>();
        }
    }
}
