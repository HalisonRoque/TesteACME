using Moq;
using FluentAssertions;
using AcmeClinic.Application.Services;
using AcmeClinic.Application.DTOs.PacientesDtos;
using AcmeClinic.Domain.Interfaces;
using AcmeClinic.Domain.Entities;
using AcmeClinic.Application.Exceptions;

namespace AcmeClinic.Tests.Services;

public class PacienteServiceTests
{
    private readonly Mock<IPacienteRepository> _repository;
    private readonly PacienteService _service;
    public PacienteServiceTests()
    {
        _repository = new();
        _service = new(_repository.Object);
    }

    [Fact]
    public async Task Deve_Criar_Paciente()
    {
        var dto = new CreatePacienteDto
        {
            Nome = "João",
            CPF = "11111111111",
            Sexo = "Masculino",
            Cidade = "CG",
            CEP = "58400000",
            Bairro = "Centro",
            Endereco = "Rua A",
            Status = "Ativo",
            DataNascimento = new DateTime(1995,1,1)
        };

        _repository.Setup(
            p => p.ExistsCPF(dto.CPF)
        )
        .ReturnsAsync(false);

        _repository.Setup(
            p => p.Create(It.IsAny<Paciente>())
        )
        .ReturnsAsync(1);

        var result = await _service.CreatePaciente(dto);

        result.Should().Be(1);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_CPF_Duplicado()
    {
        var dto = new CreatePacienteDto
        {
            Nome = "João",
            CPF = "111"
        };

        _repository.Setup(
            p => p.ExistsCPF(dto.CPF)
        )
        .ReturnsAsync(true);

        var action = async () => await _service
            .CreatePaciente(
                dto
            );

        await action.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Deve_Retornar_Paciente()
    {
        _repository
            .Setup(p => p.GetById(1))
            .ReturnsAsync(
                new Paciente
                {
                    Id=1,
                    Nome="João"
                });

        var result = await _service.GetById(1);
        result!.Nome.Should().Be("João");
    }

    [Fact]
    public async Task Deve_Retornar_404_Nao_Encontrado()
    {
        _repository
            .Setup(
                p => p.GetById(1)
            )
            .ReturnsAsync((Paciente?)null);

        var action = async () => await _service.GetById(1);
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Deve_Atualizar_Paciente()
    {
        _repository
            .Setup(p => p.GetById(1))
            .ReturnsAsync(
                new Paciente
                {
                    Id = 1
                }
            );
    
        var dto = new UpdatePacienteDto
        {
            Nome = "João Atualizado",
            DataNascimento = new DateTime(1995, 1, 1),
            Sexo = "Masculino",
            Cidade = "Campina Grande",
            CEP = "58400000",
            Bairro = "Centro",
            Endereco = "Rua A",
            Complemento = "Casa",
            Status = "Ativo"
        };
    
        await _service.UpdatePaciente(1, dto);
    
        _repository.Verify(
            p => p.UpdatePaciente(
                It.IsAny<Paciente>()
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task Deve_Inativar_Paciente()
    {
        _repository
            .Setup(p => p.GetById(1))
            .ReturnsAsync(
                new Paciente()
            );

        await _service.InactivatePaciente(1);
        _repository.Verify(p => p.InactivatePaciente(1), Times.Once);
    }

    [Fact]
    public async Task Deve_Ativar_Paciente()
    {
        _repository
            .Setup(p => p.GetById(1))
            .ReturnsAsync(
                new Paciente()
            );

        await _service.ActivatePaciente(1);
        _repository.Verify(p => p.ActivatePaciente(1), Times.Once);
    }
}