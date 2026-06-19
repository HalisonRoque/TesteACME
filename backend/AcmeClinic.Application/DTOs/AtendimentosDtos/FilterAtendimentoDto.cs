using AcmeClinic.Application.DTOs.PaginationDtos;

namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class FilterAtendimentoDto : PaginationDto
{
    public int? PacienteId { get; set; }

    public string? Status { get; set; }

    public DateTime? DataInicio { get; set; }

    public DateTime? DataFim { get; set; }
}