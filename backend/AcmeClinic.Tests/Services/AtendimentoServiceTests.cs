using Moq;
using FluentAssertions;
using AcmeClinic.Application.Services;
using AcmeClinic.Application.DTOs.AtendimentosDtos;
using AcmeClinic.Domain.Interfaces;
using AcmeClinic.Domain.Entities;
using AcmeClinic.Application.Exceptions;

namespace AcmeClinic.Tests.Services;

public class AtendimentoServiceTests
{
    private readonly Mock<IAtendimentoRepository> _repository;
    private readonly Mock<IPacienteRepository> _pacienteRepository;
    private readonly AtendimentoService _service;

    public AtendimentoServiceTests()
    {
        _repository = new();
        _pacienteRepository = new();
        _service = new(
            _repository.Object,
            _pacienteRepository.Object
        );
    }

    [Fact]
    public async Task Deve_Criar_Atendimento()
    {
        var dto = new CreateAtendimentoDto
        {
            PacienteId = 1,
            DataHora = DateTime.Now,
            Descricao = "Consulta",
            Status = "Ativo"
        };

        _pacienteRepository.Setup(
            p => p.GetById(1)
        )
        .ReturnsAsync(
            new Paciente
            {
                Id = 1,
                Status = "Ativo"
            }
        );

        _repository.Setup(
            p => p.CreateAtendimento(
                It.IsAny<Atendimento>()
            )
        )
        .ReturnsAsync(1);

        var result = await _service.CreateAtendimento(dto);
        result.Should().Be(1);
    }

    [Fact]
    public async Task Nao_Deve_Permitir_Data_Futura()
    {
        var dto = new CreateAtendimentoDto
        {
            PacienteId = 1,
            DataHora = DateTime.Now.AddDays(1),
            Descricao = "Teste",
            Status = "Ativo"
        };

        var action = async () => await _service.CreateAtendimento(dto);
        await action.Should().ThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task Nao_Deve_Permitir_Paciente_Inexistente()
    {
        _pacienteRepository
            .Setup(
                p => p.GetById(1)
            )
            .ReturnsAsync((Paciente?)null);

        var action = async () => await _service
            .CreateAtendimento(
                new()
                {
                    PacienteId = 1
                }
            );

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Nao_Deve_Permitir_Paciente_Inativo()
    {
        _pacienteRepository
            .Setup(
                p => p.GetById(1)
            )
            .ReturnsAsync(
                new Paciente
                {
                    Status = "Inativo"
                }
            );

        var action = async () =>
            await _service.CreateAtendimento(
                new()
                {
                    PacienteId = 1
                }
            );

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Deve_Inativar_Antendimento()
    {
        _repository.Setup(
                p => p.GetById(1)
            )
            .ReturnsAsync(
                new Atendimento()
            );

        await _service.InactivateAtendimento(1);
        _repository.Verify(p => p.InactivateAntedimento(1), Times.Once);
    }

    [Fact]
    public async Task Deve_Ativar_Antendimento()
    {
        _repository.Setup(
                p => p.GetById(1)
            )
            .ReturnsAsync(
                new Atendimento()
            );

        await _service.InactivateAtendimento(1);
        _repository.Verify(p => p.InactivateAntedimento(1), Times.Once);
    }
}