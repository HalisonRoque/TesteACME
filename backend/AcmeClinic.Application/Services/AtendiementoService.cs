using AcmeClinic.Application.DTOs.AtendimentosDtos;
using AcmeClinic.Application.DTOs.PaginationDtos;
using AcmeClinic.Domain.Entities;
using AcmeClinic.Domain.Interfaces;

namespace AcmeClinic.Application.Services;

public class AtendimentoService
{
    private readonly IAtendimentoRepository _repository;

    private readonly IPacienteRepository _pacienteRepository;

    public AtendimentoService(IAtendimentoRepository repository, IPacienteRepository pacienteRepository)
    {
        _repository = repository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<int> CreateAtendimento(CreateAtendimentoDto dto)
    {
        if (dto.DataHora > DateTime.Now)
        {
            throw new Exception(
                "Não é permitido registrar atendimento futuro"
            );
        }

        var paciente = await _pacienteRepository.GetById(dto.PacienteId);

        if (paciente == null)
        {
            throw new Exception(
                "Paciente não encontrado"
            );
        }

        if (paciente.Status != "Ativo")
        {
            throw new Exception(
                "Somente pacientes ativos podem receber atendimento"
            );
        }

        var atendimento = new Atendimento
        {
            PacienteId = dto.PacienteId,
            DataHora = dto.DataHora,
            Descricao = dto.Descricao,
            Status = dto.Status
        };

        return await _repository.CreateAtendimento(atendimento);
    }

    public async Task<PagedResponse<ResponseAtendimentoDto>> GetAllAtendimentos(FilterAtendimentoDto filter)
    {
        var page = filter.Page <= 0 ? 1 : filter.Page;
        var pageSize = filter.PageSize <= 0 ? 10 : filter.PageSize;

        var data = await _repository.GetAllAntendimentos(
            filter.PacienteId,
            filter.Status,
            filter.DataInicio,
            filter.DataFim,
            page,
            pageSize
        );

        var total = await _repository.Count(
            filter.PacienteId,
            filter.Status,
            filter.DataInicio,
            filter.DataFim
        );

        return new()
        {
            Data = data.Select(
                a => new ResponseAtendimentoDto
                {
                    Id = a.Id,
                    PacienteId = a.PacienteId,
                    DataHora = a.DataHora,
                    Descricao = a.Descricao,
                    Status = a.Status
                }),

            Total = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ResponseAtendimentoDto?> GetById(int id)
    {
        var atendimento = await _repository.GetById(id);

        if (atendimento == null)
        {
            throw new Exception(
                "Atendimento não encontrado"
            );
        }

        return new()
        {
            Id = atendimento.Id,
            PacienteId = atendimento.PacienteId,
            DataHora = atendimento.DataHora,
            Descricao = atendimento.Descricao,
            Status = atendimento.Status
        };
    }

    public async Task UpdateAtendimento(int id, UpdateAtendimentoDto dto)
    {
        if (dto.DataHora > DateTime.Now)
        {
            throw new Exception(
                "Data futura não permitida"
            );
        }

        var atendimento = await _repository.GetById(id);

        if (atendimento == null)
        {
            throw new Exception(
                "Atendimento não encontrado"
            );
        }

        atendimento.DataHora = dto.DataHora;
        atendimento.Descricao = dto.Descricao;
        atendimento.Status = dto.Status;

        await _repository.UpdateAtendimento(atendimento);
    }

    public async Task InactivateAtendimento(int id)
    {
        var atendimento = await _repository.GetById(id);

        if (atendimento == null)
        {
            throw new Exception(
                "Atendimento não encontrado"
            );
        }

        await _repository.InactivateAntedimento(id);
    }

    public async Task ActivateAtendimento(int id)
    {
        var atendimento = await _repository.GetById(id);

        if (atendimento == null)
        {
            throw new Exception(
                "Atendimento não encontrado"
            );
        }

        await _repository.ActivateAntedimento(id);
    }
}