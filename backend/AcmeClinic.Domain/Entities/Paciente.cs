namespace AcmeClinic.Domain.Entities;

public class Paciente
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public string CPF { get; set; }

    public string Sexo { get; set; }

    public string Cidade { get; set; }

    public string CEP { get; set; }

    public string Bairro { get; set; }

    public string Endereco { get; set; }

    public string? Complemento { get; set; }

    public string Status { get; set; }
}