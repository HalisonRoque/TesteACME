using AcmeClinic.Application.DTOs.PacientesDtos;
using AcmeClinic.Application.DTOs.PaginationDtos;
using AcmeClinic.Domain.Interfaces;
using AcmeClinic.Domain.Entities;

namespace AcmeClinic.Application.Services;

public class PacienteService
{
    private readonly IPacienteRepository _repository;

    public PacienteService(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CreatePaciente(CreatePacienteDto dto)
    {
        if (await _repository.ExistsCPF(dto.CPF))
        {
            throw new Exception("CPF já cadastrado");
        }

        var paciente = new Paciente
        {
            Nome = dto.Nome,
            DataNascimento = dto.DataNascimento,
            CPF = dto.CPF,
            Sexo = dto.Sexo,
            Cidade = dto.Cidade,
            CEP = dto.CEP,
            Bairro = dto.Bairro,
            Endereco = dto.Endereco,
            Complemento = dto.Complemento,
            Status = dto.Status
        };

        return await _repository.Create(paciente);
    }

    public async Task<PagedResponse<ResponsePacienteDto>> GetAll(FilterPacienteDto filter)
    {
        var page = filter.Page <= 0 ? 1 : filter.Page;

        var pageSize = filter.PageSize <= 0 ? 10 : filter.PageSize;

        var data = await _repository
            .GetAll(
                filter.Nome,
                filter.CPF,
                filter.Status,
                page,
                pageSize
            );

        var total = await _repository
            .Count(
                filter.Nome,
                filter.CPF,
                filter.Status
            );

        return new()
        {
            Data = data.Select(
                p => new ResponsePacienteDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    CPF = p.CPF,
                    Sexo = p.Sexo,
                    Cidade = p.Cidade,
                    Status = p.Status
                }),

            Total = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ResponsePacienteDto?> GetById(int id)
    {
        var paciente = await _repository.GetById(id);

        if (paciente == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        return new()
        {
            Id = paciente.Id,
            Nome = paciente.Nome,
            CPF = paciente.CPF,
            Sexo = paciente.Sexo,
            Cidade = paciente.Cidade,
            Status = paciente.Status
        };
    }

    public async Task UpdatePaciente(int id, UpdatePacienteDto dto)
    {
        var paciente = await _repository.GetById(id);

        if (paciente == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        paciente.Nome = dto.Nome;
        paciente.DataNascimento = dto.DataNascimento;
        paciente.Sexo = dto.Sexo;
        paciente.CEP = dto.CEP;
        paciente.Cidade = dto.Cidade;
        paciente.Bairro = dto.Bairro;
        paciente.Endereco = dto.Endereco;
        paciente.Complemento = dto.Complemento;
        paciente.Status = dto.Status;

        await _repository.UpdatePaciente(paciente);
    }

    public async Task InactivatePaciente(int id)
    {
        var paciente = await _repository.GetById(id);

        if (paciente == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        await _repository.InactivatePaciente(id);
    }

    public async Task ActivatePaciente(int id)
    {
        var paciente = await _repository.GetById(id);

        if (paciente == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        await _repository.ActivatePaciente(id);
    }
}