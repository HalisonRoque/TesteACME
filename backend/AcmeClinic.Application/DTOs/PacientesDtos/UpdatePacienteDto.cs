namespace AcmeClinic.Application.DTOs.PacientesDtos;

public class UpdatePacienteDto
{
    public required string Nome { get; set; }

    public required DateTime DataNascimento { get; set; }

    public required string Sexo { get; set; }

    public required string Cidade { get; set; }

    public required string CEP { get; set; }

    public required string Bairro { get; set; }

    public required string Endereco { get; set; }

    public string? Complemento { get; set; }

    public required string Status { get; set; }
}