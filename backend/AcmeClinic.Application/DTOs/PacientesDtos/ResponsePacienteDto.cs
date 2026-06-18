namespace AcmeClinic.Application.DTOs.PacientesDtos;

public class ResponsePacienteDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;

    public string Sexo { get; set; } = string.Empty;

    public string Cidade { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}