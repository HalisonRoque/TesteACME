namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class ResponseAtendimentoDto
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public DateTime DataHora { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}