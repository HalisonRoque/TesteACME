using Microsoft.AspNetCore.Mvc;
using AcmeClinic.Domain.Entities;
using AcmeClinic.Domain.Interfaces;

namespace AcmeClinic.Api.Controllers;

[ApiController]
[Route("api/pacientes")]
public class PacientesController: ControllerBase
{
    private readonly IPacienteRepository _repository;

    public PacientesController(IPacienteRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Paciente paciente)
    {
        var id = await _repository.Create(paciente);

        return Ok(new {id});
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _repository.GetAll();

        return Ok(response);
    }
}