using AcmeClinic.Domain.Entities;

namespace AcmeClinic.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<int> Create(
            Paciente paciente
        );

        Task<IEnumerable<Paciente>> GetAll();

        Task<Paciente?> GetById(int id);

        Task<Paciente?> GetByName(string name);
    }
}