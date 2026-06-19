using AcmeClinic.Domain.Entities;

namespace AcmeClinic.Domain.Interfaces;

public interface IAtendimentoRepository
{
    Task<int> CreateAtendimento(Atendimento atendimento);

    Task<IEnumerable<dynamic>> GetAllAntendimentos(
        int? pacienteId,
        string? pacienteNome,
        string? status,
        DateTime? dataInicio,
        DateTime? dataFim,
        int page,
        int pageSize
    );

    Task<Atendimento?> GetById(int id);

    Task<int> Count(
        int? pacienteId,
        string? pacienteNome,
        string? status,
        DateTime? dataInicio,
        DateTime? dataFim
    );

    Task UpdateAtendimento(Atendimento atendimento);

    Task InactivateAntedimento(int id);

    Task ActivateAntedimento(int id);
}