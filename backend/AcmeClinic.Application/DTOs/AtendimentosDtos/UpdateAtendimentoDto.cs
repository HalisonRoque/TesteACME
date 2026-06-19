using System.ComponentModel.DataAnnotations;

namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class UpdateAtendimentoDto
{
    [Required(ErrorMessage = "Data e hora são obrigatório!")]
    public DateTime DataHora { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatório!")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Status é obrigatório!")]
    public string Status { get; set; } = string.Empty;
}