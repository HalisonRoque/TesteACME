namespace AcmeClinic.Domain.Entities;

public class Atendimento
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public DateTime Data { get; set; }

    public string Hora { get; set; }

    public string Descricao { get; set; }

    public string Status { get; set; }
}