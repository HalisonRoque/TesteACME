namespace AcmeClinic.Domain.Entities;

public class Atendimento
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public DateTime DataHora { get; set; }

    public string Descricao { get; set; }

    public bool Status { get; set; }
}