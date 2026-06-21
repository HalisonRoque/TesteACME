using System.ComponentModel.DataAnnotations;

namespace AcmeClinic.Application.DTOs.AtendimentosDtos;

public class CreateAtendimentoDto
{
    [Required(ErrorMessage = "Paciente é obrigatório!")]
    public int PacienteId { get; set; }

    [Required(ErrorMessage = "Data é obrigatório!")]
    public DateTime Data { get; set; }

    [Required(ErrorMessage = "Horário é obrigatório!")]
    public string Hora { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatório!")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Status é obrigatório!")]
    public string Status { get; set; } = string.Empty;
}