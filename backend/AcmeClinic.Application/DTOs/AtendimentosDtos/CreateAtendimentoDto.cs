using System.ComponentModel.DataAnnotations;

namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class CreateAtendimentoDto
{
    [Required(ErrorMessage = "Paciente é obrigatório!")]
    public int PacienteId { get; set; }

    [Required(ErrorMessage = "Data e hora são obrigatório!")]
    public DateTime DataHora { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatório!")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Status é obrigatório!")]
    public string Status { get; set; } = string.Empty;
}