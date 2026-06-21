namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class ResponseAtendimentoDto
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public string PacienteNome { get; set; } = string.Empty;

    public string Data { get; set; } = string.Empty;

    public string Hora { get; set; } = string.Empty; 

    public string Descricao { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}