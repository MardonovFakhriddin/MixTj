namespace Domain.Filters;

public class BaseFilter
{
    public int PageSize { get; set; }
    public int PageNubmer { get; set; }

    public BaseFilter()
    {
        PageNubmer = 1;
        PageSize = 10;
    }

    public BaseFilter(int pageSize, int pageNumber)
    {
        PageSize = pageSize <= 0 ? 10 : pageSize;
        PageNubmer = pageNumber <= 0 ? 1 : pageNumber;
    }
}