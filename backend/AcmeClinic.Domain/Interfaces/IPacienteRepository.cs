using AcmeClinic.Domain.Entities;

namespace AcmeClinic.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<int> Create(Paciente paciente);

        Task<IEnumerable<Paciente>> GetAll(
            string? nome,
            string? cpf,
            string? status,
            int page,
            int pageSize
        );

        Task<Paciente?> GetById(int id);

        Task<bool> ExistsCPF(string cpf);

        Task<int> Count(
            string? nome,
            string? cpf,
            string? status
        );

        Task UpdatePaciente(Paciente paciente);

        Task InactivatePaciente(int id);

        Task ActivatePaciente(int id);
    }
}