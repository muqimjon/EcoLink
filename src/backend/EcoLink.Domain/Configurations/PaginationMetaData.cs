namespace EcoLink.Domain.Configurations;

public class PaginationMetaData(int totalCount, PaginationParams @params)
{
    public int CurrentPage { get; set; } = @params.PageIndex;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalCount / (double)@params.PageSize);
    public int TotalCount { get; set; } = totalCount;
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
