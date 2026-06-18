namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class UpdateAtendimentoDto
{
    public DateTime DataHora { get; set; }

    public string Descricao { get; set; }

    public string Status { get; set; }
}