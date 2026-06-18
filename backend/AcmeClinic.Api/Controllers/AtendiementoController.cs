using Microsoft.AspNetCore.Mvc;
using AcmeClinic.Application.Services;
using AcmeClinic.Application.DTOs.AtendimentosDtos;

namespace AcmeClinic.Api.Controllers;

[ApiController]
[Route("api/atendimentos")]
public class AtendimentosController : ControllerBase
{
    private readonly AtendimentoService _service;

    public AtendimentosController(AtendimentoService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAtendimento(CreateAtendimentoDto body)
    {
        var atendimento = await _service.CreateAtendimento(body);
        return Ok(new { atendimento });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAtendimentos( [FromQuery] FilterAtendimentoDto filter)
    {
        return Ok(await _service.GetAllAtendimentos(filter));
    }

    [HttpGet("find/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var atendimento = await _service.GetById(id);
        return Ok(atendimento);
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> UpdateAtendimento(int id, UpdateAtendimentoDto body)
    {
        await _service.UpdateAtendimento(id, body);
        return NoContent();
    }

    [HttpPatch("{id}/inativar")]
    public async Task<IActionResult> InactivateAtendimento(int id)
    {
        await _service.InactivateAtendimento(id);
        return NoContent();
    }

    [HttpPatch("{id}/ativar")]
    public async Task<IActionResult> ActivateAtendimento(int id)
    {
        await _service.ActivateAtendimento(id);
        return NoContent();
    }
}