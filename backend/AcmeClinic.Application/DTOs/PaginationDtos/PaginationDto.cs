namespace AcmeClinic.Application.DTOs.PaginationDtos;

public class PaginationDto
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}