namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class CreateAtendimentoDto
{
    public int PacienteId { get; set; }

    public DateTime DataHora { get; set; }

    public string Descricao { get; set; }

    public string Status { get; set; }
}