using BackendSolidario.Core.Models;

namespace BackendSolidario.Business.Interfaces {
    public interface IAutenticacionService {
        public Task<string> Autenticacion(Login login);

    }
}
