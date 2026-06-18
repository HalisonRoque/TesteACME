using AcmeClinic.Application.DTOs.PaginationDtos;

namespace AcmeClinic.Application.DTOs.PacientesDtos;

public class FilterPacienteDto : PaginationDto
{
    public string? Nome { get; set; }

    public string? CPF { get; set; }

    public string? Status { get; set; }
}