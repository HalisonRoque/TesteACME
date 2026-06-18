using System.ComponentModel.DataAnnotations;

namespace AcmeClinic.Application.DTOs;

public class CriarPacienteDto
{
    [Required]
    [MaxLength(150)]
    public required string Nome { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    [StringLength(11)]
    public required string CPF { get; set; }

    [Required]
    public required string Sexo { get; set; }

    [Required]
    public required string Cidade { get; set; }

    [Required]
    [StringLength(8)]
    public required string CEP { get; set; }

    [Required]
    public required string Bairro { get; set; }

    [Required]
    public required string Endereco { get; set; }

    public string? Complemento { get; set; }

    [Required]
    public required string Status { get; set; }
}