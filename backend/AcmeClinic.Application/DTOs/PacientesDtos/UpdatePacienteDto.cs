using System.ComponentModel.DataAnnotations;

namespace AcmeClinic.Application.DTOs.PacientesDtos;

public class UpdatePacienteDto
{

    [Required(ErrorMessage = "Nome é obrigatório!")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de nascimento é obrigatório!")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "Genero é obrigatório!")]
    public string Sexo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cidade é obrigatório!")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "CEP é obrigatório!")]
    public string CEP { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bairro é obrigatório!")]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Endereço é obrigatório!")]
    public string Endereco { get; set; } = string.Empty;

    public string? Complemento { get; set; }

    [Required(ErrorMessage = "Status é obrigatório!")]
    public string Status { get; set; } = string.Empty;
}