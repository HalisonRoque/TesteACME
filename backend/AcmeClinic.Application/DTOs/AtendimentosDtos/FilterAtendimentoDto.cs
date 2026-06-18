namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class FilterAtendimentoDto
{
    public int? PacienteId { get; set; }

    public string? Status { get; set; }

    public DateTime? DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}