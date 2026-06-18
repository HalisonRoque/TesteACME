namespace AcmeClinic.Application.DTOs.PaginationDtos;

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; }

    public int Total { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}