using Microsoft.AspNetCore.Mvc;
using AcmeClinic.Application.Services;
using AcmeClinic.Application.DTOs.PacientesDtos;

namespace AcmeClinic.Api.Controllers;

[ApiController]
[Route("api/pacientes")]
public class PacientesController: ControllerBase
{
    private readonly PacienteService _service;

    public PacientesController(PacienteService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatePacienteDto body)
    {
        var paciente = await _service.CreatePaciente(body);
        return Ok(new {paciente});
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] FilterPacienteDto filter)
    {
        var response = await _service.GetAll(filter);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var paciente = await _service.GetById(id);
        return Ok(paciente);
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> UpdatePaciente(int id, UpdatePacienteDto body)
    {
        await _service.UpdatePaciente(id, body);
        return NoContent();
    }

    [HttpPatch("{id}/inativar")]
    public async Task<IActionResult> InactivatePaciente(int id)
    {
        await _service.InactivatePaciente(id);
        return NoContent();
    }

    [HttpPatch("{id}/ativar")]
    public async Task<IActionResult> ActivatePaciente(int id)
    {
        await _service.ActivatePaciente(id);
        return NoContent();
    }
}